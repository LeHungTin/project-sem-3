using ProjectQT.BAL.Repositories;
using ProjectQT.DataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectQT.Web.Controllers
{
    public class HomeController : Controller
    {
        GenericRepository<Product> _Product;
        public HomeController()
        {
            _Product = new GenericRepository<Product>();
        }
        public ActionResult Index()
        {
            var product = _Product.GetAll();
            ViewBag.ProductViews = product.OrderByDescending(x => x.CountView);
            ViewBag.ProductSales= product.OrderByDescending(x => x.CountBuy);
            return View(product);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}