using ProjectQT.BAL.Repositories;
using ProjectQT.DataModel.Models;
using ProjectQT.ViewModel.ProductModel;
using ProjectQT.Web.Areas.Admin.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace ProjectQT.Web.Areas.Admin.Controllers
{
    [CustomizeAuth]
    public class ProductsController : Controller
    {
        GenericRepository<Product> _product;
        GenericRepository<User> _user;
        GenericRepository<Category> _category;
        GenericRepository<TypeAttribute> _typeAttribute;
        GenericRepository<ProductAttribute> _productAttribute;
        GenericRepository<OrderDetail> _orderDetail;
        GenericRepository<DataModel.Models.Attribute> _attribute;

        public ProductsController()
        {
            _product = new GenericRepository<Product>();
            _user = new GenericRepository<User>();
            _category = new GenericRepository<Category>();
            _attribute = new GenericRepository<DataModel.Models.Attribute>();
            _typeAttribute = new GenericRepository<TypeAttribute>();
            _orderDetail = new GenericRepository<OrderDetail>();
            _productAttribute = new GenericRepository<ProductAttribute>();
        }

        /// <summary>
        /// Action Get All Product Method GET 
        /// </summary>
        /// <returns></returns>
        // GET: Admin/Products
        [HttpGet]
        [CustomizeAuth(Roles = "Detail")]
        public ActionResult Index()
        {
            var listProduct = _product.GetAll().AsQueryable().Include(x => x.Categorie);
            return View(listProduct);
        }

        /// <summary>
        /// Action Get By Id Product Method GET 
        /// </summary>
        /// <returns></returns>
        // GET: Admin/Products
        [HttpGet]
        [CustomizeAuth(Roles = "Detail")]
        public ActionResult Detail(int? id)
        {
            var listProduct = from pro in _product.GetAll()
                              join user in _user.GetAll()
                              on pro.CreateBy equals user.Id
                              join cate in _category.GetAll()
                              on pro.CategoryId equals cate.Id
                              select new ListProductModel
                              {
                                  Id = pro.Id,
                                  CreateAt = pro.CreateAt,
                                  CreateBy = user.Email,
                                  Status = pro.Status,
                                  CategoryId = cate.Name,
                                  ProduceCode = pro.ProduceCode,
                                  Name = pro.Name,
                                  UpdateAt = pro.UpdateAt,
                                  UpdateBy = user.Email,
                                  Price = pro.Price,
                                  SalePrice = pro.SalePrice,
                                  Description = pro.Description,
                                  ImageUrl = pro.ImageUrl
                              };
            var product = listProduct.FirstOrDefault(x => x.Id == id);
            ViewBag.ProductAttribute = _productAttribute.GetAll().AsQueryable().Include(x => x.AttributeId).Where(x => x.ProductId == product.Id);
            return View(product);
        }

        /// <summary>
        /// Action Create Product Method GET 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [CustomizeAuth(Roles = "Create")]
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(_category.GetAll().Where(x => x.Status == true), "Id", "Name");
            ViewBag.TypeAttribute = _typeAttribute.GetAll().AsQueryable().Include(x => x.Attributes).AsEnumerable();
            return View();
        }

        /// <summary>
        /// Action Create Product Method POST 
        /// </summary>
        /// <returns></returns>
        [CustomizeAuth(Roles = "Add New")]
        [HttpPost]
        public ActionResult Create(Product product)
        {
            ViewBag.CategoryId = new SelectList(_category.GetAll(), "Id", "Name");
            ViewBag.TypeAttribute = _typeAttribute.GetAll().AsQueryable().Include(x => x.Attributes).AsEnumerable();
            if (ModelState.IsValid)
            {
                var user = Session["User"] as User;
                if (product.ProductAttributes != null)
                {
                    foreach (var item in product.ProductAttributes)
                    {
                        item.ProductId = product.Id;
                    }
                }
                product.UpdateAt = DateTime.Now;
                product.UpdateBy = user.Email;
                product.CreateAt = DateTime.Now;
                product.CreateBy = user.Id;
                product.Status = true;
                product.Reate = 5;
                product.CountBuy = 0;
                product.CountView = 0;
                if (_product.Create(product))
                {
                    TempData["CreateSuccess"] = "Create Success";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["CreateFalse"] = "Create False!";
                    return View(product);
                }
            }
            return View(product);
        }

        /// <summary>
        /// Action Edit Product Method GET 
        /// </summary>
        /// <returns></returns>
        [CustomizeAuth(Roles = "Update")]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var product = _product.GetAll().AsQueryable().Include(x => x.ProductAttributes).FirstOrDefault(x => x.Id == id);
            ViewBag.CategoryId = new SelectList(_category.GetAll(), "Id", "Name", product.CategoryId);
            ViewBag.TypeAttribute = _typeAttribute.GetAll().AsQueryable().Include(x => x.Attributes).AsEnumerable();
            return View(product);
        }

        /// <summary>
        /// Action Edit Product Method POST 
        /// </summary>
        /// <returns></returns>
        [CustomizeAuth(Roles = "Update")]
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            ViewBag.CategoryId = new SelectList(_category.GetAll(), "Id", "Name");
            ViewBag.TypeAttribute = _typeAttribute.GetAll().AsQueryable().Include(x => x.Attributes).AsEnumerable();
            if (ModelState.IsValid)
            {
                var user = Session["User"] as User;
                if (product.ProductAttributes != null)
                {
                    foreach (var item in product.ProductAttributes)
                    {
                        item.ProductId = product.Id;
                    }
                    if (!_productAttribute.DeleteRange(_productAttribute.Get(x => x.ProductId == product.Id)))
                    {
                        TempData["UpdateFalse"] = "Update False!";
                        return View(product);
                    }
                    if (!_productAttribute.CreateRange(product.ProductAttributes))
                    {
                        TempData["UpdateFalse"] = "Update False!";
                        return View(product);
                    }
                }
                product.UpdateAt = DateTime.Now;
                product.UpdateBy = user.Email;
                product.CreateBy = user.Id;
                product.Reate = 5;
                product.CountBuy = 0;
                product.CountView = 0;
                if (_product.Update(product))
                {
                    TempData["UpdateSuccess"] = "Update Success";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["UpdateFalse"] = "Update False!!";
                    return View(product);
                }
            }
            return View(product);
        }

        /// <summary>
        /// Action Delete Product Metod GET
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [CustomizeAuth(Roles = "Delete")]
        public ActionResult Delete(int? id)
        {
            try
            {
                var productId = _product.GetById(id);
                var checkExitOrder= _orderDetail.GetBy(x => x.ProductId == productId.Id);
                if (checkExitOrder == null)
                {
                    if (_product.Delete(id))
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