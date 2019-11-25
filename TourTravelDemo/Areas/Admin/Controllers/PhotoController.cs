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
    public class PhotoController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Photo
        public ActionResult Index(int id)
        {
            var listTour = db.Tours.Where(x => x.Publish == true).OrderByDescending(x => x.CreateDate).ToList();
            ViewBag.ListTour = new SelectList(listTour,"Id","Title");
            ViewBag.Id = id;
            var photos = db.Photos.Where(x => x.Publish == true && x.TourId == id).OrderByDescending(x => x.SortOrder).ToList();
            return View(photos);
        }

        [HttpPost]
        public ActionResult Upload(int id,HttpPostedFileBase[] fileUpload)
        {
            var tour = db.Tours.SingleOrDefault(x => x.Id == id);

            if (fileUpload != null)
            {
                foreach (var file in fileUpload)
                {
                    var filename = Common.UpLoadImg(file, 1000000, Common.NameStandard(tour.Title));
                    var photo = new Photo();
                    photo.PathPhoto = filename;
                    photo.Publish = true;
                    photo.TourId = id;
                    photo.IsAvatar = false;
                    photo.SortOrder = (db.Photos.Max(x => (int?)x.SortOrder) ?? 0) + 1;

                    db.Photos.Add(photo);
                    db.SaveChanges();
                }
                
            }

            return RedirectToAction("Index","Photo",new { id });
        }

        public ActionResult Edit(int id)
        {
            var photo = db.Photos.SingleOrDefault(x => x.Id == id);
            if (photo == null) return HttpNotFound();
            return View(photo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include ="Id,TourId,PathPhoto,SortOrder,Publish,IsAvatar")]Photo photo)
        {
            if (ModelState.IsValid)
            {
                
                db.Entry(photo).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index","Photo",new { id = photo.TourId });
        }

        public ActionResult Delete(int id)
        {
            var photo = db.Photos.SingleOrDefault(x => x.Id == id);
            if (photo == null) return HttpNotFound();
            return View(photo);
        }

        public ActionResult DeleteConfirm(int id)
        {
            var photo = db.Photos.Find(id);
            if (photo.IsAvatar) //Check IsAvatar
            {
                //Do Something ...
                return RedirectToAction("Index",new { id = photo.TourId});
            }
            db.Photos.Remove(photo);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = photo.TourId });
        }
    }
}