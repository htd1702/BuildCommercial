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
    [RoutePrefix("api/color")]
    [Authorize]
    public class ColorController : ApiControllerBase
    {
        private IColorService _colorService;

        #region Initialize

        //khai bao contructor
        public ColorController(IErrorService errorService, IColorService colorService) : base(errorService)
        {
            this._colorService = colorService;
        }

        #endregion Initialize

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyword, int page, int pageSize)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var model = _colorService.GetAll(keyword);
                //count model
                totalRow = model.Count();
                //sap xep giam dan
                var query = model.OrderByDescending(x => x.CreatedDate).Skip(page * pageSize).Take(pageSize);
                //mapp data
                var responseData = Mapper.Map<IEnumerable<Color>, IEnumerable<ColorViewModel>>(query);
                //create pageination and set value
                var paginationSet = new PaginationSet<ColorViewModel>()
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

        [Route("getname")]
        [HttpGet]
        public HttpResponseMessage GetName(HttpRequestMessage request, string term)
        {
            if (!string.IsNullOrWhiteSpace(term))
            {
                return CreateHttpResponse(request, () =>
                {
                    var model = _colorService.ListNameColor(term);
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
                    var listColor = new JavaScriptSerializer().Deserialize<List<int>>(listId);
                    foreach (var id in listColor)
                    {
                        _colorService.Delete(id);
                    }
                    //Save change
                    _colorService.Save();
                    //Check request
                    response = request.CreateResponse(HttpStatusCode.OK, listColor.Count);
                }
                return response;
            });
        }
    }
}