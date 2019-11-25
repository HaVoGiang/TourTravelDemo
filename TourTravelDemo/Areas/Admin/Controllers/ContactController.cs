using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TourTravelDemo.Models;

namespace TourTravelDemo.Areas.Admin.Controllers
{
    [Authorize]
    public class ContactController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Contact
        public ActionResult Index()
        {
            var contacts = db.Contacts.OrderByDescending(x => x.CreateDate).ToList();
            return View(contacts);
        }

        public ActionResult Detail(int id)
        {
            var contact = db.Contacts.SingleOrDefault(x => x.Id == id);
            if (contact == null) return HttpNotFound();
            return View(contact);
        }

        public ActionResult Delete(int id)
        {
            var contact = db.Contacts.SingleOrDefault(x => x.Id == id);
            if (contact == null) return HttpNotFound();
            return View(contact);
        }

        public ActionResult DeleteConfirm(int id)
        {
            var contact = db.Contacts.Find(id);
            db.Contacts.Remove(contact);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}