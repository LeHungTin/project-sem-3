using ProjectQT.DataModel.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectQT.Web.Areas.Admin.Models
{
    public class CustomizeAuthAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (HttpContext.Current.Session["User"] == null)
            {
                return false;
            }
            if (HttpContext.Current.Session["roleId"].ToString() != "1")
            {
                return false;
            }

            var _user = (User)HttpContext.Current.Session["User"];
            //Lấy các quyền Action yêu cầu
            var requiredRoles = this.Roles.Split(',').Where(x => !string.IsNullOrEmpty(x)).ToList();

            // Lấy controller hiện tại

            var role = httpContext.Request.RequestContext.RouteData;
            string _controllerName = role.GetRequiredString("controller");
            if (requiredRoles.Count() > 0) // có yêu cần quyền
            {
                // Lấy các quền của Usẻ hiện tại 
                var _role = HttpContext.Current.Session["role"] as IEnumerable<string>;
                //Kiểm tra xem có tồn tại các quền yêu cầu trong số quền đã gán
                var check = false;
                foreach (var item in requiredRoles)
                {
                    var _r = _controllerName + "_" + item;
                    if (_role.Any(x => x == _r))
                    {
                        check = true;
                        break;
                    }
                }

                if (check)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("~/Error/Error401");
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (this.AuthorizeCore(filterContext.HttpContext))
            {
                base.OnAuthorization(filterContext);
            }
            else
            {
                this.HandleUnauthorizedRequest(filterContext);
            }
        }

    }
}