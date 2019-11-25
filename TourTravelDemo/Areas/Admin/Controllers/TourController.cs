using BinhBaDemo.CodeHelpers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TourTravelDemo.Models;

namespace TourTravelDemo.Areas.Admin.Controllers
{
    [Authorize]
    public class TourController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Tour
        public ActionResult Index()
        {
            var tours = db.Tours.OrderByDescending(x => x.CreateDate).ToList();
            return View(tours);
        }

        public ActionResult Create()
        {
            var categorys = db.Category.Where(x => x.Publish == true).OrderByDescending(x => x.CreateDate).ToList();
            ViewBag.CategoryId = new SelectList(categorys,"Id","Title"); 
            var destinations = db.Destinations.Where(x => x.Publish == true).OrderByDescending(x => x.CreateDate).ToList();
            ViewBag.DestinationId = new SelectList(destinations,"Id","Title");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include ="CategoryId,DestinationId,Title,Price,DaysTour,Description,Content,Url,Publish,SortOrder,Status")]Tour tour)
        {
            tour.Url = Common.TiegVietKhongDauUrl(Common.NameStandard(tour.Title));
            tour.CreateDate = DateTime.Now;
            tour.ModifiedDate = DateTime.Now;
            db.Tours.Add(tour);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var tour = db.Tours.SingleOrDefault(x => x.Id == id);
            if (tour == null) return HttpNotFound();
            var categorys = db.Category.Where(x => x.Publish == true).OrderByDescending(x => x.CreateDate).ToList();
            ViewBag.CategoryId = new SelectList(categorys, "Id", "Title");
            var destinations = db.Destinations.Where(x => x.Publish == true).OrderByDescending(x => x.CreateDate).ToList();
            ViewBag.DestinationId = new SelectList(destinations, "Id", "Title");
            var tourOptions = db.TourOptions.Where(x => x.Publish == true).OrderBy(x => x.SortOrder).ToList();
            ViewBag.TourOption = tourOptions;
            var tOptionRelate = db.TourOptionRelates.Where(x => x.TourId == id).ToList();
            ViewBag.TourOptionRelate = tOptionRelate;
            return View(tour);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "Id,CategoryId,DestinationId,Title,Price,DaysTour,Description,Content,Url,Publish,SortOrder,CreateDate,Status")]Tour tour, FormCollection f)
        {
            tour.ModifiedDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                tour.Url = Common.TiegVietKhongDauUrl(Common.NameStandard(tour.Title));
                db.Entry(tour).State = EntityState.Modified;
                db.SaveChanges();

                var tourOption = db.TourOptions.Where(x => x.Publish == true).OrderBy(x => x.SortOrder).ToList();
                foreach (var item in tourOption)
                {
                    if (f["tOptions" + item.Id] != "false")
                    {
                        var tOptionRelate = db.TourOptionRelates.Where(x => x.TourId == tour.Id && x.TourOptionId == item.Id).ToList() ;
                        if (!tOptionRelate.Any())
                        {
                            var relate = new TourOptionRelate();
                            relate.TourId = tour.Id;
                            relate.TourOptionId = item.Id;

                            db.TourOptionRelates.Add(relate);
                            db.SaveChanges();
                        }
                        
                    }
                }
                
            }

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var tour = db.Tours.SingleOrDefault(x => x.Id == id);
            if (tour == null) return HttpNotFound();

            return View(tour);
        }

        public ActionResult DeleteConfirm(int id)
        {
            var tour = db.Tours.Find(id);
            db.Tours.Remove(tour);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}