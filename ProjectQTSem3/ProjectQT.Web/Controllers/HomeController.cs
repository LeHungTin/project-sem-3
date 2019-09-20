using ProjectQT.BAL.Repositories;
using ProjectQT.DataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace ProjectQT.Web.Controllers
{
    public class HomeController : Controller
    {
        GenericRepository<Product> _Product;
        public HomeController()
        {
            _Product = new GenericRepository<Product>();
        }
        public ActionResult Index(string key, int page = 1, int pageSize = 8)
        {
            var product = _Product.GetAll().Where(x => x.Status == true);
            ViewBag.ProductViews = product.OrderByDescending(x => x.CountView);
            ViewBag.ProductSales= product.OrderByDescending(x => x.CountBuy);
            if (!string.IsNullOrEmpty(key))
            {
                product.Where(x => x.Name.ToLower().Contains(key.ToLower())).ToList();
            }
            return View(product.OrderByDescending(x => x.CreateAt).ToPagedList(page, pageSize));
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