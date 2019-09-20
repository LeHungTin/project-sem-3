using ProjectQT.BAL.Repositories;
using ProjectQT.DataModel.Models;
using ProjectQT.ViewModel.CartModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ProjectQT.Web.Controllers
{
    public class CartsController : Controller
    {
        GenericRepository<Product> _product;
        GenericRepository<Order> _order;
        GenericRepository<OrderDetail> _orderDetail;
        GenericRepository<User> _user;
        GenericRepository<DataModel.Models.Attribute> _attribute;
        GenericRepository<ProductDetailOrder> _productDetailOrder;
        public CartsController()
        {
            _product = new GenericRepository<Product>();
            _order = new GenericRepository<Order>();
            _orderDetail = new GenericRepository<OrderDetail>();
            _user = new GenericRepository<User>();
            _attribute = new GenericRepository<DataModel.Models.Attribute>();
            _productDetailOrder = new GenericRepository<ProductDetailOrder>();
        }

        /// <summary>
        /// Action Cart View Method GET
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            List<CartModel> lis = new List<CartModel>();
            if (Session["cart"] != null)
            {
                lis = Session["cart"] as List<CartModel>;

            }
            ViewBag.attrs = _attribute.GetAll();
            return View(lis);
        }

        /// <summary>
        /// Action Add Poroduct into Session Cart
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddToCart(int? id)
        {
            if (Session["User"] == null)
            {
                ViewData["login"] = "Please login to continue";
                return RedirectToAction("Login", "Account");
            }

            // Lấy sp cần thêm theo id
            var product = _product.GetAll().FirstOrDefault(x => x.Id == id);
            // Lấy các thuộc tính được chọn của người mua
            var _attributes = Request["attributes"].ToString();
            // Lấy số lượng
            var qty = Convert.ToInt32(Request["qty"]);
            if (qty<=0)
            {
                TempData["err"]= "Quantity must be greater than zero";
                return RedirectToAction("Detail", "Products", new { id = id });
            }
            CartModel cart = new CartModel()
            {
                Products = product,
                Attrs = _attributes,
                Quantity = qty
            };
            // Giỏ hàng sẽ lưu trong list này
            List<CartModel> lst = new List<CartModel>();

            if (Session["cart"] != null) // Đã có giỏ hàng
            {
                // Lấy các sp trong session ra list
                lst = Session["cart"] as List<CartModel>;
                // Kiểm tra sp đã có trong giỏ chưa
                var check = false;
                foreach (var item in lst)
                {
                    // Tiêu chí để kiểm tra sp có hay chưa
                    // ProductId
                    // Attrs
                    if (item.Products.Id == id && item.Attrs == _attributes)
                    {
                        item.Quantity += cart.Quantity;
                        check = true;
                    }
                }
                if (!check) // nếu chưa có trong giỏ thì thêm mới
                {
                    lst.Add(cart);
                }
            }
            else // Chưa có thì thêm mới sp vào bình thường
            {
                lst.Add(cart);
            }
            // Lưu trữ list trong Session
            Session["cart"] = lst;

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Action Add Session into OrderDetail Method POST
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateOrder(OrderDetail orderDetail)
        {
            User user = Session["User"] as User;
            List<CartModel> Listcart = Session["cart"] as List<CartModel>;
            var orderModel = new Order()
            {
                UserId = user.Id,
                UpdateAt = DateTime.Now,
                CreateAt = DateTime.Now,
                UpdateBy = user.Email,
                Status = false
            };
            try
            {
                _order.Create(orderModel);
            }
            catch (Exception)
            {
                return RedirectToAction("Index");

            }

            foreach (var item in Listcart)
            {
                if (item.Products.SalePrice == null)
                {
                    var oderDetail = new OrderDetail()
                    {
                        OrderId = orderModel.Id,
                        ProductId = item.Products.Id,
                        Quantity = item.Quantity,
                        Price = item.Quantity * item.Products.Price,
                        CreateBy = user.Id,
                        CreateAt = DateTime.Now
                    };
                    _orderDetail.Create(oderDetail);
                    var product = _product.GetBy(x => x.Id == item.Products.Id);
                    product.CountBuy += item.Quantity;
                    _product.Update(product);
                    var listIdAttr = item.Attrs.Split(',');
                    foreach (var item1 in listIdAttr)
                    {
                        var proDetailAttr = new ProductDetailOrder
                        {
                            OrderDeltailId = oderDetail.Id,
                            NameAtt = _attribute.GetById(Convert.ToInt32(item1)).Name
                        };
                        _productDetailOrder.Create(proDetailAttr);
                    }
                }
                else
                {
                    var oderDetail = new OrderDetail()
                    {
                        OrderId = orderModel.Id,
                        ProductId = item.Products.Id,
                        Quantity = item.Quantity,
                        Price = item.Quantity * item.Products.SalePrice,
                        CreateBy = user.Id,
                        CreateAt = DateTime.Now
                    };
                    _orderDetail.Create(oderDetail);
                    var product = _product.GetBy(x => x.Id == item.Products.Id);
                    product.CountBuy += item.Quantity;
                    _product.Update(product);

                    var listIdAttr = item.Attrs.Split(',');

                    foreach (var item1 in listIdAttr)
                    {
                        var proDetailAttr = new ProductDetailOrder
                        {
                            OrderDeltailId = oderDetail.Id,
                            NameAtt = _attribute.GetById(Convert.ToInt32(item1)).Name
                        };
                        _productDetailOrder.Create(proDetailAttr);
                    }
                }

            }
            Session.Remove("cart");
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Delete()
        {
            Session.Remove("cart");
            return RedirectToAction("Index");
        }
    }
}