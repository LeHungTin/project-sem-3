using ProjectQT.BAL.Repositories;
using ProjectQT.DataModel.Models;
using ProjectQT.ViewModel.ProductModel;
using ProjectQT.ViewModel.SeachProduct;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectQT.Web.Controllers
{
    public class SearchProductController : Controller
    {
        GenericRepository<Product> _product;
        public SearchProductController()
        {
            _product = new GenericRepository<Product>();
        }
        public IEnumerable<Product> GetProduct(SeachProduct dateModel)
        {
            var listProduct = _product.GetAll().OrderByDescending(x => x.Reate);
            if (!string.IsNullOrEmpty(dateModel.ProductName))
            {
                listProduct = listProduct.Where(s => s.Name.ToLower().Contains(dateModel.ProductName.ToLower())).OrderByDescending(x => x.Reate);
            }
            if (!string.IsNullOrEmpty(dateModel.StartPrice) && !string.IsNullOrEmpty(dateModel.EndPrice))
            {
                listProduct = listProduct.Where(s => s.Price >= Convert.ToDecimal(dateModel.StartPrice) && s.Price <= Convert.ToDecimal(dateModel.EndPrice)).OrderByDescending(x => x.Reate);
            }
            if (dateModel.Reate1 == true)
            {
                listProduct = listProduct.Where(s => s.Reate <= 1).OrderByDescending(x => x.Reate);
                var count = listProduct.Count();
            }
            if (dateModel.Reate2 == true)
            {
                listProduct = listProduct.Where(s => s.Reate <= 2).OrderByDescending(x => x.Reate);
                var count = listProduct.Count();
            }
            if (dateModel.Reate3 == true)
            {
                listProduct = listProduct.Where(s => s.Reate <= 3).OrderByDescending(x => x.Reate);
                var count = listProduct.Count();
            }
            if (dateModel.Reate4 == true)
            {
                listProduct = listProduct.Where(s => s.Reate <= 4).OrderByDescending(x => x.Reate);
                var count = listProduct.Count();
            }
            if (dateModel.Reate5 == true)
            {
                listProduct = listProduct.Where(s => s.Reate <= 5).OrderByDescending(x => x.Reate);
                var count = listProduct.Count();
            }

            return listProduct;
        }

        // GET: SearchProduct
        public ActionResult Index()
        {
            ViewBag.ListProduct = _product.GetAll().AsQueryable().Include(x => x.Categorie);
            return View();
        }

        [HttpPost]
        public ActionResult FilterProduct(SeachProduct dateModel)
        {
            var listProduct = GetProduct(dateModel).ToList();
            return PartialView("_SeachProduct", listProduct);
        }
    }
}