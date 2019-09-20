using ProjectQT.BAL.Repositories;
using ProjectQT.DataModel.Models;
using ProjectQT.ViewModel.OrderModel;
using ProjectQT.Web.Areas.Admin.Models;
using ProjectQT.Web.Models;
using System.Linq;
using System.Web.Mvc;

namespace ProjectQT.Web.Areas.Admin.Controllers
{
    [CustomizeAuth]

    public class OrderController : Controller
    {
        GenericRepository<Order> _order;
        GenericRepository<User> _user;
        GenericRepository<OrderDetail> _orderDetail;
        GenericRepository<Product> _product;
        GenericRepository<ProductDetailOrder> _productDetailOrder;

        public OrderController()
        {
            _order = new GenericRepository<Order>();
            _user = new GenericRepository<User>();
            _orderDetail = new GenericRepository<OrderDetail>();
            _product = new GenericRepository<Product>();
            _productDetailOrder = new GenericRepository<ProductDetailOrder>();
        }

        /// <summary>
        /// Action Get All Order
        /// </summary>
        /// <returns></returns>
        [CustomizeAuth(Roles = "Detail")]

        public ActionResult Index()
        {
            var orderModel = from order in _order.GetAll()
                             join user in _user.GetAll()
                             on order.UserId equals user.Id
                             select new GetAllOrderModel
                             {
                                 Id = order.Id,
                                 CreateAt = order.CreateAt,
                                 CreateBy = user.Id,
                                 Status = order.Status,
                                 UpdateAt = order.UpdateAt,
                                 UpdateBy = user.Email,
                                 UserId = user.Email,
                                 HandelerId = order.HandelerId
                             };
            return View(orderModel);
        }

        /// <summary>
        /// Action Get OrderDetail Method get
        /// </summary>
        /// <returns></returns>
        [CustomizeAuth(Roles = "Detail")]

        public ActionResult OrderDetail(int id)
        {

            var orderDetailModel = from orderDetail in _orderDetail.GetAll()
                                   join product in _product.GetAll()
                                   on orderDetail.ProductId equals product.Id
                                   join user in _user.GetAll()
                                   on orderDetail.CreateBy equals user.Id
                                   where orderDetail.OrderId == id
                                   select new OrderDetailModel
                                   {
                                       Id = orderDetail.Id,
                                       CreateAt = orderDetail.CreateAt,
                                       CreateBy = user.Email,
                                       OrderId = orderDetail.OrderId,
                                       ProductId = product.Name,
                                       Quantity = orderDetail.Quantity,
                                       Status = orderDetail.Status,
                                       Price = orderDetail.Price,
                                       NameAttr = (from attrName in _productDetailOrder.GetMany(x=>x.OrderDeltailId==orderDetail.Id)
                                                   where attrName.OrderDeltailId == orderDetail.Id
                                                   select attrName.NameAtt).ToList()
                                   };
            ViewBag.OrderDetail = orderDetailModel;
            var getOrder = _order.GetAll().FirstOrDefault(x => x.Id == id);
            return View(getOrder);
        }

        /// <summary>
        /// Action Approve OrderDetail Method POST
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Approve(Order orders)
        {
            var order = _order.GetById(orders.Id);
            var userId = Session["User"] as User;
            if (order.Status == false)
            {
                var user = _user.GetBy(x => x.Id == order.UserId);
                
                var orderBy = _orderDetail.GetAll().Where(x => x.OrderId == order.Id);
                order.Status = true;
                order.HandelerId = userId.Email;
                if (_order.Update(order))
                {
                    Helper.SendMail(user.Email, "quangciucuca@gmail.com", "Anhquang1009", "QTShop_Approve Order", string.Format(@"
                    <h1> Your order has been approved</h1>
                    <p>Best regards </p>
                    "));
                    TempData["UpdateSuccess"] = "Update Success";
                    return RedirectToAction("Index");
                }
                TempData["UpdateFalse"] = "Update False!";
                return RedirectToAction("Index");
            }
            else
            {
                var user = _user.GetBy(x => x.Id == order.UserId);
                var orderBy = _orderDetail.GetAll().Where(x => x.OrderId == order.Id);
                order.Status = false;
                order.HandelerId = userId.Email;
                if (_order.Update(order))
                {
                    Helper.SendMail(user.Email, "quangciucuca@gmail.com", "Anhquang1009", "QTShop_Reject Order", string.Format(@"
                    <h1>Sorry. Your order has been declined</h1>
                    <p>Best regards </p>
                    "));
                    TempData["UpdateSuccess"] = "Update Success";
                    return RedirectToAction("Index");
                }
                TempData["UpdateFalse"] = "Update False!";
                return RedirectToAction("Index");
            }
        }
    }
}

