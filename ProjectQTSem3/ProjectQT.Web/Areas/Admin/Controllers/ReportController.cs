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
        public IEnumerable<OrderReport> GetOrderReports(FilterDateModel dateModel)
        {
            var listOrderDetail = from order in _order.GetAll()
                                  join orderDetail in _orderDetail.GetAll()
                                  on order.Id equals orderDetail.OrderId
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
                                      Price = orderDetail.Price,
                                      CountBuy = product.CountBuy,
                                      CountView = product.CountView
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
            //var product = _product.GetAll();
            //var orderReport = GetOrderReports(dateModel).ToList();
            //ViewBag.xLabels = product.Select(x => x.Name).ToList();
            //ViewBag.yValues = GetOrderReports(dateModel).Select(x => x.CountBuy).ToList(); 
            //ViewBag.y2Values = GetOrderReports(dateModel).Select(x => x.CountView).ToList();
            //return View(orderReport);
            return View();
        }

        [HttpPost]
        public ActionResult FilterReport(FilterDateModel dateModel)
        {
            var product = _product.GetAll();
            var orderReport = GetOrderReports(dateModel).ToList();
            ViewBag.xLabels = product.Select(x => x.Name).ToList();
            ViewBag.yValues = GetOrderReports(dateModel).Select(x => x.CountBuy).ToList();
            ViewBag.y2Values = GetOrderReports(dateModel).Select(x => x.CountView).ToList();
            return PartialView("_ReportAuction", orderReport);
        }
    }
}