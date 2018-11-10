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
        DBContext db = new DBContext();

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
            var model = db.PostCategorys.OrderByDescending(c => c.Name).ToList();
            return PartialView(model);
        }

        [HttpGet]
        public JsonResult GetListPostCateogy()
        {
            var list = _postCategoryService.GetAll();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}