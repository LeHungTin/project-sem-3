using ProjectQT.BAL.Repositories;
using ProjectQT.DataModel.Models;
using ProjectQT.Web.Areas.Admin.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace ProjectQT.Web.Areas.Admin.Controllers
{
    //[CustomizeAuth]
    public class GroupsController : Controller
    {
        GenericRepository<Group> _group;
        GenericRepository<Business> _business;
        GenericRepository<Role> _role;
        GenericRepository<GroupRole> _groupRole;
        GenericRepository<User> _user;
        public GroupsController()
        {
            _group = new GenericRepository<Group>();
            _business = new GenericRepository<Business>();
            _role = new GenericRepository<Role>();
            _groupRole = new GenericRepository<GroupRole>();
            _user = new GenericRepository<User>();
        }

        /// <summary>
        /// Action Get List Business and list Group
        /// </summary>
        /// <returns></returns>
        // GET: Admin/Groups
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Business = _business.GetAll();
            ViewBag.Role = _role.GetAll();
            var group = _group.GetAll();
            return View(group);
        }

        /// <summary>
        /// Action Create Group Method GET
        /// </summary>
        /// <returns></returns>
        // GET: Admin/Groups
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Action Create Group Method POST
        /// </summary>
        /// <returns></returns>
        // GET: Admin/Groups
        [HttpPost]
        public ActionResult Create(Group group)
        {
            if (ModelState.IsValid)
            {
                var user = Session["User"] as User;
                group.CreateAt = DateTime.Now;
                group.Status = true;
                group.CreateBy = user.Id;
                if (_group.GetAll().FirstOrDefault(x => x.GroupName == group.GroupName) != null)
                {
                    ModelState.AddModelError("TypeName", "Category already exists");
                    return View(group);
                }
                try
                {
                    if (_group.Create(group))
                    {
                        TempData["CreateSuccess"] = "Create Success";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return View(group);
                    }
                }
                catch (Exception)
                {
                    return View(group);
                }
            }
            return View();
        }

        /// <summary>
        /// Action Delete Attribute Metod GET
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            try
            {
                var groupID = _group.GetById(id);
                var checkExitUSer = _user.GetBy(x => x.GroupId == groupID.GroupId);
                if (checkExitUSer == null)
                {
                    if (_group.Delete(id))
                    {
                        TempData["DeleteSuccess"] = "Delete Success";
                        return RedirectToAction("Index");
                    }
                    TempData["DeleteFalse"] = "Delete False";
                    return RedirectToAction("Index");
                }
                TempData["DeleteFalse"] = "Delete False";
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["DeleteFalse"] = "Delete False";
                return RedirectToAction("Index");
            }

        }


    }
}