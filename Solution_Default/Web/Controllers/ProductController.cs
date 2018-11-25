using AutoMapper;
using Data;
using Model.Model;
using Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Web.Models;

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

        public ActionResult SaleProduct()
        {
            return View();
        }

        public ActionResult NewProduct()
        {
            return View();
        }

        public ActionResult HotProduct()
        {
            var model = _productService.GetHotProduct(10);
            return PartialView(model);
        }

        public ActionResult Details(string id)
        {
            DBContext db = new DBContext();
            if (string.IsNullOrEmpty(id))
                id = "0";
            var model = _productService.GetById(int.Parse(id));
            var listProduct = Mapper.Map<Product, ProductViewModel>(model);
            var cookie = Request.Cookies["Views"];
            if (cookie == null)
            {
                cookie = new HttpCookie("Views");
            }
            cookie.Values[id.ToString()] = id.ToString();
            Response.Cookies.Add(cookie);
            var cookieId = cookie.Values.AllKeys.Select(k => int.Parse(k)).ToList();
            //get view
            ViewBag.Views = db.Products.Where(p => cookieId.Contains(p.ID));
            List<string> listImgs = new JavaScriptSerializer().Deserialize<List<string>>(listProduct.MoreImages);
            ViewBag.MoreImgs = listImgs;
            //get product by category
            DataTable dt = _productService.ListRelatedProduct(id);
            if (dt.Rows.Count > 0)
                ViewBag.ProductCagtegory = dt.AsEnumerable().OrderBy(p => p.Field<int>("ID")).Take(9).ToList();
            return View(listProduct);
        }

        [HttpPost]
        public JsonResult LoadListProduct(string categories, string sortBy, string sortPrice, string sortColor, int parentID, int pageSize)
        {
            try
            {
                DataTable dt = new DataTable();
                List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
                //check params
                if (categories == "0" || categories == null)
                    categories = "%";
                if (sortBy == "0" || sortBy == null)
                    sortBy = "%";
                if (sortColor == "0" || sortColor == null)
                    sortColor = "%";
                if (parentID > 0)
                    dt = _productService.ListProduct(parentID.ToString(), sortBy, sortPrice, sortColor, categories);
                else
                    dt = _productService.ListProduct(categories, sortBy, sortPrice, sortColor, "%");
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

        [HttpPost]
        public JsonResult LoadListProductByCategory(string categories, int parentID)
        {
            try
            {
                DataTable dt = new DataTable();
                List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
                if (categories == "0" || categories == null)
                    categories = "%";
                if (parentID > 0)
                    dt = _productService.ListProduct(parentID.ToString(), "%", "0", "%", categories);
                else
                    dt = _productService.ListProduct(categories, "%", "0", "%", "%");

                if (dt.Rows.Count > 0)
                {
                    //Get data by take
                    var model = dt.AsEnumerable().OrderBy(p => p.Field<int>("ID")).CopyToDataTable();
                    list = _productService.GetTableRows(model);
                }
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public JsonResult ListNameProduct(string term)
        {
            var model = _productService.ListNameProduct(term);
            return Json(new
            {
                data = model,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListHotProduct(int top)
        {
            var model = _productService.GetHotProduct(top);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SearchProduct(string keyword)
        {
            string key = keyword.ToLower().Replace(" ", "-");
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            DataTable dt = _productService.ListProductByKeyword(key);
            if (dt.Rows.Count > 0)
            {
                var model = dt.AsEnumerable().OrderBy(p => p.Field<int>("ID")).Take(8).CopyToDataTable();
                list = _productService.GetTableRows(model);
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListProductDiscount()
        {
            var model = _productService.ListProductDiscount();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListNewProduct()
        {
            var model = _productService.ListNewProduct();
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}