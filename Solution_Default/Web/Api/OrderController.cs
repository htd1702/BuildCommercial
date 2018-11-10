using AutoMapper;
using Data;
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
    [RoutePrefix("api/order")]
    [Authorize]
    public class OrderController : ApiControllerBase
    {
        private IOrderService _orderService;
        private IOrderDetailService _orderDetailService;
        private IProductService _productService;
        private IProductDetailService _productDetailService;
        private DBContext db = new DBContext();

        #region Initialize

        //khai bao contructor
        public OrderController(IErrorService errorService, IOrderService orderService, IOrderDetailService orderDetailService, IProductService productService, IProductDetailService productDetailService)
            : base(errorService)
        {
            this._orderService = orderService;
            this._orderDetailService = orderDetailService;
            this._productService = productService;
            this._productDetailService = productDetailService;
        }

        #endregion Initialize

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, int page, int pageSize)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var model = _orderService.GetAll();
                //count model
                totalRow = model.Count();
                //sap xep giam dan
                var query = model.OrderByDescending(x => x.OrderDate).Skip(page * pageSize).Take(pageSize);
                //mapp data
                var responseData = Mapper.Map<IEnumerable<Order>, IEnumerable<OrderViewModel>>(query);
                //create pageination and set value
                var paginationSet = new PaginationSet<OrderViewModel>()
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

        [Route("getid/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetId(HttpRequestMessage request, int id)
        {
            if (id > 0)
            {
                return CreateHttpResponse(request, () =>
                {
                    var model = _orderService.GetById(id);
                    //mapp data
                    var responseData = Mapper.Map<Order, OrderViewModel>(model);
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
        public HttpResponseMessage Create(HttpRequestMessage request, object obj)
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
                    Order newOrder = new Order();
                    OrderViewModel orderVM = new OrderViewModel();
                    OrderDetail newOrderDetail = new OrderDetail();
                    OrderDetailViewModel orderDetailVM = new OrderDetailViewModel();
                    JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
                    dynamic dynamicObj = jsonSerializer.Deserialize<dynamic>(obj.ToString());
                    var listProductID = dynamicObj["listProductID"];
                    var listPrice = dynamicObj["listPrice"];
                    var listPromotion = dynamicObj["listPromotion"];
                    var listQuantity = dynamicObj["listQuantity"];
                    var listSize = dynamicObj["listSize"];
                    var listColor = dynamicObj["listColor"];
                    orderVM.CustomerName = dynamicObj["customerName"];
                    orderVM.Email = dynamicObj["customerEmail"];
                    orderVM.Address = dynamicObj["customerAddress"];
                    orderVM.Phone = dynamicObj["customerPhomeNumber"];
                    orderVM.CustomerMessage = dynamicObj["customerMessage"];
                    orderVM.Total = decimal.Parse(dynamicObj["total"]);
                    orderVM.Status = true;
                    orderVM.OrderDate = DateTime.Parse(DateTime.Now.ToString("MM/dd/yyyy"));
                    //Call method add product category in folder extensions
                    newOrder.UpdateOrder(orderVM);
                    //Add data
                    _orderService.Add(newOrder);
                    //Save change
                    _orderService.Save();
                    //Mapping data to dataView
                    var responseData = Mapper.Map<Order, OrderViewModel>(newOrder);
                    for (var i = 0; i < listProductID.Length; i++)
                    {
                        int productID = int.Parse(listProductID[i].ToString());
                        int colorID = int.Parse(listColor[i].ToString());
                        int sizeID = int.Parse(listSize[i].ToString());
                        int productDetailID = db.ProductDetails.FirstOrDefault(p => p.ProductID == productID && p.ColorID == colorID && p.SizeID == sizeID).ID;
                        orderDetailVM.ProductID = productDetailID;
                        orderDetailVM.OrderID = newOrder.ID;
                        orderDetailVM.Quantitty = int.Parse(listQuantity[i].ToString());
                        orderDetailVM.UnitPrice = int.Parse(listPrice[i].ToString());
                        //Call method add product category in folder extensions
                        newOrderDetail.UpdateOrderDetail(orderDetailVM);
                        //Add data
                        int result = _orderDetailService.Add(newOrderDetail);
                    }
                    //Check request
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }
                return response;
            });
        }

        //[Route("update")]
        //[HttpPut]
        //[AllowAnonymous]
        //public HttpResponseMessage Update(HttpRequestMessage request, OrderViewModel orderVM)
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
        //            Order dbOrder = _orderService.GetById(orderVM.ID);
        //            //Call method add product category in folder extensions
        //            dbOrder.UpdateOrder(orderVM);
        //            //Set date
        //            dbOrder.UpdatedDate = DateTime.Now;
        //            //Add data
        //            _orderService.Update(dbOrder);
        //            //Save change
        //            _orderService.Save();
        //            //Mapping data to dataView
        //            var responseData = Mapper.Map<Order, OrderViewModel>(dbOrder);
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
                        var reponse = _orderService.Delete(id);
                        //Save change
                        _orderService.Save();
                        //Mapping data to dataView
                        var responseData = Mapper.Map<Order, OrderViewModel>(reponse);
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
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string orderId)
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
                    var orderOrder = new JavaScriptSerializer().Deserialize<List<int>>(orderId);
                    foreach (var id in orderOrder)
                    {
                        _orderService.Delete(id);
                    }
                    //Save change
                    _orderService.Save();
                    //Check request
                    response = request.CreateResponse(HttpStatusCode.OK, orderOrder.Count);
                }
                return response;
            });
        }
    }
}