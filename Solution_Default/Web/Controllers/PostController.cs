using Data;
using Service;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class PostController : Controller
    {
        private IPostCategoryService _postCategoryService;
        private IPostService _postService;
        private DBContext db = new DBContext();

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

        public ActionResult _viewPosts()
        {
            ViewBag.items = db.Posts.OrderByDescending(p => p.CreatedDate).ToList();
            return PartialView();
        }

        public ActionResult GetListPostPaging(int PageNo = 0, int PageSize = 3)
        {
            ViewBag.items = db.Posts.Where(p => p.Status == true).OrderByDescending(p => p.CreatedDate).Skip(PageNo * PageSize).Take(PageSize).ToList();
            return PartialView("_viewPosts");
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

        public ActionResult GetPostByPostCategory(int id)
        {
            ViewBag.items = db.Posts.Where(p => p.CategoryID == id && p.Status == true).OrderByDescending(p => p.CreatedDate).Take(3).ToList();
            return View("Index");
        }

        public ActionResult GetPagePostCount(int PageSize = 3)
        {
            var pageCount = Math.Ceiling(1.0 * db.Posts.Count() / PageSize);
            return Json(pageCount, JsonRequestBehavior.AllowGet);
        }
    }
}