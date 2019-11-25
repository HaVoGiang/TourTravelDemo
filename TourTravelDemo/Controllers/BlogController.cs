using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TourTravelDemo.Models;
using PagedList;

namespace TourTravelDemo.Controllers
{
    public class BlogController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Blog
        [Route("~/blog")]
        public ActionResult Index(int? page)
        {
            int pageNum = page ?? 1;
            var blogs = db.Blogs.Where(x => x.Publish).OrderByDescending(x => x.CreateDate).ToPagedList(pageNum,5);
            return View(blogs);
        }

        [Route("~/blog/{slug}")]
        public ActionResult DetailBlog(string slug)
        {
            var blog = db.Blogs.SingleOrDefault(x => x.Url == slug);
            if (blog == null) return HttpNotFound();
            ViewBag.ListBlog = db.Blogs.Where(x => x.CategoryBlogId == blog.CategoryBlogId && x.Publish).OrderByDescending(x => x.CreateDate).ToList();

            var blogNext = db.Blogs.FirstOrDefault(x => x.CreateDate > blog.CreateDate);
            if (blogNext == null) blogNext = blog;
            ViewBag.BlogNextSlug = blogNext.Url;

            var blogLast = db.Blogs.OrderByDescending(x => x.CreateDate).FirstOrDefault(x => x.CreateDate < blog.CreateDate);
            if (blogLast == null) blogLast = blog;
            ViewBag.BlogLastSlug = blogLast.Url;

            return View(blog);
        }
    }
}