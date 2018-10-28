using AutoMapper;
using Model.Model;
using Service;
using System;
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
    [RoutePrefix("api/productdetail")]
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

        #region Method ProductDetail

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

        [Route("getlistproductbysizecolor")]
        [HttpGet]
        public HttpResponseMessage Get_ListProductBySizeColor(HttpRequestMessage request, int id, int type)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _productDetailService.Get_ListProductBySizeColor(id, type);
                var response = request.CreateResponse(HttpStatusCode.OK, model);
                return response;
            });
        }

        #endregion Method ProductDetail
    }
}