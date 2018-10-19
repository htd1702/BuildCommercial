using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class ProductCategoryController : Controller
    {
        private IProductCategoryService _productCategoryService;

        //contructor
        public ProductCategoryController(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }

        // GET: ProductCategory
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetCategoryByTake(int take)
        {
            try
            {
                var list = _productCategoryService.GetCategoryByTake(take);
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}