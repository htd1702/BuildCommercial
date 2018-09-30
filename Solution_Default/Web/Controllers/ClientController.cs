using AutoMapper;
using Model.Model;
using Service;
using System.Collections.Generic;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class ClientController : Controller
    {
        private IProductCategoryService _productCategoryService;

        public ClientController(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
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
        public ActionResult Banner()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult Cart()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult Details()
        {
            return PartialView();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult News()
        {
            return View();
        }

        public ActionResult ShoppingCart()
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