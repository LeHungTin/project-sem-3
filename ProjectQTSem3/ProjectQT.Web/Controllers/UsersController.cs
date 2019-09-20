
using ProjectQT.BAL.Repositories;
using ProjectQT.DataModel.Models;
using ProjectQT.ViewModel.OrderModel;
using ProjectQT.ViewModel.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ProjectQT.Web.Controllers
{
    public class UsersController : Controller
    {
        GenericRepository<User> _User;
        GenericRepository<Order> _Order;
        GenericRepository<Product> _Product;
        GenericRepository<ProductDetailOrder> _productDetailOrder;
        GenericRepository<OrderDetail> _orderDetail;
        public UsersController()
        {
            _User = new GenericRepository<User>();
            _Order = new GenericRepository<Order>();
            _Product = new GenericRepository<Product>();
            _productDetailOrder = new GenericRepository<ProductDetailOrder>();
            _orderDetail = new GenericRepository<OrderDetail>();

        }
        // GET: Users
        public ActionResult Index(int id)
        {
            var user = _User.GetById(id);
            return View(user);
        }

        [HttpPost]
        public ActionResult Index(User user)
        {
            _User.Update(user);
            return RedirectToAction("Index", "Home");
        }

        public static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }

        public ActionResult ChangePassword(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePassword changePassword)
        {
            var user = _User.GetById(changePassword.Id);
            user.Password = MD5Hash(changePassword.Password);
            _User.Update(user);
            TempData["UpdateSuccess"] = "Update Success";
            return RedirectToAction("Login", "Account");
        }

        public ActionResult OrderHistory(int id)
        {
            var orderModel = (from order in _Order.GetAll()
                              join user in _User.GetAll()
                              on order.UserId equals user.Id
                              select new GetAllOrderModel
                              {
                                  Id = order.Id,
                                  CreateAt = order.CreateAt,
                                  CreateBy = user.Id,
                                  Status = order.Status,
                                  UpdateAt = order.UpdateAt,
                                  UpdateBy = user.Email,
                                  UserId = user.Email,
                                  HandelerId = order.HandelerId
                              }).Where(X => X.CreateBy == id);
            return View(orderModel);
        }

        public ActionResult OrderDetail(int id)
        {
            var orderDetailModel = from orderDetail in _orderDetail.GetAll()
                                   join product in _Product.GetAll()
                                   on orderDetail.ProductId equals product.Id
                                   join user in _User.GetAll()
                                   on orderDetail.CreateBy equals user.Id
                                   where orderDetail.OrderId == id
                                   select new OrderDetailModel
                                   {
                                       Id = orderDetail.Id,
                                       CreateAt = orderDetail.CreateAt,
                                       CreateBy = user.Email,
                                       OrderId = orderDetail.OrderId,
                                       ProductId = product.Name,
                                       Quantity = orderDetail.Quantity,
                                       Status = orderDetail.Status,
                                       Price = orderDetail.Price,
                                       NameAttr = (from attrName in _productDetailOrder.GetMany(x => x.OrderDeltailId == orderDetail.Id)
                                                   where attrName.OrderDeltailId == orderDetail.Id
                                                   select attrName.NameAtt).ToList()
                                   };
            ViewBag.OrderDetail = orderDetailModel;
            var getOrder = _Order.GetAll().FirstOrDefault(x => x.Id == id);
            return View(getOrder);
        }
    }
}