using ProjectQT.BAL.Repositories;
using ProjectQT.DataModel.Models;
using ProjectQT.ViewModel.CategoryModel;
using ProjectQT.Web.Areas.Admin.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace ProjectQT.Web.Areas.Admin.Controllers
{
    [CustomizeAuth]
    public class CategoriesController : Controller
    {
        GenericRepository<Category> _category;
        GenericRepository<User> _user;
        GenericRepository<Product> _product;

        public CategoriesController()
        {
            _category = new GenericRepository<Category>();
            _user = new GenericRepository<User>();
            _product = new GenericRepository<Product>();
        }

        /// <summary>
        /// Action Get All list Category 
        /// </summary>
        /// <returns></returns>
        // GET: Admin/Categories
        [CustomizeAuth(Roles = "Detail")]

        public ActionResult Index()
        {
            var listCategory = from category in _category.GetAll()
                               join user in _user.GetAll()
                               on category.CreateBy equals user.Id
                               select new CategoryModel
                               {
                                   Id = category.Id,
                                   CreateAt = category.CreateAt,
                                   CreateBy = user.Email,
                                   Status = category.Status,
                                   Name = category.Name,
                                   UpdateAt = category.UpdateAt,
                                   UpdateBy = user.Email,
                               };
            return View(listCategory);
        }

        /// <summary>
        /// Action Detail Category Method GET
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [CustomizeAuth(Roles = "Detail")]

        public ActionResult Detail(int? id)
        {
            var category = _category.GetById(id);
            return View(category);
        }

        /// <summary>
        /// Action Create Category Method GET
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [CustomizeAuth(Roles = "Add New")]
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Action Create Category Method POST
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(CategoryModel categoryModel)
        {
            if (ModelState.IsValid)
            {
                var user = Session["User"] as User;
                var category = new Category();
                category.CreateAt = DateTime.Now;
                category.Status = true;
                category.Name = categoryModel.Name;
                category.UpdateAt = DateTime.Now;
                category.CreateBy = user.Id;
                category.UpdateBy = user.FullName;
                if (_category.GetAll().FirstOrDefault(x => x.Name == category.Name) != null)
                {
                    ModelState.AddModelError("Name", "Category already exists");
                    return View(categoryModel);
                }
                try
                {
                    if (_category.Create(category))
                    {
                        TempData["CreateSuccess"] = "Create Success";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return View(categoryModel);
                    }
                }
                catch (Exception)
                {
                    return View(categoryModel);
                }
            }
            return View();
        }

        /// <summary>
        /// Action Edit Category Method GET
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [CustomizeAuth(Roles = "Update")]
        public ActionResult Edit(int? id)
        {
            var category = _category.GetById(id);
            var categoryModel = new CategoryModel();
            categoryModel.Id = category.Id;
            categoryModel.Name = category.Name;
            categoryModel.Status = category.Status;
            categoryModel.CreateBy = category.CreateBy.ToString();
            categoryModel.UpdateBy = category.UpdateBy;
            categoryModel.CreateAt = category.CreateAt;
            categoryModel.UpdateAt = category.UpdateAt;

            return View(categoryModel);
        }

        /// <summary>
        /// Action Edit Category Method POST
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(CategoryModel categoryModel)
        {
            var user = Session["User"] as User;
            if (ModelState.IsValid)
            {
                var category = new Category();
                category.Name = categoryModel.Name;
                category.CreateAt = categoryModel.CreateAt;
                category.UpdateAt = DateTime.Now;
                category.UpdateBy = user.Email;
                category.Status = categoryModel.Status;
                category.Id = categoryModel.Id;
                category.CreateBy = Convert.ToInt32(categoryModel.CreateBy);

                try
                {
                    if (_category.Update(category))
                    {
                        TempData["UpdateSuccess"] = "Update Success";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["UpdateFalse"] = "Update False!";
                        return View(categoryModel);
                    }
                }
                catch (Exception)
                {
                    TempData["UpdateFalse"] = "Update False!";
                    return View(categoryModel);
                }
            }
            return View(categoryModel);
        }

        /// <summary>
        /// Action Delete Category Metod GET
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [CustomizeAuth(Roles = "Delete")]
        public ActionResult Delete(int? id)
        {
            try
            {
                var categoryId = _category.GetById(id);
                var checkExitProduct = _product.GetBy(x => x.CategoryId == categoryId.Id);
                if (checkExitProduct == null)
                {
                    if (_category.Delete(id))
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