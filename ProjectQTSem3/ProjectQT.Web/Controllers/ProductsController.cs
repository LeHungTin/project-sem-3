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
        GenericRepository<DataModel.Models.Attribute> _Attribute;
        public ProductsController()
        {
            _Product = new GenericRepository<Product>();
            _TypeAttribute = new GenericRepository<TypeAttribute>();
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
                return View(product);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}