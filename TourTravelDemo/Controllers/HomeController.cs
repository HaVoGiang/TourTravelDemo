using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TourTravelDemo.Models;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using BinhBaDemo.CodeHelpers;
using System.Windows.Forms;

namespace TourTravelDemo.Controllers
{
    
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        [Route("~/")]
        public ActionResult Index()
        {

            var destination = db.Destinations.Where(x => x.Status == 1 && x.Publish == true).OrderByDescending(x => x.CreateDate).Take(4).ToList();
            ViewBag.Destination = destination;
            var tours = db.Tours.OrderByDescending(x => x.CreateDate).ToList();
            ViewBag.Tour = tours;

            var hotTour = db.Tours.Where(x => x.Status == 1).OrderByDescending(x => x.CreateDate).Take(4).ToList();
            ViewBag.HotTour = hotTour;



            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [Route("~/lien-he")]
        public ActionResult Contact()
        {
            return View();
        }

        [Route("~/lien-he")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Contact([Bind(Include ="Name,Email,Phone,Message")]Contact contact)
        {
            contact.CreateDate = DateTime.Now;
            db.Contacts.Add(contact);
            db.SaveChanges();

            if (ModelState.IsValid)
            {
                var body = Common.ReadFileText(Server.MapPath("~/FormContact/form-mail.html"));
                string content = body;
                content = content.Replace("{{name}}", contact.Name);
                content = content.Replace("{{email}}", contact.Email);
                content = content.Replace("{{phone}}", contact.Phone);
                content = content.Replace("{{messenger}}", contact.Message);
                var message = new MailMessage();
                message.To.Add(new MailAddress("vogiangha1997@gmail.com"));  // replace with valid value 
                message.From = new MailAddress("vogiangha97@gmail.com");  // replace with valid value
                message.Subject = "Contact Form Email";
                message.Body = content;
                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = "vogiangha97@gmail.com",  // replace with valid value
                        Password = "ha0905418567"  // replace with valid value
                    };
                    smtp.Credentials = credential;
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(message);
                    //return RedirectToAction("Sent");
                }
            }

            return RedirectToAction("Index");
        }
        public PartialViewResult AvatarTourPartial(int tourId)
        {
            var avatar = db.Photos.SingleOrDefault(x => x.TourId == tourId && x.IsAvatar);
            if(avatar == null)
            {
                var avatar2 = db.Photos.SingleOrDefault(x => x.TourId == tourId);
                return PartialView("_PartialAvatarTour", avatar2);
            }
            return PartialView("_PartialAvatarTour",avatar);
        }

        [Route("~/admin")]
        public ActionResult Admin()
        {
            return RedirectToAction("Index", "Home", new { Area = "Admin" });
        }
    }
}