using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TourTravelDemo.Models;

namespace TourTravelDemo.Areas.Admin.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Order
        public ActionResult Index()
        {
            var orders = db.Orders.OrderByDescending(x => x.CreateDate).ToList();
            return View(orders);
        }

        public ActionResult Detail(int id)
        {
            var order = db.Orders.SingleOrDefault(x => x.Id == id);
            if (order == null) return HttpNotFound();

            return View(order);
        }
    }
}