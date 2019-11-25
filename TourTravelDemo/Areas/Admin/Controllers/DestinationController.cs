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
    public class DestinationController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Destination
        public ActionResult Index()
        {
            var destination = db.Destinations.OrderByDescending(x => x.CreateDate).ToList();
            return View(destination);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Title,Url,FormatUrl,SortOrder,Publish,CreateDate,ModifiedDate,Status")]Destination destination,HttpPostedFileBase fileUpload)
        {
            destination.Id = (db.Destinations.Max(x => (int?)x.Id) ?? 0) + 1;
            destination.Url = Common.TiegVietKhongDauUrl(Common.NameStandard(destination.Title));
            destination.CreateDate = DateTime.Now;
            destination.ModifiedDate = DateTime.Now;

            if (fileUpload != null)
            {
                var fileName = Common.UpLoadImg(fileUpload, 1000000, destination.Title);
                destination.Avatar = fileName;
            }

            db.Destinations.Add(destination);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var destinations = db.Destinations.SingleOrDefault(q => q.Id == id);
            if (destinations == null) return HttpNotFound();

            return View(destinations);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Avatar,Url,SortOrder,Publish,CreateDate,ModifiedDate,Status")]Destination destination, HttpPostedFileBase fileUpload)
        {
            if (fileUpload != null)
            {
                if (System.IO.File.Exists(Server.MapPath("~/Images/uploads/" + destination.Avatar)))
                {
                    System.IO.File.Delete(Server.MapPath("~/Images/uploads/" + destination.Avatar));
                }
                var fileName = Common.UpLoadImg(fileUpload, 1000000, destination.Title);
                destination.Avatar = fileName;

            }
            destination.ModifiedDate = DateTime.Now;
            db.Entry(destination).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var destinations = db.Destinations.SingleOrDefault(x => x.Id == id);
            if (destinations == null) return HttpNotFound();

            return View(destinations);
        }

        public ActionResult DeleteConfirm(int id)
        {
            var destination = db.Destinations.Find(id);
            if (System.IO.File.Exists(Server.MapPath("~/Images/uploads/" + destination.Avatar)))
            {
                System.IO.File.Delete(Server.MapPath("~/Images/uploads/" + destination.Avatar));
            }
            db.Destinations.Remove(destination);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}