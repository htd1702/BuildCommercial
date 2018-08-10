using AutoMapper;
using Model.Model;
using Service;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Web.Infrastructure.Core;
using Web.Infrastructure.Extensions;
using Web.Models;

namespace Web.Api
{
    [RoutePrefix("api/postcategory")]
    public class PostCategoryController : ApiControllerBase
    {
        private IPostCategoryService _postCategoryService;

        //Page cần thiết 1 errorservice nên child cũng cần truyền
        public PostCategoryController(IErrorService errorService, IPostCategoryService postCategoryService) :
            base(errorService)
        {
            this._postCategoryService = postCategoryService;
        }

        [Route("getall")]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var listCategory = _postCategoryService.GetAll();
                //Mapp data
                var listCategoryVM = Mapper.Map<List<PostCategory>>(listCategory);
                //status http
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listCategory);
                return response;
            });
        }

        [Route("add")]
        public HttpResponseMessage Post(HttpRequestMessage request, PostCategoryViewModel postCategoryVM)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid)
                {
                    //check request http
                    request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    PostCategory postCategory = new PostCategory();
                    //call update method
                    postCategory.UpdatePostCategory(postCategoryVM);
                    //call add method
                    var category = _postCategoryService.Add(postCategory);
                    //save change
                    _postCategoryService.Save();
                    //status request
                    response = request.CreateResponse(HttpStatusCode.Created, category);
                }
                return response;
            });
        }

        [Route("update")]
        public HttpResponseMessage Put(HttpRequestMessage request, PostCategoryViewModel postCategoryVM)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid)
                {
                    request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    //get id postCategory
                    var postCategoryDB = _postCategoryService.GetId(postCategoryVM.ID);
                    //call update method
                    postCategoryDB.UpdatePostCategory(postCategoryVM);
                    _postCategoryService.Update(postCategoryDB);
                    //save change
                    _postCategoryService.Save();
                    //status request
                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                return response;
            });
        }

        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid)
                {
                    request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    _postCategoryService.Delete(id);
                    _postCategoryService.Save();

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                return response;
            });
        }
    }
}