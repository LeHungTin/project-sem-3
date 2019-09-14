using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectQT.Web.Controllers
{
    public class ProductsController : Controller
    {
        
        /// <summary>
        /// Action View Detail Product Method GET
        /// </summary>
        /// <returns></returns>
        public ActionResult ProductQickView()
        {
            return View();
        }
    }
}