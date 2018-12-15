using AutoMapper;
using Model.Model;
using Service;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;
using Web.Infrastructure.Core;
using Web.Infrastructure.Extensions;
using Web.Models;

namespace Web.Api
{
    [RoutePrefix("api/orderDetail")]
    [Authorize]
    public class OrderDetailController : ApiControllerBase
    {
        private IOrderDetailService _orderDetailService;
        private IProductService _productService;

        #region Initialize

        //khai bao contructor
        public OrderDetailController(IErrorService errorService, IOrderDetailService orderDetailService, IProductService productService)
            : base(errorService)
        {
            this._orderDetailService = orderDetailService;
            this._productService = productService;
        }

        #endregion Initialize

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _orderDetailService.GetAll();
                //mapp data
                var responseData = Mapper.Map<IEnumerable<OrderDetail>, IEnumerable<OrderDetailViewModel>>(model);
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
                    var model = _orderDetailService.GetById(id);
                    //mapp data
                    var responseData = Mapper.Map<OrderDetail, OrderDetailViewModel>(model);
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
        public HttpResponseMessage Create(HttpRequestMessage request, OrderDetailViewModel orderDetailVM)
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
                    OrderDetail newOrderDetail = new OrderDetail();
                    //Call method add product category in folder extensions
                    newOrderDetail.UpdateOrderDetail(orderDetailVM);
                    //Add data
                    _orderDetailService.Add(newOrderDetail);
                    //Save change
                    _orderDetailService.Save();
                    //Mapping data to dataView
                    var responseData = Mapper.Map<OrderDetail, OrderDetailViewModel>(newOrderDetail);
                    //Check request
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }
                return response;
            });
        }

        //[Route("update")]
        //[HttpPut]
        //[AllowAnonymous]
        //public HttpResponseMessage Update(HttpRequestMessage request, OrderDetailViewModel orderDetailVM)
        //{
        //    return CreateHttpResponse(request, () =>
        //    {
        //        HttpResponseMessage response = null;
        //        //check issue
        //        if (!ModelState.IsValid)
        //        {
        //            response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
        //        }
        //        else
        //        {
        //            OrderDetail dbOrderDetail = _orderDetailService.GetById(orderDetailVM.);
        //            //Call method add product category in folder extensions
        //            dbOrderDetail.UpdateOrderDetail(orderDetailVM);
        //            //Add data
        //            _orderDetailService.Update(dbOrderDetail);
        //            //Save change
        //            _orderDetailService.Save();
        //            //Mapping data to dataView
        //            var responseData = Mapper.Map<OrderDetail, OrderDetailViewModel>(dbOrderDetail);
        //            //Check request
        //            response = request.CreateResponse(HttpStatusCode.Created, responseData);
        //        }
        //        return response;
        //    });
        //}

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
                        var reponse = _orderDetailService.Delete(id);
                        //Save change
                        _orderDetailService.Save();
                        //Mapping data to dataView
                        var responseData = Mapper.Map<OrderDetail, OrderDetailViewModel>(reponse);
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
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string orderDetailId)
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
                    var orderDetailOrderDetail = new JavaScriptSerializer().Deserialize<List<int>>(orderDetailId);
                    foreach (var id in orderDetailOrderDetail)
                    {
                        _orderDetailService.Delete(id);
                    }
                    //Save change
                    _orderDetailService.Save();
                    //Check request
                    response = request.CreateResponse(HttpStatusCode.OK, orderDetailOrderDetail.Count);
                }
                return response;
            });
        }
    }
}