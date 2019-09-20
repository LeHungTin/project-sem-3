using ProjectQT.BAL.Repositories;
using ProjectQT.DataModel.Models;
using ProjectQT.ViewModel.TypeAttributeModel;
using ProjectQT.Web.Areas.Admin.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace ProjectQT.Web.Areas.Admin.Controllers
{
    [CustomizeAuth]
    public class TypeAttributeController : Controller
    {
        GenericRepository<TypeAttribute> _typeAttribute;
        GenericRepository<DataModel.Models.Attribute> _attribute;
        GenericRepository<User> _user;
        public TypeAttributeController()
        {
            _typeAttribute = new GenericRepository<TypeAttribute>();
            _user = new GenericRepository<User>();
            _attribute = new GenericRepository<DataModel.Models.Attribute>();
        }

        /// <summary>
        /// Action Get List Type Attributes
        /// </summary>
        /// <returns></returns>
        // GET: Admin/TypeAttribute
        [CustomizeAuth(Roles = "Detail")]

        public ActionResult Index()
        {
            var listTypeAttributes = from typeAttribue in _typeAttribute.GetAll()
                               join user in _user.GetAll()
                               on typeAttribue.CreateBy equals user.Id
                               select new TypeAttributeModel
                               {
                                   Id = typeAttribue.Id,
                                   CreateAt = typeAttribue.CreateAt,
                                   CreateBy = user.Email,
                                   Status = typeAttribue.Status,
                                   TypeName = typeAttribue.TypeName,
                               };
            return View(listTypeAttributes);
        }
        
        /// <summary>
        /// Action Create Type Attributes Method GET
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [CustomizeAuth(Roles = "Add New")]
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Action Create Type Attributes Method POST
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(TypeAttribute typeAttribute)
        {
            if (ModelState.IsValid)
            {
                var user = Session["User"] as User;
                typeAttribute.CreateAt = DateTime.Now;
                typeAttribute.Status = true;
                typeAttribute.CreateBy = user.Id;
                if (_typeAttribute.GetAll().FirstOrDefault(x => x.TypeName == typeAttribute.TypeName) != null)
                {
                    ModelState.AddModelError("TypeName", "Category already exists");
                    return View(typeAttribute);
                }
                try
                {
                    if (_typeAttribute.Create(typeAttribute))
                    {
                        TempData["CreateSuccess"] = "Create Success";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return View(typeAttribute);
                    }
                }
                catch (Exception)
                {
                    return View(typeAttribute);
                }
            }
            return View();
        }

        /// <summary>
        /// Action Edit Type Attributes Method GET
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [CustomizeAuth(Roles = "Update")]
        public ActionResult Edit(int id)
        {
            var TypeAttribute = _typeAttribute.GetById(id);
            return View(TypeAttribute);
        }
        
        /// <summary>
        /// Action Edit Type Attributes Method POST
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(TypeAttribute typeAttribute)
        {
            if (_typeAttribute.Update(typeAttribute))
            {
                TempData["UpdateSuccess"] = "Update Success";
                return RedirectToAction("Index");
            }
            TempData["UpdateFalse"] = "Update False!";
            return View(typeAttribute);
        }

        /// <summary>
        /// Action Delete TypeAttribute Metod GET
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [CustomizeAuth(Roles = "Delete")]
        public ActionResult Delete(int? id)
        {
            try
            {
                var typeAttributeId = _typeAttribute.GetById(id);
                var checkExitAttributeId = _attribute.GetBy(x => x.TypeId == typeAttributeId.Id);
                if (checkExitAttributeId == null)
                {
                    if (_typeAttribute.Delete(id))
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