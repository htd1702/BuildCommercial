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
    [RoutePrefix("api/info")]
    [Authorize]
    public class ContactDetailController : ApiControllerBase
    {
        private IContactDetailService _contactDetailService;

        #region Initialize

        //khai bao contructor
        public ContactDetailController(IErrorService errorService, IContactDetailService contactDetailService) : base(errorService)
        {
            this._contactDetailService = contactDetailService;
        }

        #endregion Initialize

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _contactDetailService.GetAll();
                //mapp data
                var responseData = Mapper.Map<IEnumerable<ContactDetail>, IEnumerable<ContactDetailViewModel>>(model);
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
                    var model = _contactDetailService.GetById(id);
                    //mapp data
                    var responseData = Mapper.Map<ContactDetail, ContactDetailViewModel>(model);
                    //check status
                    var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                    //return status
                    return response;
                });
            }
            else
                return request.CreateResponse(HttpStatusCode.BadRequest);
        }

        [Route("update")]
        [HttpPut]
        [AllowAnonymous]
        public HttpResponseMessage Update(HttpRequestMessage request, ContactDetailViewModel contactDetailVM)
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
                    ContactDetail dbContactDetail = _contactDetailService.GetById(contactDetailVM.ID);
                    //Call method add product category in folder extensions
                    dbContactDetail.UpdateContactDetail(contactDetailVM);
                    //Add data
                    _contactDetailService.Update(dbContactDetail);
                    //Save change
                    _contactDetailService.Save();
                    //Mapping data to dataView
                    var responseData = Mapper.Map<ContactDetail, ContactDetailViewModel>(dbContactDetail);
                    //Check request
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }
                return response;
            });
        }
    }
}