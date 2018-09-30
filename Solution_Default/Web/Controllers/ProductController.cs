using Newtonsoft.Json.Linq;
using Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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

        [HttpGet]
        public JsonResult LoadListProduct(string categories, string sortBy, string sortPrice, string sortColor)
        {
            try
            {
                //var resolveRequest = HttpContext.Request;
                //resolveRequest.InputStream.Seek(0, SeekOrigin.Begin);
                //var jsonString = new StreamReader(resolveRequest.InputStream).ReadToEnd();
                //dynamic obj = JValue.Parse(jsonString);
                if (categories == "1")
                    categories = "%";
                if (sortPrice == "0")
                    categories = "%";
                List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
                DataTable dt = _productService.ListProduct(categories, sortBy, sortPrice, sortColor);
                list = _productService.GetTableRows(dt);
                var jsonResult = Json(list, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = Int32.MaxValue;
                return jsonResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}