using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TourTravelDemo.Models;

namespace TourTravelDemo.Controllers
{
    public class TourController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Tour
        //[Route("~/{slugCategory}")]
        public ActionResult Index(string slugCategory)
        {
            var tour = db.Tours.Where(x => x.Category.Url == slugCategory && x.Publish).OrderByDescending(x => x.CreateDate).ToList();
            var categoryTour = db.Category.SingleOrDefault(x => x.Url == slugCategory);
            if (categoryTour == null) return HttpNotFound();
            ViewBag.CategoryTour = categoryTour;
            return View(tour);
        }

        //[Route("~/{slugCategory}/{slug}")]
        public ActionResult TourDetail(string slugCategory,string slug)
        {
            var tour = db.Tours.SingleOrDefault(x => x.Category.Url == slugCategory && x.Url == slug);
            if (tour == null) return HttpNotFound();

            var toursLienQuan = db.Tours.Where(x => x.Publish && x.DestinationId == tour.DestinationId && x.Url != slug).OrderByDescending(x => x.CreateDate).Take(2).ToList();
            ViewBag.TourLienQuan = toursLienQuan;

            var tourOption = db.TourOptionRelates.Where(x => x.TourId == tour.Id).ToList();
            ViewBag.TourOption = tourOption;

            var photo = db.Photos.Where(x => x.TourId == tour.Id && x.Publish).ToList();
            ViewBag.Photo = photo;

            return View(tour);
        }
    }
}