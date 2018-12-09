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
    [RoutePrefix("api/banner")]
    [Authorize]
    public class BannerController : ApiControllerBase
    {
        private IBannerService _bannerService;

        #region Initialize

        //khai bao contructor
        public BannerController(IErrorService errorService, IBannerService bannerService) : base(errorService)
        {
            this._bannerService = bannerService;
        }

        #endregion Initialize

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyword, int page, int pageSize)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var model = _bannerService.GetAll(keyword);
                //count model
                totalRow = model.Count();
                //sap xep giam dan
                var query = model.OrderByDescending(x => x.CreatedDate).Skip(page * pageSize).Take(pageSize);
                //mapp data
                var responseData = Mapper.Map<IEnumerable<Banner>, IEnumerable<BannerViewModel>>(query);
                //create pageination and set value
                var paginationSet = new PaginationSet<BannerViewModel>()
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
                var model = _bannerService.GetAll();
                //mapp data
                var responseData = Mapper.Map<IEnumerable<Banner>, IEnumerable<BannerViewModel>>(model);
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
                    var model = _bannerService.GetById(id);
                    //mapp data
                    var responseData = Mapper.Map<Banner, BannerViewModel>(model);
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
                    var model = _bannerService.ListNameBanner(term);
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
        public HttpResponseMessage Create(HttpRequestMessage request, BannerViewModel bannerVM)
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
                    Banner newBanner = new Banner();
                    //Call method add product category in folder extensions
                    newBanner.UpdateBanner(bannerVM);
                    //Set date
                    newBanner.CreatedDate = DateTime.Now;
                    //Add data
                    _bannerService.Add(newBanner);
                    //Save change
                    _bannerService.Save();
                    //Mapping data to dataView
                    var responseData = Mapper.Map<Banner, BannerViewModel>(newBanner);
                    //Check request
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }
                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        [AllowAnonymous]
        public HttpResponseMessage Update(HttpRequestMessage request, BannerViewModel bannerVM)
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
                    Banner dbBanner = _bannerService.GetById(bannerVM.ID);
                    //Call method add product category in folder extensions
                    dbBanner.UpdateBanner(bannerVM);
                    //Set date
                    dbBanner.UpdatedDate = DateTime.Now;
                    //Add data
                    _bannerService.Update(dbBanner);
                    //Save change
                    _bannerService.Save();
                    //Mapping data to dataView
                    var responseData = Mapper.Map<Banner, BannerViewModel>(dbBanner);
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
                        var reponse = _bannerService.Delete(id);
                        //Save change
                        _bannerService.Save();
                        //Mapping data to dataView
                        var responseData = Mapper.Map<Banner, BannerViewModel>(reponse);
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
                    var listBanner = new JavaScriptSerializer().Deserialize<List<int>>(listId);
                    foreach (var id in listBanner)
                    {
                        _bannerService.Delete(id);
                    }
                    //Save change
                    _bannerService.Save();
                    //Check request
                    response = request.CreateResponse(HttpStatusCode.OK, listBanner.Count);
                }
                return response;
            });
        }
    }
}