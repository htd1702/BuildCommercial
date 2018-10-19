using AutoMapper;
using Model.Model;
using Service;
using System;
using System.Collections.Generic;
using System.Data;
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
    [RoutePrefix("api/list")]
    [Authorize]
    public class ListController : ApiControllerBase
    {
        private IListService _listService;
        private IProductService _productService;

        #region Initialize

        //khai bao contructor
        public ListController(IErrorService errorService, IListService listService, IProductService productService)
            : base(errorService)
        {
            this._listService = listService;
            this._productService = productService;
        }

        #endregion Initialize

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyword, int page, int pageSize)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var model = _listService.GetAll(keyword);
                //count model
                totalRow = model.Count();
                //sap xep giam dan
                var query = model.OrderByDescending(x => x.CreatedDate).Skip(page * pageSize).Take(pageSize);
                //mapp data
                var responseData = Mapper.Map<IEnumerable<List>, IEnumerable<ListViewModel>>(query);
                //create pageination and set value
                var paginationSet = new PaginationSet<ListViewModel>()
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
                var model = _listService.GetAll();
                //mapp data
                var responseData = Mapper.Map<IEnumerable<List>, IEnumerable<ListViewModel>>(model);
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
                    var model = _listService.GetById(id);
                    //mapp data
                    var responseData = Mapper.Map<List, ListViewModel>(model);
                    //check status
                    var response = request.CreateResponse(HttpStatusCode.OK, responseData);
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
        public HttpResponseMessage Create(HttpRequestMessage request, ListViewModel listVM)
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
                    List newList = new List();
                    //Call method add product category in folder extensions
                    newList.UpdateList(listVM);
                    //Set date
                    newList.CreatedDate = DateTime.Now;
                    //Add data
                    _listService.Add(newList);
                    //Save change
                    _listService.Save();
                    //Mapping data to dataView
                    var responseData = Mapper.Map<List, ListViewModel>(newList);
                    //Check request
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }
                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        [AllowAnonymous]
        public HttpResponseMessage Update(HttpRequestMessage request, ListViewModel listVM)
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
                    List dbList = _listService.GetById(listVM.ID);
                    //Call method add product category in folder extensions
                    dbList.UpdateList(listVM);
                    //Set date
                    dbList.UpdatedDate = DateTime.Now;
                    //Add data
                    _listService.Update(dbList);
                    //Save change
                    _listService.Save();
                    //Mapping data to dataView
                    var responseData = Mapper.Map<List, ListViewModel>(dbList);
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
                        //Delete
                        var reponse = _listService.Delete(id);
                        //Save change
                        _listService.Save();
                        //Mapping data to dataView
                        var responseData = Mapper.Map<List, ListViewModel>(reponse);
                        //Check request
                        response = request.CreateResponse(HttpStatusCode.Created, responseData);
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
                    var listList = new JavaScriptSerializer().Deserialize<List<int>>(listId);
                    foreach (var id in listList)
                    {
                        _listService.Delete(id);
                    }
                    //Save change
                    _listService.Save();
                    //Check request
                    response = request.CreateResponse(HttpStatusCode.OK, listList.Count);
                }
                return response;
            });
        }

        [Route("getallbytype")]
        [HttpGet]
        public HttpResponseMessage GetAllListByType(HttpRequestMessage request, int type)
        {
            return CreateHttpResponse(request, () =>
            {
                List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
                DataTable dt = _listService.GetListAll(type);
                list = _productService.GetTableRows(dt);
                var response = request.CreateResponse(HttpStatusCode.OK, list);
                return response;
            });
        }
    }
}