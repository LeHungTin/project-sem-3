using ProjectQT.Web.Areas.Admin.Models;
using System.Web.Mvc;

namespace ProjectQT.Web.Areas.Admin.Controllers
{
    //[CustomizeAuth]
    public class HomeController : Controller
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}