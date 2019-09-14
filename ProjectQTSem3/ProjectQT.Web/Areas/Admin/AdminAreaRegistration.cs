using System.Web.Mvc;

namespace ProjectQT.Web.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_Home",
                "Admin",
                new { Controller = "Home" ,action = "Index", id = UrlParameter.Optional },
                new[] { "ProjectQT.Web.Areas.Admin.Controllers"}
            );

            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new[] { "ProjectQT.Web.Areas.Admin.Controllers"}
            );
        }
    }
}