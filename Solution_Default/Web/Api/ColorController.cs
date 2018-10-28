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
    [RoutePrefix("api/color")]
    [Authorize]
    public class ColorController : ApiControllerBase
    {
        private IColorService _colorService;
        private IProductService _productService;

        #region Initialize

        //khai bao contructor
        public ColorController(IErrorService errorService, IColorService colorService, IProductService productService)
            : base(errorService)
        {
            this._colorService = colorService;
            this._productService = productService;
        }

        #endregion Initialize

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _colorService.GetAll();
                //mapp data
                var responseData = Mapper.Map<IEnumerable<Color>, IEnumerable<ColorViewModel>>(model);
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
                    var model = _colorService.GetById(id);
                    //mapp data
                    var responseData = Mapper.Map<Color, ColorViewModel>(model);
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
        public HttpResponseMessage Create(HttpRequestMessage request, ColorViewModel colorVM)
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
                    Color newColor = new Color();
                    //Call method add product category in folder extensions
                    newColor.UpdateColor(colorVM);
                    //Set date
                    newColor.CreatedDate = DateTime.Now;
                    //Add data
                    _colorService.Add(newColor);
                    //Save change
                    _colorService.Save();
                    //Mapping data to dataView
                    var responseData = Mapper.Map<Color, ColorViewModel>(newColor);
                    //Check request
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }
                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        [AllowAnonymous]
        public HttpResponseMessage Update(HttpRequestMessage request, ColorViewModel colorVM)
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
                    Color dbColor = _colorService.GetById(colorVM.ID);
                    //Call method add product category in folder extensions
                    dbColor.UpdateColor(colorVM);
                    //Set date
                    dbColor.UpdatedDate = DateTime.Now;
                    //Add data
                    _colorService.Update(dbColor);
                    //Save change
                    _colorService.Save();
                    //Mapping data to dataView
                    var responseData = Mapper.Map<Color, ColorViewModel>(dbColor);
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
                        var reponse = _colorService.Delete(id);
                        //Save change
                        _colorService.Save();
                        //Mapping data to dataView
                        var responseData = Mapper.Map<Color, ColorViewModel>(reponse);
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
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string colorId)
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
                    var colorColor = new JavaScriptSerializer().Deserialize<List<int>>(colorId);
                    foreach (var id in colorColor)
                    {
                        _colorService.Delete(id);
                    }
                    //Save change
                    _colorService.Save();
                    //Check request
                    response = request.CreateResponse(HttpStatusCode.OK, colorColor.Count);
                }
                return response;
            });
        }
    }
}