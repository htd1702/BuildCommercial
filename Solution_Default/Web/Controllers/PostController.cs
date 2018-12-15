using AutoMapper;
using Data;
using Model.Model;
using Service;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Web.Models;

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

        public ActionResult _viewPostDetails(int id)
        {
            var postID = db.Posts.FirstOrDefault(p => p.CategoryID == id).ID;
            var model = _postService.GetById(postID);
            var listPost = Mapper.Map<Post, PostViewModel>(model);
            List<string> listImgs = new JavaScriptSerializer().Deserialize<List<string>>(listPost.MoreImages);
            ViewBag.MoreImgs = listImgs;
            return View(listPost);
        }

        [HttpGet]
        public JsonResult ListNamePost(string term)
        {
            var model = _postService.ListNamePost(term);
            return Json(new
            {
                data = model,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SearchPost(string keyword)
        {
            int id = _postService.ListPostIDByName(keyword);
            return Json(id, JsonRequestBehavior.AllowGet);
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
    }
}