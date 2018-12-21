using Service;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class ClientController : Controller
    {
        private IProductCategoryService _productCategoryService;
        private IProductService _productService;
        private IBannerService _bannerService;
        private IContactDetailService _contactDetailService;


        public ClientController(IProductCategoryService productCategoryService, IProductService productService, IBannerService bannerService, IContactDetailService contactDetailService)
        {
            _productCategoryService = productCategoryService;
            _productService = productService;
            _bannerService = bannerService;
            _contactDetailService = contactDetailService;
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
        public ActionResult Cart()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult _sectionSlider()
        {
            var model = _bannerService.ListBannerByType(1, 1);
            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult _viewFooter()
        {
            ViewBag.Parent = _productCategoryService.GetCategoriyByType(1);
            ViewBag.Category = _productCategoryService.GetCategoriyByType(2);
            ViewBag.ConAddress = _contactDetailService.GetDefaultContact().Address;
            ViewBag.ConNumber = _contactDetailService.GetDefaultContact().Phone;
            ViewBag.ConEmail = _contactDetailService.GetDefaultContact().Email;

            return PartialView();
        }

        public ActionResult _viewProduct()
        {
            return PartialView();
        }

        public ActionResult _viewCart()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult _viewHeader()
        {
            ViewBag.ParentCategory = _productCategoryService.GetCategoriyByType(1);
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult Main()
        {
            return PartialView();
        }
    }
}