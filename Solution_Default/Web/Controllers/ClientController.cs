using AutoMapper;
using Data;
using Model.Model;
using Service;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Web.Models;

namespace Web.Controllers
{
    public class ClientController : Controller
    {
        private IProductCategoryService _productCategoryService;
        private IProductService _productService;
        private DBContext db = new DBContext();

        public ClientController(IProductCategoryService productCategoryService, IProductService productService)
        {
            _productCategoryService = productCategoryService;
            _productService = productService;
        }

        // GET: Client
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult Header()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult Slider()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult Cart()
        {
            return PartialView();
        }

        public ActionResult ProductCategoryDetail(string id)
        {
            if (string.IsNullOrEmpty(id))
                id = "0";
            var model = _productService.ListProductByCategory(int.Parse(id));
            var listProduct = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(model);
            return View(listProduct);
        }

        public ActionResult Details(string id)
        {
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
            ViewBag.Views = db.Products.Where(p => cookieId.Contains(p.ID));
            List<string> listImgs = new JavaScriptSerializer().Deserialize<List<string>>(listProduct.MoreImages);
            ViewBag.MoreImgs = listImgs;
            return View(listProduct);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult News()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult _viewFooter()
        {
            var model = _productCategoryService.GetAll();
            var listCategory = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(model);
            return PartialView(listCategory);
        }

        [ChildActionOnly]
        public ActionResult Main()
        {
            return PartialView();
        }
    }
}