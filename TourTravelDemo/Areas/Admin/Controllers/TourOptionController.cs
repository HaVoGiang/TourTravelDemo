using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TourTravelDemo.Models;

namespace TourTravelDemo.Areas.Admin.Controllers
{
    [Authorize]
    public class TourOptionController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/TourOption
        public ActionResult Index()
        {
            var tourOption = db.TourOptions.OrderBy(x => x.SortOrder).ToList();

            return View(tourOption);
        }
        
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include ="Title,Icon,SortOrder,Publish")]TourOption tourOption)
        {
            db.TourOptions.Add(tourOption);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var tourOption = db.TourOptions.SingleOrDefault(x => x.Id == id);
            if (tourOption == null) return HttpNotFound();
            return View(tourOption);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include ="Id,Title,Icon,SortOrder,Publish")]TourOption tourOption)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tourOption).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var tourOption = db.TourOptions.SingleOrDefault(x => x.Id == id);
            if (tourOption == null) return HttpNotFound();
            return View(tourOption);
        }

        public ActionResult DeleteConfirm(int id)
        {
            var tourOption = db.TourOptions.Find(id);
            db.TourOptions.Remove(tourOption);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}