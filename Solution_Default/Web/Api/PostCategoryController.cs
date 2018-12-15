using AutoMapper;
using Model.Model;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;
using Web.Infrastructure.Core;
using Web.Infrastructure.Extensions;
using Web.Models;

namespace Web.Api
{
    [RoutePrefix("api/postcategory")]
    [Authorize]
    public class PostCategoryController : ApiControllerBase
    {
        private IPostCategoryService _postCategoryService;

        #region Initialize

        //khai bao contructor
        public PostCategoryController(IErrorService errorService, IPostCategoryService postCategoryService) : base(errorService)
        {
            this._postCategoryService = postCategoryService;
        }

        #endregion Initialize

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyword, int page, int pageSize)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var model = _postCategoryService.GetAll(keyword);
                //count model
                totalRow = model.Count();
                //sap xep giam dan
                var query = model.OrderByDescending(x => x.CreatedDate).Skip(page * pageSize).Take(pageSize);
                //mapp data
                var responseData = Mapper.Map<IEnumerable<PostCategory>, IEnumerable<PostCategoryViewModel>>(query);
                //create pageination and set value
                var paginationSet = new PaginationSet<PostCategoryViewModel>()
                {
                    //item = data response
                    Items = responseData,
                    //current page
                    Page = page,
                    //count page
                    TotalCount = totalRow,
                    //làm tròn
                    TotalPages = (int)(Math.Ceiling((decimal)totalRow / pageSize))
                };
                //check status
                var response = request.CreateResponse(HttpStatusCode.OK, paginationSet);
                return response;
            });
        }

        [Route("getallparents")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _postCategoryService.GetAll();
                //mapp data
                var responseData = Mapper.Map<IEnumerable<PostCategory>, IEnumerable<PostCategoryViewModel>>(model);
                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }

        [Route("getallparentbytype")]
        [HttpGet]
        public HttpResponseMessage GetAllByType(HttpRequestMessage request, int type)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _postCategoryService.GetCategoriyByType(type);
                //mapp data
                var responseData = Mapper.Map<IEnumerable<PostCategory>, IEnumerable<PostCategoryViewModel>>(model);
                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }

        [Route("getid/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetId(HttpRequestMessage request, int id)
        {
            if (id > 0)
            {
                return CreateHttpResponse(request, () =>
                {
                    var model = _postCategoryService.GetById(id);
                    //mapp data
                    var responseData = Mapper.Map<PostCategory, PostCategoryViewModel>(model);
                    //check status
                    var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                    //return status
                    return response;
                });
            }
            else
                return request.CreateResponse(HttpStatusCode.BadRequest);
        }

        [Route("getname")]
        [HttpGet]
        public HttpResponseMessage GetName(HttpRequestMessage request, string term)
        {
            if (!string.IsNullOrWhiteSpace(term))
            {
                return CreateHttpResponse(request, () =>
                {
                    var model = _postCategoryService.ListNamePostCategory(term);
                    //check status
                    var response = request.CreateResponse(HttpStatusCode.OK, model);
                    //return status
                    return response;
                });
            }
            else
                return request.CreateResponse(HttpStatusCode.BadRequest);
        }

        [Route("create")]
        [HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage Create(HttpRequestMessage request, PostCategoryViewModel postCategoryVM)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                //check issue
                if (!ModelState.IsValid)
                {
                    //get status
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    PostCategory newPostCategory = new PostCategory();
                    //Call method add product category in folder extensions
                    newPostCategory.UpdatePostCategory(postCategoryVM);
                    //Set date
                    newPostCategory.CreatedDate = DateTime.Now;
                    //Add data
                    _postCategoryService.Add(newPostCategory);
                    //Save change
                    _postCategoryService.Save();
                    //Mapping data to dataView
                    var responseData = Mapper.Map<PostCategory, PostCategoryViewModel>(newPostCategory);
                    //Check request
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }
                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        [AllowAnonymous]
        public HttpResponseMessage Update(HttpRequestMessage request, PostCategoryViewModel postCategoryVM)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                //check issue
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    PostCategory dbPostCategory = _postCategoryService.GetById(postCategoryVM.ID);
                    //Call method add product category in folder extensions
                    dbPostCategory.UpdatePostCategory(postCategoryVM);
                    //Set date
                    dbPostCategory.UpdatedDate = DateTime.Now;
                    //Add data
                    _postCategoryService.Update(dbPostCategory);
                    //Save change
                    _postCategoryService.Save();
                    //Mapping data to dataView
                    var responseData = Mapper.Map<PostCategory, PostCategoryViewModel>(dbPostCategory);
                    //Check request
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }
                return response;
            });
        }

        [Route("delete")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            if (id > 0)
            {
                return CreateHttpResponse(request, () =>
                {
                    HttpResponseMessage response = null;
                    //check issue
                    if (!ModelState.IsValid)
                    {
                        //get status
                        response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                    }
                    else
                    {
                        int result = 0;
                        if (_postCategoryService.CheckExistsPostCategory(id) != 1)
                        {
                            //Delete
                            var reponse = _postCategoryService.Delete(id);
                            //Save change
                            _postCategoryService.Save();
                            result = 1;
                        }
                        else
                            result = -1;
                        response = request.CreateResponse(HttpStatusCode.Created, result);
                    }
                    return response;
                });
            }
            else
                return request.CreateResponse(HttpStatusCode.BadRequest);
        }

        [Route("deletemulti")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string listId)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                //check issue
                if (!ModelState.IsValid)
                {
                    //get status
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    List<int> result = new List<int>();
                    var listPostCategory = new JavaScriptSerializer().Deserialize<List<int>>(listId);
                    foreach (var id in listPostCategory)
                    {
                        if (_postCategoryService.CheckExistsPostCategory(id) != 1)
                        {
                            _postCategoryService.Delete(id);
                            //Save change
                            _postCategoryService.Save();
                            result.Add(1);
                        }
                        else
                            result.Add(-1);
                    }
                    //Check request
                    response = request.CreateResponse(HttpStatusCode.OK, result);
                }
                return response;
            });
        }
    }
}