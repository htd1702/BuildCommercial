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
    [RoutePrefix("api/size")]
    [Authorize]
    public class SizeController : ApiControllerBase
    {
        private ISizeService _sizeService;

        #region Initialize

        //khai bao contructor
        public SizeController(IErrorService errorService, ISizeService sizeService) : base(errorService)
        {
            this._sizeService = sizeService;
        }

        #endregion Initialize

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyword, int page, int pageSize)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var model = _sizeService.GetAll(keyword);
                //count model
                totalRow = model.Count();
                //sap xep giam dan
                var query = model.OrderByDescending(x => x.CreatedDate).Skip(page * pageSize).Take(pageSize);
                //mapp data
                var responseData = Mapper.Map<IEnumerable<Size>, IEnumerable<SizeViewModel>>(query);
                //create pageination and set value
                var paginationSet = new PaginationSet<SizeViewModel>()
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

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _sizeService.GetAll();
                //mapp data
                var responseData = Mapper.Map<IEnumerable<Size>, IEnumerable<SizeViewModel>>(model);
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
                    var model = _sizeService.GetById(id);
                    //mapp data
                    var responseData = Mapper.Map<Size, SizeViewModel>(model);
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
                    var model = _sizeService.ListNameSize(term);
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
        public HttpResponseMessage Create(HttpRequestMessage request, SizeViewModel sizeVM)
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
                    Size newSize = new Size();
                    //Call method add product category in folder extensions
                    newSize.UpdateSize(sizeVM);
                    //Set date
                    newSize.CreatedDate = DateTime.Now;
                    //Add data
                    _sizeService.Add(newSize);
                    //Save change
                    _sizeService.Save();
                    //Mapping data to dataView
                    var responseData = Mapper.Map<Size, SizeViewModel>(newSize);
                    //Check request
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }
                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        [AllowAnonymous]
        public HttpResponseMessage Update(HttpRequestMessage request, SizeViewModel sizeVM)
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
                    Size dbSize = _sizeService.GetById(sizeVM.ID);
                    //Call method add product category in folder extensions
                    dbSize.UpdateSize(sizeVM);
                    //Set date
                    dbSize.UpdatedDate = DateTime.Now;
                    //Add data
                    _sizeService.Update(dbSize);
                    //Save change
                    _sizeService.Save();
                    //Mapping data to dataView
                    var responseData = Mapper.Map<Size, SizeViewModel>(dbSize);
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
                        var reponse = _sizeService.Delete(id);
                        //Save change
                        _sizeService.Save();
                        //Mapping data to dataView
                        var responseData = Mapper.Map<Size, SizeViewModel>(reponse);
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
                    var listSize = new JavaScriptSerializer().Deserialize<List<int>>(listId);
                    foreach (var id in listSize)
                    {
                        _sizeService.Delete(id);
                    }
                    //Save change
                    _sizeService.Save();
                    //Check request
                    response = request.CreateResponse(HttpStatusCode.OK, listSize.Count);
                }
                return response;
            });
        }
    }
}