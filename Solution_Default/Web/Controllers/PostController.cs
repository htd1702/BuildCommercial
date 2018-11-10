using Data;
using Service;
using System.Linq;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class PostController : Controller
    {
        private IPostCategoryService _postCategoryService;
        private IPostService _postService;
        DBContext db = new DBContext();

        //contructor
        public PostController(IPostCategoryService postCategoryService, IPostService postService)
        {
            _postCategoryService = postCategoryService;
            _postService = postService;
        }

        // GET: Post
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult _viewPost()
        {
            var model = db.Posts.OrderByDescending(p => p.CreatedDate).Take(4).ToList();
            return PartialView(model);
        }

        public ActionResult _viewPostDetails(int id)
        {
            var model = db.Posts.Find(id);
            return View(model);
        }

        [HttpGet]
        public JsonResult GetListPost()
        {
            var list = _postService.GetAll();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}