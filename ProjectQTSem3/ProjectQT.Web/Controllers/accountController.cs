using ProjectQT.BAL.Repositories;
using ProjectQT.DataModel.Models;
using ProjectQT.ViewModel.AccountModel;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using ProjectQT.Web.Models;
using System.Security.Cryptography;
using System.Text;

namespace ProjectQT.Web.Controllers
{
    public class AccountController : Controller
    {
        GenericRepository<User> _repositoryUser;
        GenericRepository<GroupRole> _groupRole;
        public AccountController()
        {
            _repositoryUser = new GenericRepository<User>();
            _groupRole = new GenericRepository<GroupRole>();
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
        /// <summary>
        /// Action Login Method GET
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Login()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return View();
        }

        /// <summary>
        /// Action Login Method POST
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(LoginViewModel loginViewModel)
        {
            var hassPass = MD5Hash(loginViewModel.Password);
            var user = _repositoryUser.GetAll().FirstOrDefault(x => x.Email == loginViewModel.Email && x.Password == hassPass);
            if (user == null)
            {
                ViewBag.err = "Wrong account or password!";
                return View();
            }
            if (user.Status == false)
            {
                ViewBag.err1 = "Your account has been locked";
                return View();
            }
            if (user != null)
            {
                var id = user.Id;
                var fullName = user.FullName;
                //Session.Add("Id", id);
                //Session.Add("FullName", fullName);

                Session["User"] = user;
                var permissions = _groupRole.GetAll().Where(x => x.GroupId == user.GroupId).Select(x => x.BusinessId + "_" + x.RoleId);
                //VD: {"Business_Add", "Business_VIew}
                Session["role"] = permissions;
                Session["roleId"] = user.GroupId;
                return RedirectToAction("Index", "Home");
            }
            ViewBag.err = "Wrong account or password!";
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


        /// <summary>
        /// Action Register Method POST
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                User user = new User();
                user.Email = registerViewModel.Email;
                var md5data = MD5Hash(registerViewModel.Password);
                user.Password = md5data;
                user.FullName = registerViewModel.FullName;
                user.BirthDay = registerViewModel.BirthDay;
                user.PhoneNumber = registerViewModel.PhoneNumber;
                user.Address = registerViewModel.Address;
                user.CreateAt = DateTime.Now;
                user.UpdateAt = DateTime.Now;
                user.GroupId = 3;
                user.Status = true;
                if (_repositoryUser.GetAll().FirstOrDefault(x => x.Email == user.Email) != null)
                {
                    ModelState.AddModelError("Email", "Email already exists");
                    return View(registerViewModel);
                }
                if (_repositoryUser.Create(user))
                {
                    Helper.SendMail(user.Email, "quangciucuca@gmail.com", "Anhquang1009", "QTShop_Đăng ký tài khoản", string.Format(@"
                    <h1> Đăng ký tài khoản thành công</h1>
                    <b>Email đăng ký :</b> {0}
                    <p>Visit: https://localhost:44341</p>
                    <p>Trân trọng </p>
                    ", user.Email));
                    TempData["RigisterSucess"] = "Register successfully!";
                    return RedirectToAction("Login");
                }
                return View(registerViewModel);

            }
            return View(registerViewModel);
        }

        /// <summary>
        /// Action Logout
        /// </summary>
        /// <returns></returns>
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("index", "Home");
        }
    }
}