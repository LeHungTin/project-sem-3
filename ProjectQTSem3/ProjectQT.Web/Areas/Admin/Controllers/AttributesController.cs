using ProjectQT.BAL.Repositories;
using ProjectQT.DataModel.Models;
using ProjectQT.ViewModel.AttributeModel;
using ProjectQT.Web.Areas.Admin.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace ProjectQT.Web.Areas.Admin.Controllers
{
    //[CustomizeAuth]
    public class AttributesController : Controller
    {

        GenericRepository<DataModel.Models.Attribute> _attribute;
        GenericRepository<TypeAttribute> _Typeattribute;
        GenericRepository<ProductAttribute> _productAttribute;
        GenericRepository<User> _user;

        public AttributesController()
        {
            _attribute = new GenericRepository<DataModel.Models.Attribute>();
            _user = new GenericRepository<User>();
            _productAttribute = new GenericRepository<ProductAttribute>();
            _Typeattribute = new GenericRepository<TypeAttribute>();
        }

        /// <summary>
        /// Action Get List Attribute Method GET
        /// </summary>
        /// <returns></returns>
        // GET: Admin/Attributes
        [HttpGet]
        public ActionResult Index()
        {
            var listAttribute = from attribue in _attribute.GetAll()
                               join user in _user.GetAll()
                               on attribue.CreateBy equals user.Id
                               join type in _Typeattribute.GetAll()
                               on attribue.TypeId equals type.Id
                               select new AttributeModel
                               {
                                   Id = attribue.Id,
                                   CreateAt = attribue.CreateAt,
                                   CreateBy = user.Email,
                                   Status = attribue.Status,
                                   Name = attribue.Name,
                                   TypeName = type.TypeName,
                                   UpdateAt = attribue.UpdateAt,
                                   UpdateBy = user.Email,
                               };
            return View(listAttribute);
        }

        /// <summary>
        /// Action Detail Attribut Method GET
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Detail(int? id)
        {
            var atrribute = _attribute.GetById(id);
            var user = _user.GetBy(x => x.Id == atrribute.CreateBy);
            var typeAttribute = _Typeattribute.GetBy(x => x.Id == atrribute.TypeId);
            GetAtrributeModel atrributeModel = new GetAtrributeModel()
            {
                Id = atrribute.Id,
                CreateAt = atrribute.CreateAt,
                CreateBy = user.Email,
                Status = atrribute.Status,
                Name = atrribute.Name,
                TypeId = typeAttribute.TypeName,
                Value = atrribute.Value,
                UpdateAt = atrribute.UpdateAt,
                UpdateBy = atrribute.UpdateBy
            };
            return View(atrributeModel);
        }

        /// <summary>
        /// Action Create Attribute Method GET
        /// </summary>
        /// <returns></returns>
        // GET: Admin/Attributes
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.TypeId = new SelectList(_Typeattribute.GetAll(), "Id", "TypeName");
            return View();
        }

        /// <summary>
        /// Action Create Attribute Method POST
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(AttributeModel attributeModel)
        {
            if (ModelState.IsValid)
            {
                var user = Session["User"] as User;
                var atrribute = new DataModel.Models.Attribute();
                atrribute.CreateAt = DateTime.Now;
                atrribute.Status = true;
                atrribute.Name = attributeModel.Name;
                atrribute.UpdateAt = DateTime.Now;
                atrribute.CreateBy = user.Id;
                atrribute.UpdateBy = user.Email;
                atrribute.TypeId = attributeModel.TypeId;
                atrribute.Value = "1";
                if (_attribute.GetAll().FirstOrDefault(x => x.Name == attributeModel.Name) != null)
                {
                    ModelState.AddModelError("Name", "Category already exists");
                    return View(attributeModel);
                }
                try
                {
                    if (_attribute.Create(atrribute))
                    {
                        TempData["CreateSuccess"] = "Create Success";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return View(attributeModel);
                    }
                }
                catch (Exception)
                {
                    return View(attributeModel);
                }
            }
            return View();
        }

        /// <summary>
        /// Action Edit Atribute Method GET
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            var category = _attribute.GetById(id);
            var attributeModel = new AttributeModel();
            attributeModel.Id = category.Id;
            attributeModel.Name = category.Name;
            attributeModel.Status = category.Status;
            attributeModel.CreateBy = category.CreateBy.ToString();
            attributeModel.UpdateBy = category.UpdateBy;
            attributeModel.CreateAt = category.CreateAt;
            attributeModel.UpdateAt = category.UpdateAt;
            attributeModel.TypeId = category.TypeId;
            attributeModel.Value = category.Value;

            return View(attributeModel);
        }

        /// <summary>
        /// Action Edit Category Method POST
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(AttributeModel attributeModel)
        {
            var user = Session["User"] as User;
            if (ModelState.IsValid)
            {
                var attribute= new DataModel.Models.Attribute();
                attribute.Name = attributeModel.Name;
                attribute.CreateAt = attributeModel.CreateAt;
                attribute.UpdateAt = DateTime.Now;
                attribute.UpdateBy = user.Email;
                attribute.Status = attributeModel.Status;
                attribute.Id = attributeModel.Id;
                attribute.TypeId = attributeModel.TypeId;
                attribute.Value = attributeModel.Value;
                attribute.CreateBy = Convert.ToInt32(attributeModel.CreateBy);

                try
                {
                    if (_attribute.Update(attribute))
                    {
                        TempData["UpdateSuccess"] = "Update Success";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["UpdateFalse"] = "Update False!";
                        return View(attributeModel);
                    }
                }
                catch (Exception)
                {
                    TempData["UpdateFalse"] = "Update False!";
                    return View(attributeModel);
                }
            }
            return View(attributeModel);
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
                var attributeId = _attribute.GetById(id);
                var checkExitProduct = _productAttribute.GetBy(x => x.AttributeId == attributeId.Id);
                if (checkExitProduct == null)
                {
                    if (_attribute.Delete(id))
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