using ProjectQT.BAL.Repositories;
using ProjectQT.DataModel.Models;
using ProjectQT.ViewModel.DashboardModels;
using ProjectQT.ViewModel.ReportModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectQT.Web.Areas.Admin.Controllers
{
    public class ReportController : Controller
    {
        GenericRepository<OrderDetail> _orderDetail;
        GenericRepository<Order> _order;
        GenericRepository<Product> _product;
        GenericRepository<User> _User;
        public ReportController()
        {
            _orderDetail = new GenericRepository<OrderDetail>();
            _order = new GenericRepository<Order>();
            _product = new GenericRepository<Product>();
            _User = new GenericRepository<User>();
        }
        public IEnumerable<OrderReport> GetAuctionReports(FilterDateModel dateModel)
        {
            var listOrderDetail = from orderDetail in _orderDetail.GetAll()
                                  join order in _order.GetAll()
                                  on orderDetail.OrderId equals order.Id
                                  join product in _product.GetAll()
                                  on orderDetail.ProductId equals product.Id
                                  join user in _User.GetAll()
                                  on orderDetail.CreateBy equals user.Id
                                  where order.Status == true
                                  select new OrderReport
                                  {
                                      Id = orderDetail.Id,
                                      CreateAt = orderDetail.CreateAt,
                                      CreateBy = user.Email,
                                      OrderId = orderDetail.OrderId,
                                      ProductId = product.Name,
                                      Quantity = orderDetail.Quantity,
                                      Status = orderDetail.Status,
                                      Price = orderDetail.Price
                                  };
            if (!string.IsNullOrEmpty(dateModel.NameProduct))
            {
                listOrderDetail = listOrderDetail.Where(s => s.ProductId.ToLower().Contains(dateModel.NameProduct.ToLower()));
            }
            var date = Convert.ToDateTime(dateModel.EndDate);
            if (date != DateTime.MinValue)
            {
                listOrderDetail = listOrderDetail.Where(d => d.CreateAt.Date == date);
            }
            return listOrderDetail;
        }

        // GET: Admin/Report
        public ActionResult Index(FilterDateModel dateModel)
        {
            var orderReport = GetAuctionReports(dateModel).ToList();
            ViewBag.xLabels = GetAuctionReports(dateModel).Select(x => x.ProductId).ToList();
            ViewBag.yValues = GetAuctionReports(dateModel).Select(x => x.Price).ToList();

            return View(orderReport);
        }

        //[HttpPost]
        //public ActionResult FilterReport(FilterDateModel dateModel)
        //{
        //    var orderReport = GetAuctionReports(dateModel).ToList();
        //    ViewBag.xLabels = GetAuctionReports(dateModel).Select(x => x.ProductId).ToList();
        //    ViewBag.yValues = GetAuctionReports(dateModel).Select(x => x.Price).ToList();
        //    return PartialView("~/Admin/View/Report/_ReportAuction", orderReport);
        //}
    }
}