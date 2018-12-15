using Data;
using Service;
using System.Linq;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class PostCategoryController : Controller
    {
        private IPostCategoryService _postCategoryService;
        private IPostService _postService;
        private DBContext db = new DBContext();

        //contructor
        public PostCategoryController(IPostCategoryService postCategoryService, IPostService postService)
        {
            _postCategoryService = postCategoryService;
            _postService = postService;
        }

        // GET: PostCategory
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult _viewPostCategory()
        {
            ViewBag.PostNews = db.PostCategorys.Where(p => p.Status == true && p.HomeFlag == true && p.Promotion != true).OrderByDescending(p => p.CreatedDate).ToList();
            ViewBag.PostPromotion = db.PostCategorys.Where(p => p.Status == true && p.HomeFlag == true && p.Promotion == true).OrderByDescending(p => p.CreatedDate).ToList();
            return View();
        }

        [HttpGet]
        public JsonResult GetListPostCateogy()
        {
            var list = _postCategoryService.GetAll();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}