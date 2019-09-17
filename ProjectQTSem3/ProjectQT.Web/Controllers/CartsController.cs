using ProjectQT.BAL.Repositories;
using ProjectQT.DataModel.Models;
using ProjectQT.ViewModel.CartModel;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ProjectQT.Web.Controllers
{
    public class CartsController : Controller
    {
        GenericRepository<Product> _product;
        GenericRepository<Order> _order;
        GenericRepository<OrderDetail> _orderDetail;
        GenericRepository<User> _user;
        public CartsController()
        {
            _product = new GenericRepository<Product>();
            _order = new GenericRepository<Order>();
            _orderDetail = new GenericRepository<OrderDetail>();
            _user = new GenericRepository<User>();
        }

        /// <summary>
        /// Action Cart View Method GET
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            if (Session["User"] == null)
            {
                ViewData["login"] = "Please login to continue";
                return RedirectToAction("Login", "Account");
            }
            if (Session["cart"] != null)
            {
                var listCart = Session["cart"] as List<CartModel>;
                ViewBag.lisCart = listCart;
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Action Add Poroduct into Session Cart
        /// </summary>
        /// <returns></returns>
        public ActionResult AddToCart(int id)
        {
            List<CartModel> listCart = new List<CartModel>();
            var product = _product.GetById(id);
            if (Session["cart"] != null)
            {
                listCart = Session["cart"] as List<CartModel>;
                bool check = false;
                foreach (var item in listCart)
                {
                    if (item.Products.Id == product.Id)
                    {
                        item.Quantity += 1;
                        check = true;
                        break;
                    }
                }
                if (!check)
                {
                    listCart.Add(new CartModel { Products = product, Quantity = 1 });
                }
            }
            else
            {
                listCart.Add(new CartModel { Products = product, Quantity = 1 });
            }
            Session["cart"] = listCart;
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Action Add Session into OrderDetail Method POST
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddToCart(OrderDetail orderDetail)
        {
            User user = Session["User"] as User;
            List<CartModel> Listcart = Session["cart"] as List<CartModel>;
            var orderModel = new Order()
            {
                UserId = user.Id,
                UpdateAt = DateTime.Now,
                CreateAt = DateTime.Now,
                UpdateBy = user.Email,
                Status = false
            };
            try
            {
                _order.Create(orderModel);
            }
            catch (Exception)
            {
                return RedirectToAction("Index");

            }
            foreach (var item in Listcart)
            {
                var oderDetail = new OrderDetail()
                {
                    OrderId = orderModel.Id,
                    ProductId = item.Products.Id,
                    Quantity = item.Quantity,
                    Price = item.Quantity * item.Products.Price,
                    CreateBy = user.Id,
                    CreateAt = DateTime.Now
                };
                _orderDetail.Create(oderDetail);
                Session.Remove("cart");
            }

            return RedirectToAction("Index", "Home");
        }
    }
}