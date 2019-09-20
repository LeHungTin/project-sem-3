using ProjectQT.BAL.Repositories;
using ProjectQT.DataModel.Models;
using ProjectQT.Web.Areas.Admin.Models;
using System.Linq;
using System.Web.Mvc;

namespace ProjectQT.Web.Areas.Admin.Controllers
{
    [CustomizeAuth]
    public class GroupRolesController : Controller
    {
        GenericRepository<GroupRole> _groupRole;

        public GroupRolesController()
        {
            _groupRole = new GenericRepository<GroupRole>();
        }

        /// <summary>
        /// Get data Role by data JSON
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetRole(int id)
        {
            var data = _groupRole.GetAll().Where(x => x.GroupId == id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        
        /// <summary>
        /// Action Update Role
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdatePermission(GroupRole groupRole)
        {
            // kiểm tra quền đã được gán hay chưa
            if (_groupRole.GetAll().Any(x => x.GroupId == groupRole.GroupId && x.BusinessId == groupRole.BusinessId && x.RoleId == groupRole.RoleId))
            {
                // nếu có thì xóa (hủy quên)
                // lấy đối tượng cần xóa
                var obj = _groupRole.GetAll().FirstOrDefault(x => x.GroupId == groupRole.GroupId && x.BusinessId == groupRole.BusinessId && x.RoleId == groupRole.RoleId);
                _groupRole.DeleteEntity(obj);
                return Json(new
                {
                    Status = 200,
                    Message = "Successfully canceled"
                }, JsonRequestBehavior.AllowGet); ;
            }
            else
            {
                _groupRole.Create(groupRole);
                return Json(new
                {
                    Status = 200,
                    Message = "Successful assignment"
                }, JsonRequestBehavior.AllowGet);
            }

        }
    }
}