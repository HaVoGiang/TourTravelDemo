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
    public class CategoryBlogController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/CategoryTour
        public ActionResult Index()
        {
            var category = db.CategoryBlogs.OrderByDescending(x => x.CreateDate).ToList();
            return View(category);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Title,FormatUrl,SortOrder,Publish")]CategoryBlog category)
        {
            category.Id = (db.CategoryBlogs.Max(x => (int?)x.Id) ?? 0) + 1;

            category.Url = Common.TiegVietKhongDauUrl(Common.NameStandard(category.Title));
            category.CreateDate = DateTime.Now;
            category.ModifiedDate = DateTime.Now;


            db.CategoryBlogs.Add(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var category = db.CategoryBlogs.SingleOrDefault(x => x.Id == id);
            if (category == null) return HttpNotFound();
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Url,FormatUrl,SortOrder,Publish,CreateDate,ModifiedDate")]CategoryBlog category)
        {
            category.ModifiedDate = DateTime.Now;
            db.Entry(category).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var category = db.CategoryBlogs.SingleOrDefault(x => x.Id == id);
            if (category == null) return HttpNotFound();

            return View(category);
        }

        public ActionResult DeleteConfirm(int id)
        {
            var category = db.CategoryBlogs.Find(id);

            db.CategoryBlogs.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}