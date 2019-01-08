using AutoMapper;
using Model.Model;
using Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Web.Models;

namespace Web.Controllers
{
    public class ProductController : Controller
    {
        private IProductService _productService;
        private IBannerService _bannerService;
        private IProductCategoryService _productCategoryService;
        private IProductDetailService _productDetailService;

        //contructor
        public ProductController(IProductService productService, IProductCategoryService productCategoryService, IProductDetailService productDetailService, IBannerService bannerService)
        {
            _productService = productService;
            _productCategoryService = productCategoryService;
            _productDetailService = productDetailService;
            _bannerService = bannerService;
        }

        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SaleProduct()
        {
            Session["ShoppingUrl"] = "/sale-product";
            ViewBag.BannerImage = _bannerService.ListBannerByType(2, 2);
            ViewBag.Categories = _productCategoryService.GetCategoriyByType(3).Where(p => p.Type == 1).ToList();
            return View();
        }

        public ActionResult NewProduct()
        {
            Session["ShoppingUrl"] = "/new-product";
            ViewBag.BannerImage = _bannerService.ListBannerByType(2, 3);
            ViewBag.Categories = _productCategoryService.GetCategoriyByType(3).Where(p => p.Type == 1).ToList();
            return View();
        }

        public ActionResult ProductByCategory(int id)
        {
            Session["ShoppingUrl"] = "/product-category/" + id.ToString();
            var category = _productCategoryService.GetById(id);
            ViewBag.Name = category.Name;
            ViewBag.NameFr = category.NameFr;
            ViewBag.NameVN = category.NameVN;
            ViewBag.ParentID = category.ParentID;
            ViewBag.ID = id;
            ViewBag.Categories = _productCategoryService.ListCategoryById(id);
            return View();
        }

        public ActionResult HotProductOverView()
        {
            ViewBag.ClothesProductHot = _productService.ListStoreOverview(2, 3).AsEnumerable().Take(10).ToList();
            ViewBag.CosmeticsProductHot = _productService.ListStoreOverview(2, 2).AsEnumerable().Take(10).ToList();
            ViewBag.InteriorProductHot = _productService.ListStoreOverview(2, 1).AsEnumerable().Take(10).ToList();
            return PartialView();
        }

        public ActionResult SaleProductOverView()
        {
            ViewBag.ClothesProductSale = _productService.ListStoreOverview(3, 3).AsEnumerable().Take(10).ToList();
            ViewBag.CosmeticsProductSale = _productService.ListStoreOverview(3, 2).AsEnumerable().Take(10).ToList();
            ViewBag.InteriorProductSale = _productService.ListStoreOverview(3, 1).AsEnumerable().Take(10).ToList();
            return PartialView();
        }

        public ActionResult Details(string id)
        {
            Session["ShoppingUrl"] = "/product-details/" + id;
            int viewNow = 0;
            if (string.IsNullOrEmpty(id))
                id = "0";
            var model = _productService.GetById(int.Parse(id));
            var listProduct = Mapper.Map<Product, ProductViewModel>(model);
            viewNow = int.Parse(listProduct.ViewCount.ToString()) + 1;
            //get view
            ViewBag.Views = EditView(int.Parse(id), viewNow);
            //get string img multi
            List<string> listImgs = new JavaScriptSerializer().Deserialize<List<string>>(listProduct.MoreImages);
            ViewBag.MoreImgs = listImgs;
            //get producT related
            DataTable dt = _productService.ListRelatedProduct(id);
            //Get name category
            var listCate = _productCategoryService.GetById(model.CategoryID);
            ViewBag.CategoryName = listCate.Name;
            ViewBag.CategoryNameVN = listCate.NameVN;
            ViewBag.CategoryNameFr = listCate.NameFr;
            //check list relate
            if (dt.Rows.Count > 0)
                ViewBag.ProductCagtegory = dt.AsEnumerable().OrderBy(p => p.Field<int>("ID")).Take(9).ToList();
            return View(listProduct);
        }

        public int EditView(int id, int view)
        {
            int result = 0;
            if (id != 0)
            {
                Product newProduct = new Product();
                ProductViewModel productVM = new ProductViewModel();
                newProduct = _productService.GetById(id);
                newProduct.ViewCount = view;
                _productService.Update(newProduct);
                _productService.Save();
                result = _productService.GetViewProduct(id);
            }
            return result;
        }

        [HttpPost]
        public JsonResult LoadListProduct(string colorID, string fromPrice, string toPrice, string categoryID)
        {
            try
            {
                if (colorID == "0")
                    colorID = "%";
                DataTable dt = new DataTable();
                List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
                dt = _productService.ListProduct(colorID, fromPrice, toPrice, categoryID);
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

        [HttpPost]
        public JsonResult LoadListProductByCategory(string categories, int parentID)
        {
            try
            {
                DataTable dt = new DataTable();
                List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
                if (categories == "0" || categories == null)
                    categories = "%";
                dt = _productService.LoadListProductByCategory(categories);
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
            DataTable dt = new DataTable();
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            dt = _productService.ListStoreOverview(4, 3);
            if (dt.Rows.Count > 0)
            {
                //Get data by take
                var model = dt.AsEnumerable().CopyToDataTable();
                list = _productService.GetTableRows(model);
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListStoreOverview(int type, int categoryType)
        {
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            DataTable dt = _productService.ListStoreOverview(type, categoryType);
            if (dt.Rows.Count > 0)
            {
                var model = dt.AsEnumerable().Take(10).CopyToDataTable();
                list = _productService.GetTableRows(model);
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CheckQuantityProduct(int colorID, int sizeID)
        {
            var model = _productService.CheckInventoryProduct(colorID, sizeID);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult InventoryByProductDetails(int colorID, int sizeID)
        {
            var model = _productService.InventoryByProductDetails(colorID, sizeID);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}