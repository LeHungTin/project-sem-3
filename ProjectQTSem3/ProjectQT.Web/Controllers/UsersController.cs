
using ProjectQT.BAL.Repositories;
using ProjectQT.DataModel.Models;
using ProjectQT.ViewModel.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ProjectQT.Web.Controllers
{
    public class UsersController : Controller
    {
        GenericRepository<User> _User;
        GenericRepository<Order> _Order;
        public UsersController()
        {
            _User = new GenericRepository<User>();
            _Order = new GenericRepository<Order>();
        }
        // GET: Users
        public ActionResult Index(int id)
        {
            var user = _User.GetById(id);
            return View(user);
        }

        [HttpPost]
        public ActionResult Index(User user)
        {
            _User.Update(user);
            return RedirectToAction("Index", "Home");
        }

        public static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }

        public ActionResult ChangePassword(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePassword changePassword)
        {
            var user = _User.GetById(changePassword.Id);
            user.Password = MD5Hash(changePassword.Password);
            _User.Update(user);
            return RedirectToAction("Login", "Account");
        }

        public ActionResult OrderHistory(int id)
        {
            var ordrers = _Order.GetAll().Where(x => x.UserId == id);
            return View(ordrers);
        }

        public ActionResult Detail(int id)
        {
            return View();
        }
    }
}