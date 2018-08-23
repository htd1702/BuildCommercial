using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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