using ProjectQT.BAL.Repositories;
using ProjectQT.DataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectQT.Web.Controllers
{
    public class ProductsController : Controller
    {
        GenericRepository<Product> _Product;
        public ProductsController()
        {
            _Product = new GenericRepository<Product>();
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
            var product = _Product.GetById(id);
            product.CountView += 1;
            if (_Product.Update(product))
            {
                return View(product);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}