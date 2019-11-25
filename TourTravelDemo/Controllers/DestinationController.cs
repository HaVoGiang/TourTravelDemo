using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TourTravelDemo.Models;
using PagedList;
namespace TourTravelDemo.Controllers
{
    public class DestinationController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Destination
        [Route("~/diem-den")]
        public ActionResult Index(int? page)
        {
            int pageNum = page ?? 1;
            var destination = db.Destinations.Where(x => x.Publish == true).OrderByDescending(x => x.CreateDate).ToPagedList(pageNum,9);

            var tours = db.Tours.Where(x => x.Publish == true).OrderByDescending(x => x.CreateDate).ToList();
            ViewBag.Tour = tours;
            return View(destination);
        }

        [Route("~/diem-den/{slug}")]
        public ActionResult DestinationTour(string slug)
        {
            
            var tours = db.Tours.Where(x => x.Destination.Url == slug).ToList();
            //var destination = db.Destinations.SingleOrDefault(x => x.Id == destinationId);
            //if (destination == null) return HttpNotFound();
            //ViewBag.Destination = destination;
            var destination = db.Destinations.FirstOrDefault(x => x.Url == slug);
            if (destination == null) return HttpNotFound();
            ViewBag.Destination = destination;
            return View(tours);
        }
    }
}