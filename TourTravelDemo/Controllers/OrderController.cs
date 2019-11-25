using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TourTravelDemo.Models;

namespace TourTravelDemo.Controllers
{
    public class OrderController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Order
        [Route("~/booking")]
        public ActionResult Index()
        {
            int id = int.Parse(Session["TourId"].ToString());
            var tour = db.Tours.SingleOrDefault(x => x.Id == id);
            if (tour == null) return HttpNotFound();
            ViewBag.Tour = tour;
            var tourOptionRelate = db.TourOptionRelates.Where(x => x.TourId == id).ToList();
            ViewBag.TourOption = tourOptionRelate;
            return View();
        }

        [HttpPost]
        public ActionResult BookingDetail(int id, FormCollection f)
        {
            Session["StartDay"] = DateTime.Parse(f["StartDay"]).ToString("dd/MM/yyyy");
            Session["Adults"] = int.Parse(f["Adults"]);
            Session["Kids"] = int.Parse(f["Kids"]);
            Session["TourId"] = id;
            return RedirectToAction("Index");
        }

        [Route("~/booking/thong-tin-nguoi-dung")]
        public ActionResult PersonalInfo()
        {

            return View();
        }

        [Route("~/booking/thong-tin-nguoi-dung")]
        [HttpPost]
        public ActionResult PersonalInfo(FormCollection f)
        {
            Session["Name"] = f["firstName"] + " " + f["lastName"];
            Session["Email"] = f["email"];
            Session["CountryCode"] = f["countryCode"];
            Session["Phone"] = f["phone"];

            return RedirectToAction("PaymentInfo");
        }

        [Route("~/booking/thong-tin-thanh-toan")]
        public ActionResult PaymentInfo()
        {
            int id = int.Parse(Session["TourId"].ToString());
            var tour = db.Tours.SingleOrDefault(x => x.Id == id);
            if (tour == null) return HttpNotFound();

            return View(tour);
        }

        [Route("~/booking/xac-thuc-thong-tin-thanh-toan")]
        public ActionResult PaymentInfoConfirm()
        {
            var customer = new Customer();
            customer.Name = Session["Name"].ToString();
            customer.Phone = Session["Phone"].ToString();
            customer.Email = Session["Email"].ToString();
            customer.CountryCode = Session["CountryCode"].ToString();
            customer.CreateDate = DateTime.Now;
            db.Customers.Add(customer);
            db.SaveChanges();

            var customerId = db.Customers.Max(x => x.Id);
            var order = new Order();
            order.CustomerId = customerId;
            order.TourId = int.Parse(Session["TourId"].ToString());
            order.Adults = int.Parse(Session["Adults"].ToString());
            order.Kids = int.Parse(Session["Kids"].ToString());
            order.StartDate = DateTime.Parse(Session["StartDay"].ToString());
            order.CreateDate = DateTime.Now;

            int tourId = int.Parse(Session["TourId"].ToString());
            var tour = db.Tours.SingleOrDefault(x => x.Id == tourId);

            order.Price = tour.Price * (int.Parse(Session["Kids"].ToString()) + int.Parse(Session["Adults"].ToString()));
            order.TransNo = Session["CountryCode"].ToString();
            order.OrderNumber = (int.Parse(db.Orders.Max(x => x.OrderNumber)) + 1).ToString();
            Session["OrderNumber"] = order.OrderNumber;
            db.Orders.Add(order);
            db.SaveChanges();

            return RedirectToAction("Confirmation");
        }
        [Route("~/booking/thanh-toan-thanh-cong")]
        public ActionResult Confirmation()
        {
            int id = int.Parse(Session["TourId"].ToString());
            var tour = db.Tours.SingleOrDefault(x => x.Id == id);
            if (tour == null) return HttpNotFound();
            ViewBag.Tour = tour;

            return View();
        }
        [Route("~/booking/ve-trang-chu")]
        public ActionResult GoIndex()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }


    }
}