using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TourTravelDemo.Models;

namespace TourTravelDemo.Areas.Admin.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Customer
        public ActionResult Index()
        {
            var customers = db.Customers.OrderByDescending(x => x.CreateDate).ToList();
            return View(customers);
        }

        public ActionResult Detail(int id)
        {
            var customer = db.Customers.SingleOrDefault(x => x.Id == id);
            if (customer == null) return HttpNotFound();
            return View(customer);
        }
    }
}