using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectQT.Web.Controllers
{
    public class accountController : Controller
    {
        // GET: account
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Action Register Method GET
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        ///// <summary>
        ///// Action Register Method POST
        ///// </summary>
        ///// <returns></returns>
        //[HttpPost ]
        //public ActionResult Register()
        //{

        //}

    }
}