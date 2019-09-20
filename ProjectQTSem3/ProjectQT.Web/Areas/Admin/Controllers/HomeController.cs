using ProjectQT.BAL.Repositories;
using ProjectQT.DataModel.Models;
using ProjectQT.ViewModel.DashboardModels;
using ProjectQT.ViewModel.OrderModel;
using ProjectQT.ViewModel.ReportModel;
using ProjectQT.Web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

namespace ProjectQT.Web.Areas.Admin.Controllers
{
    [CustomizeAuth]
    public class HomeController : Controller
    {
        GenericRepository<RevenueModel> _revenueModel;
        GenericRepository<OrderDetail> _orderDetail;
        GenericRepository<Order> _order;
        GenericRepository<Product> _product;
        GenericRepository<User> _User;

        public HomeController()
        {
            _revenueModel = new GenericRepository<RevenueModel>();
            _orderDetail = new GenericRepository<OrderDetail>();
            _order = new GenericRepository<Order>();
            _product = new GenericRepository<Product>();
            _User = new GenericRepository<User>();
        }


        public IEnumerable<RevenueModel> GetRevenue()
        {
            var revenueList = new List<RevenueModel>();
            var listOrderDetail = from orderDetail in _orderDetail.GetAll()
                                  join order in _order.GetAll()
                                  on orderDetail.OrderId equals order.Id
                                  where order.Status == true
                                  select new OrderDetailModel
                                  {
                                      Id = orderDetail.Id,
                                      CreateAt = orderDetail.CreateAt,
                                      CreateBy = orderDetail.CreateBy.ToString(),
                                      OrderId = orderDetail.OrderId,
                                      ProductId = orderDetail.ProductId.ToString(),
                                      Quantity = orderDetail.Quantity,
                                      Status = orderDetail.Status,
                                      Price = orderDetail.Price
                                  };
            var cont = listOrderDetail.Count();
            foreach (var item in listOrderDetail)
            {
                if (revenueList.Any(x => x.Date.Date == item.CreateAt.Date))
                {
                    var revenue = revenueList.Find(x => x.Date.Date == item.CreateAt.Date);
                    revenue.Money += item.Price;
                }
                else
                {
                    var reveneList = new RevenueModel { Date = item.CreateAt, Money = item.Price };
                    revenueList.Add(reveneList);
                }
            }
            return revenueList.OrderBy(x => x.Date).ToList();
        }

        [HttpGet]
        [CustomizeAuth(Roles = "Detail")]
        public ActionResult Index()
        {
            var listOrderDetail = GetRevenue();
            var listLabels = listOrderDetail.Select(item => item.Date.ToLongDateString().ToString()).ToArray();
            ViewBag.ListLabel = listLabels;

            var listData = listOrderDetail.Select(item => item.Money).ToArray();
            ViewBag.ListData = listData;
            var product = _product.GetAll();
            ViewBag.xLabels = product.Select(x => x.Name).ToList();
            ViewBag.yValues = product.Select(x => x.CountBuy).ToList();
            ViewBag.y2Values = product.Select(x => x.CountView).ToList();
            return View();
        }

    }
}
