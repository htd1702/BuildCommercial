using System.Web.Mvc;

namespace Web.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        // GET: Admin/Account
        public ActionResult Index()
        {
            return View();
        }

        // GET: Admin/Account
        public ActionResult Login()
        {
            return PartialView();
        }
    }
}