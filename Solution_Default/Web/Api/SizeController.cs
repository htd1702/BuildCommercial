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
    [RoutePrefix("api/size")]
    [Authorize]
    public class SizeController : ApiControllerBase
    {
        private ISizeService _sizeService;
        private IProductService _productService;

        #region Initialize

        //khai bao contructor
        public SizeController(IErrorService errorService, ISizeService sizeService, IProductService productService)
            : base(errorService)
        {
            this._sizeService = sizeService;
            this._productService = productService;
        }

        #endregion Initialize

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
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string sizeId)
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
                    var sizeSize = new JavaScriptSerializer().Deserialize<List<int>>(sizeId);
                    foreach (var id in sizeSize)
                    {
                        _sizeService.Delete(id);
                    }
                    //Save change
                    _sizeService.Save();
                    //Check request
                    response = request.CreateResponse(HttpStatusCode.OK, sizeSize.Count);
                }
                return response;
            });
        }
    }
}