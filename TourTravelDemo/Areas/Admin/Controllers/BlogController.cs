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
    public class BlogController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Blog
        public ActionResult Index()
        {
            var blogs = db.Blogs.OrderByDescending(x => x.CreateDate).ToList();
            return View(blogs);
        }

        public ActionResult Create()
        {
            var categorys = db.CategoryBlogs.Where(x => x.Publish == true).OrderByDescending(x => x.CreateDate).ToList();
            ViewBag.CategoryBlogId = new SelectList(categorys, "Id", "Title");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryBlogId,Title,Photo,Description,Content,Url,Publish,SortOrder,Status")]Blog blog,HttpPostedFileBase fileUpload)
        {
            if (blog.Url == "") blog.Url = Common.TiegVietKhongDauUrl(Common.NameStandard(blog.Title));
            blog.CreateDate = DateTime.Now;
            blog.ModifiedDate = DateTime.Now;
            if(fileUpload != null)
            {
                var fileName = Common.UpLoadImg(fileUpload,1000000,Common.NameStandard(blog.Title));
                blog.Photo = fileName;
            }
            db.Blogs.Add(blog);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var blog = db.Blogs.SingleOrDefault(x => x.Id == id);
            if (blog == null) return HttpNotFound();
            var categorys = db.CategoryBlogs.Where(x => x.Publish == true).OrderByDescending(x => x.CreateDate).ToList();
            ViewBag.CategoryBlogId = new SelectList(categorys, "Id", "Title");

            return View(blog);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CategoryBlogId,Title,Photo,Description,Content,Url,Publish,SortOrder,Status,CreateDate")]Blog blog, HttpPostedFileBase fileUpload)
        {
            blog.ModifiedDate = DateTime.Now;

            if (ModelState.IsValid)
            {
                if (fileUpload != null)
                {
                    var fileName = Common.UpLoadImg(fileUpload, 1000000, Common.NameStandard(blog.Title));
                    blog.Photo = fileName;
                }
                db.Entry(blog).State = EntityState.Modified;
                db.SaveChanges();

            }

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var blog = db.Blogs.SingleOrDefault(x => x.Id == id);
            if (blog == null) return HttpNotFound();

            return View(blog);
        }

        public ActionResult DeleteConfirm(int id)
        {
            var blog = db.Blogs.Find(id);
            db.Blogs.Remove(blog);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}