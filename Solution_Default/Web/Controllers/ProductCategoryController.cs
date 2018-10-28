using Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class ProductCategoryController : Controller
    {
        private IProductCategoryService _productCategoryService;
        private IProductService _productService;

        //contructor
        public ProductCategoryController(IProductCategoryService productCategoryService, IProductService productService)
        {
            _productCategoryService = productCategoryService;
            _productService = productService;
        }

        // GET: ProductCategory
        public ActionResult Index()
        {
            return View();
        }

        //List category
        public ActionResult _viewCategory()
        {
            DataTable dt = new DataTable();
            dt = _productCategoryService.GetCategoryByParent();
            return PartialView(dt);
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

        [HttpGet]
        public JsonResult GetCategoryByType(int type)
        {
            try
            {
                var list = _productCategoryService.GetCategoriyByType(type);
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}