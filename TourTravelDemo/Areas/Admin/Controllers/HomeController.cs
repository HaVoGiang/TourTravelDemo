using System.Threading.Tasks;
using System.Web.Mvc;
using TourTravelDemo.Models;

namespace TourTravelDemo.Areas.Admin.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}