using ProjectQT.BAL.Repositories;
using ProjectQT.DataModel.Models;
using ProjectQT.ViewModel.UserModel;
using ProjectQT.Web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectQT.Web.Areas.Admin.Controllers
{
    //[CustomizeAuth]
    public class UserController : Controller
    {
        GenericRepository<User> _user;
        GenericRepository<Group> _group;


        public UserController()
        {
            _user = new GenericRepository<User>();
            _group = new GenericRepository<Group>();

        }

        /// <summary>
        /// Action Get List User
        /// </summary>
        /// <returns></returns>
        // GET: Admin/User
        public ActionResult Index()
        {
            var user = _user.GetAll().AsQueryable().Include(x => x.Groups);
            return View(user);
        }

        /// <summary>
        /// Action Get Lock And Unlock User
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult LockUser(int id)
        {
            var user = _user.GetById(id);
            user.Status = false;
            _user.Update(user);
            TempData["LockSuccess"] = "Lock User Success";
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Action Get Lock And Unlock User
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult UnLockUser(int id)
        {
            var user = _user.GetById(id);
            user.Status = true;
            _user.Update(user);
            TempData["UnLockSuccess"] = "UnLock User Success";
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Action Detail User
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Detail(int id)
        {
            var user = _user.GetById(id);
            var group = _group.GetBy(x => x.GroupId == user.GroupId);
            UserModel userModel = new UserModel()
            {
                Id = user.Id,
                CreateAt = user.CreateAt,
                Status = user.Status,
                Email = user.Email,
                GroupId = group.GroupName,
                FullName = user.FullName,
                BirthDay = user.BirthDay,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                UpdateAt = user.UpdateAt,
            };
            return View(userModel);
        }

        /// <summary>
        /// Action Edit User Method GET
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var user = _user.GetById(id);
            ViewBag.GroupId = new SelectList(_group.GetAll(), "GroupId", "GroupName", user.GroupId);
            return View(user);
        }

        /// <summary>
        /// Action Edit User Method POST
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(User user)
        {
            ViewBag.GroupId = new SelectList(_group.GetAll(), "GroupId", "GroupName");
            if (ModelState.IsValid)
            {
                user.UpdateAt = DateTime.Now;
                user.CreateAt = DateTime.Now;
                if (_user.Update(user))
                {
                    TempData["UpdateSuccess"] = "Update Success";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["UpdateFalse"] = "Update False!!";
                    return View(user);
                }
            }
            return View(user);
        }

    }
}