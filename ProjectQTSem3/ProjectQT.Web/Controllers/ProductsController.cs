using ProjectQT.BAL.Repositories;
using ProjectQT.DataModel.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectQT.Web.Controllers
{
    public class ProductsController : Controller
    {
        GenericRepository<Product> _Product;
        GenericRepository<TypeAttribute> _TypeAttribute;
        GenericRepository<Feedback> _feeback;
        GenericRepository<OrderDetail> _orderDetail;
        GenericRepository<DataModel.Models.Attribute> _Attribute;
        public ProductsController()
        {
            _Product = new GenericRepository<Product>();
            _TypeAttribute = new GenericRepository<TypeAttribute>();
            _feeback = new GenericRepository<Feedback>();
            _orderDetail = new GenericRepository<OrderDetail>();
            _Attribute = new GenericRepository<DataModel.Models.Attribute>();
        }

        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Action View Detail Product Method GET
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public ActionResult Detail(int id)
        {
            var product = _Product.GetAll().AsQueryable()
                .Include(x => x.ProductAttributes)
                .Include(x => x.Categorie)
                .FirstOrDefault(x => x.Id == id);
            product.CountView += 1;

            if (_Product.Update(product))
            {
                var data = (from p in product.ProductAttributes
                            join a in _Attribute.GetAll() on p.AttributeId equals a.Id
                            join t in _TypeAttribute.GetAll() on a.TypeId equals t.Id
                            group a by new { t.Id, t.TypeName } into gr
                            select new
                            {
                                Id = gr.Key.Id,
                                TypeName = gr.Key.TypeName,
                                Attributes = gr.ToList()
                            }).Select(x => new TypeAttribute
                            {
                                Id = x.Id,
                                TypeName = x.TypeName,
                                Attributes = x.Attributes
                            }).ToList();
                ViewBag.types = data;
                var listFeedback = _feeback.GetAll().AsQueryable().Include(x => x.Users).Where(x => x.ProductId == product.Id).ToList();
                var orderDetail = _orderDetail.GetAll();
                foreach (var item in listFeedback)
                {
                    if (orderDetail.Any(x => x.ProductId == item.ProductId))
                    {
                        item.existOrder = true;
                        _feeback.Update(item);
                    }
                }
                ViewBag.listFeedback = listFeedback;
                ViewBag.countFeedback = listFeedback.Count();
                return View(product);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult CreateFeedback(Feedback feedback)
        {
            var user = Session["User"] as User;
            var feedbackModel = new Feedback()
            {
                UserId = user.Id,
                CreateAt = DateTime.Now,
                Email = user.Email,
                Title = feedback.Title,
                Content = feedback.Content,
                FeedbackStar = feedback.FeedbackStar,
                Status = true,
                ProductId = feedback.ProductId,
                existOrder = false
            };
            _feeback.Create(feedbackModel);
            float countStar = 0;
            var listFeedBack = _feeback.GetAll().Where(x => x.ProductId == feedbackModel.ProductId);
            float countUser = listFeedBack.Count();
            foreach (var item in listFeedBack)
            {
                countStar += item.FeedbackStar;
            }
            var product = _Product.GetById(feedbackModel.ProductId);
            product.Reate = countStar / countUser;
            _Product.Update(product);

            return RedirectToAction("Index", "Home");

        }
    }
}