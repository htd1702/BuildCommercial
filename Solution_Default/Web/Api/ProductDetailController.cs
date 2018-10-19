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
    [RoutePrefix("api/productdetail")]
    [Authorize]
    public class ProductDetailController : ApiControllerBase
    {
        private IProductDetailService _productDetailService;
        private IProductService _productService;

        #region Initialize

        //khai bao contructor
        public ProductDetailController(IErrorService errorService, IProductDetailService productDetailService, IProductService productService)
            : base(errorService)
        {
            this._productDetailService = productDetailService;
            this._productService = productService;
        }

        #endregion Initialize

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, int page, int pageSize)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var model = _productDetailService.GetAll();
                //count model
                totalRow = model.Count();
                //sap xep giam dan
                var query = model.OrderByDescending(x => x.CreatedDate).Skip(page * pageSize).Take(pageSize);
                //mapp data
                var responseData = Mapper.Map<IEnumerable<ProductDetail>, IEnumerable<ProductDetailViewModel>>(query);
                //create pageination and set value
                var paginationSet = new PaginationSet<ProductDetailViewModel>()
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
                var model = _productDetailService.GetAll();
                //mapp data
                var responseData = Mapper.Map<IEnumerable<ProductDetail>, IEnumerable<ProductDetailViewModel>>(model);
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
                    var model = _productDetailService.GetById(id);
                    //mapp data
                    var responseData = Mapper.Map<ProductDetail, ProductDetailViewModel>(model);
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
                    return response;
                }
                else
                {
                    ProductDetail newProductDetail = new ProductDetail();
                    ProductDetailViewModel newProductDetailVM = new ProductDetailViewModel();
                    JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
                    dynamic dynamicObj = jsonSerializer.Deserialize<dynamic>(obj.ToString());
                    var productID = dynamicObj["id"];
                    var listColor = dynamicObj["colorList"];
                    var listSize = dynamicObj["sizeList"];
                    var listQuantity = dynamicObj["quantityList"];
                    var createBy = dynamicObj["createdBy"];
                    for (int i = 0; i < listColor.Length; i++)
                    {
                        //Call method add product category in folder extensions
                        newProductDetailVM.ColorID = int.Parse(listColor[i].ToString());
                        newProductDetailVM.Quantity = int.Parse(listQuantity[i].ToString());
                        for (int j = 0; j < listSize.Length; j++)
                        {
                            newProductDetailVM.ProductID = productID;
                            newProductDetailVM.SizeID = int.Parse(listSize[j].ToString());
                            newProductDetailVM.CreatedBy = createBy;
                            newProductDetailVM.CreatedDate = DateTime.Parse(DateTime.Now.ToString("MM/dd/yyyy"));
                            newProductDetailVM.UpdatedBy = createBy;
                            newProductDetailVM.UpdatedDate = newProductDetailVM.CreatedDate;
                            newProductDetail.UpdateProductDetail(newProductDetailVM, 1);
                            //Add data
                            _productDetailService.Add(newProductDetail);
                            //Save change
                            _productDetailService.Save();
                        }
                    }
                    var responseData = Mapper.Map<ProductDetail, ProductDetailViewModel>(newProductDetail);
                    //Check request
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                    return response;
                }
            });
        }

        [Route("update")]
        [HttpPut]
        [AllowAnonymous]
        public HttpResponseMessage Update(HttpRequestMessage request, ProductDetailViewModel productDetailVM)
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
                    ProductDetail dbProductDetail = _productDetailService.GetById(productDetailVM.ID);
                    //Call method add product category in folder extensions
                    dbProductDetail.UpdateProductDetail(productDetailVM, 2);
                    //Set date
                    dbProductDetail.UpdatedDate = DateTime.Now;
                    //Add data
                    _productDetailService.Update(dbProductDetail);
                    //Save change
                    _productDetailService.Save();
                    //Mapping data to dataView
                    var responseData = Mapper.Map<ProductDetail, ProductDetailViewModel>(dbProductDetail);
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
                        var reponse = _productDetailService.Delete(id);
                        //Save change
                        _productDetailService.Save();
                        //Mapping data to dataView
                        var responseData = Mapper.Map<ProductDetail, ProductDetailViewModel>(reponse);
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
                    var listProductDetail = new JavaScriptSerializer().Deserialize<List<int>>(listId);
                    foreach (var id in listProductDetail)
                    {
                        _productDetailService.Delete(id);
                    }
                    //Save change
                    _productDetailService.Save();
                    //Check request
                    response = request.CreateResponse(HttpStatusCode.OK, listProductDetail.Count);
                }
                return response;
            });
        }

        [Route("getlistcategory")]
        [HttpGet]
        public HttpResponseMessage LoadListCategory(HttpRequestMessage request, int id, int type)
        {
            return CreateHttpResponse(request, () =>
            {
                if (type == 1)
                {
                    var model = _productDetailService.GetCategories();
                    var responseData = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(model);
                    var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                    return response;
                }
                else if (type == 2)
                {
                    var model = _productDetailService.GetProductCategories(id);
                    var responseData = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(model);
                    var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                    return response;
                }
                else
                {
                    var model = _productService.ListProductByCategory(id);
                    var responseData = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(model);
                    var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                    return response;
                }
            });
        }
    }
}