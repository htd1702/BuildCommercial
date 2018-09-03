using System.Web.Mvc;

namespace Web.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        public ActionResult productAddView()
        {
            return PartialView();
        }

        public ActionResult productEditView()
        {
            return PartialView();
        }

        public ActionResult productListView()
        {
            return PartialView();
        }
    }
}