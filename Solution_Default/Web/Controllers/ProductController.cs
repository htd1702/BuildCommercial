using Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class ProductController : Controller
    {
        private IProductService _productService;

        //contructor
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult LoadListProduct(string categories, string sortBy, string sortPrice, string sortColor, int pageSize)
        {
            try
            {
                List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
                //check params
                if (categories == "0" || categories == null)
                    categories = "%";
                if (sortBy == "0" || sortBy == null)
                    sortBy = "%";
                if (sortColor == "0" || sortColor == null)
                    sortColor = "%";
                DataTable dt = _productService.ListProduct(categories, sortBy, sortPrice, sortColor);
                if (dt.Rows.Count > 0)
                {
                    //Get data by take
                    var model = dt.AsEnumerable().OrderBy(p => p.Field<int>("ID")).Take(pageSize).CopyToDataTable();
                    list = _productService.GetTableRows(model);
                }
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}