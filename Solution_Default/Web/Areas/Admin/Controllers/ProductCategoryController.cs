using System.Web.Mvc;

namespace Web.Areas.Admin.Controllers
{
    public class ProductCategoryController : Controller
    {
        public ActionResult productCategoryAddView()
        {
            return PartialView();
        }

        public ActionResult productCategoryEditView()
        {
            return PartialView();
        }

        public ActionResult productCategoryListView()
        {
            return PartialView();
        }
    }
}