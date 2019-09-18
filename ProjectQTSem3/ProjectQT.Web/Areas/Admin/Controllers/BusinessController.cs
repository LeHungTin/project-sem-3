using ProjectQT.BAL.Repositories;
using ProjectQT.DataModel.Models;
using ProjectQT.Web.Areas.Admin.Models;
using System.Linq;
using System.Web.Mvc;

namespace ProjectQT.Web.Areas.Admin.Controllers
{
    //[CustomizeAuth]
    public class BusinessController : Controller
    {
        GenericRepository<Business> _business;
        public BusinessController()
        {
            _business = new GenericRepository<Business>();
        }

        /// <summary>
        /// Action Get All Business
        /// </summary>
        /// <returns></returns>
        // GET: Admin/Business
        [HttpGet]
        public ActionResult Index()
        {
            var business = _business.GetAll();
            return View(business);
        }

        // GET: Admin/Business
        public ActionResult UpdateBusiness()
        {
            Reflection reflection = new Reflection();
            var controller = reflection.GetController("ProjectQT.Web.Areas.Admin.Controllers").Select(x => x.Name.Replace("Controller",""));
            var oldBusiness = _business.GetAll();
            foreach (var item in controller)
            {
                if (!oldBusiness.Any(x => x.BusinessId == item))
                {
                    Business business = new Business()
                    {
                        BusinessId = item,
                        BusinessName = "Uploading ...",
                        Status = true
                    };

                    _business.Create(business);
                }

            }
            return RedirectToAction("Index");
        }
    }
}