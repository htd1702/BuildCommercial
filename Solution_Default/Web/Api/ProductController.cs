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
    [RoutePrefix("api/product")]
    [Authorize]
    public class ProductController : ApiControllerBase
    {
        private IProductService _productService;
        private IProductDetailService _productDetailService;

        //Contructor
        public ProductController(IErrorService errorService, IProductService productService, IProductDetailService productDetailService) : base(errorService)
        {
            this._productService = productService;
            this._productDetailService = productDetailService;
        }

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _productService.GetAll();
                model = model.OrderBy(x => x.Code).ToList();
                //mapp data
                var reponseData = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(model);
                var reponse = request.CreateResponse(HttpStatusCode.OK, reponseData);
                return reponse;
            });
        }

        [Route("getindex")]
        [HttpGet]
        public HttpResponseMessage GetIndex(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                string index = _productService.GetCodeIndexProduct();
                var reponse = request.CreateResponse(HttpStatusCode.OK, index);
                return reponse;
            });
        }

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyword, int page, int pageSize)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var model = _productService.GetAll(keyword);
                //count model
                totalRow = model.Count();
                //sap xep giam dan
                var query = model.OrderByDescending(x => x.Code).Skip(page * pageSize).Take(pageSize);
                //mapp data
                var responseData = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(query);
                //create pageination and set value
                var paginationSet = new PaginationSet<ProductViewModel>()
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
                var reponse = request.CreateResponse(HttpStatusCode.OK, paginationSet);
                return reponse;
            });
        }

        [Route("getname")]
        [HttpGet]
        public HttpResponseMessage GetName(HttpRequestMessage request, string term)
        {
            if (!string.IsNullOrWhiteSpace(term))
            {
                return CreateHttpResponse(request, () =>
                {
                    var model = _productService.ListNameProduct(term);
                    //check status
                    var response = request.CreateResponse(HttpStatusCode.OK, model);
                    //return status
                    return response;
                });
            }
            else
                return request.CreateResponse(HttpStatusCode.BadRequest);
        }

        [Route("getid/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetId(HttpRequestMessage request, int id)
        {
            if (id > 0)
            {
                return CreateHttpResponse(request, () =>
                {
                    var model = _productService.GetById(id);
                    //mapp data
                    var responseData = Mapper.Map<Product, ProductViewModel>(model);
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
                    ProductDetail newProductDetail = new ProductDetail();
                    ProductDetailViewModel newProductDetailVM = new ProductDetailViewModel();
                    ProductViewModel productVM = new ProductViewModel();
                    Product newProduct = new Product();
                    JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
                    dynamic dynamicObj = jsonSerializer.Deserialize<dynamic>(obj.ToString());
                    var listColor = dynamicObj["colorList"];
                    var listSize = dynamicObj["sizeList"];
                    var listQuantity = dynamicObj["quantityList"];
                    productVM.Name = dynamicObj["Name"];
                    productVM.NameVN = dynamicObj["NameVN"];
                    productVM.NameFr = dynamicObj["NameFr"];
                    productVM.Code = dynamicObj["Code"];
                    productVM.Alias = dynamicObj["Alias"];
                    productVM.CategoryID = dynamicObj["CategoryID"];
                    productVM.Image = dynamicObj["Image"];
                    productVM.MoreImages = dynamicObj["MoreImages"];
                    productVM.Price = dynamicObj["Price"];
                    productVM.PriceVN = dynamicObj["PriceVN"];
                    productVM.PriceFr = dynamicObj["PriceFr"];
                    productVM.Scale = dynamicObj["Scale"];
                    productVM.PromotionPrice = dynamicObj["PromotionPrice"];
                    productVM.Tags = "";
                    productVM.Quantity = 0;
                    productVM.Warranty = dynamicObj["Warranty"];
                    productVM.Description = dynamicObj["Content"];
                    productVM.Content = dynamicObj["Content"];
                    productVM.ViewCount = 1;
                    productVM.CreatedDate = DateTime.Parse(DateTime.Now.ToString("MM/dd/yyyy"));
                    productVM.CreatedBy = dynamicObj["CreatedBy"];
                    productVM.UpdatedDate = DateTime.Parse(DateTime.Now.ToString("MM/dd/yyyy"));
                    productVM.UpdatedBy = dynamicObj["UpdatedBy"];
                    productVM.MetaKeyword = "";
                    productVM.MetaDescription = "";
                    productVM.HomeFlag = dynamicObj["HomeFlag"];
                    productVM.HotFlag = dynamicObj["HotFlag"];
                    productVM.Status = dynamicObj["Status"];
                    //Call method add product in folder extensions
                    newProduct.UpdateProduct(productVM, 1);
                    //Add data
                    _productService.Add(newProduct);
                    //Save change
                    _productService.Save();
                    //Mapping data to dataView
                    var responseData = Mapper.Map<Product, ProductViewModel>(newProduct);
                    //check list color > 0
                    if (listColor.Length > 0)
                    {
                        for (int i = 0; i < listColor.Length; i++)
                        {
                            //Call method add product category in folder extensions
                            newProductDetailVM.ColorID = int.Parse(listColor[i].ToString());
                            newProductDetailVM.Quantity = int.Parse(listQuantity[i].ToString());
                            newProductDetailVM.Inventory = newProductDetailVM.Quantity;
                            for (int j = 0; j < listSize.Length; j++)
                            {
                                newProductDetailVM.ProductID = responseData.ID;
                                newProductDetailVM.SizeID = int.Parse(listSize[j].ToString());
                                newProductDetailVM.CreatedBy = productVM.CreatedBy;
                                newProductDetailVM.CreatedDate = DateTime.Parse(DateTime.Now.ToString("MM/dd/yyyy"));
                                newProductDetailVM.UpdatedBy = productVM.UpdatedBy;
                                newProductDetailVM.UpdatedDate = newProductDetailVM.CreatedDate;
                                newProductDetail.UpdateProductDetail(newProductDetailVM, 1);
                                //Add data
                                _productDetailService.Add(newProductDetail);
                                //Save change
                                _productDetailService.Save();
                            }
                        }
                    }
                    //Check request
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }
                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        [AllowAnonymous]
        public HttpResponseMessage Update(HttpRequestMessage request, object obj)
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
                    ProductDetail newProductDetail = new ProductDetail();
                    ProductDetailViewModel newProductDetailVM = new ProductDetailViewModel();
                    ProductViewModel productVM = new ProductViewModel();
                    Product newProduct = new Product();
                    JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
                    dynamic dynamicObj = jsonSerializer.Deserialize<dynamic>(obj.ToString());
                    newProduct = _productService.GetById(dynamicObj["ID"]);
                    var listColor = dynamicObj["colorList"];
                    var listSize = dynamicObj["sizeList"];
                    var listQuantity = dynamicObj["quantityList"];
                    productVM.ID = dynamicObj["ID"];
                    productVM.Name = dynamicObj["Name"];
                    productVM.NameVN = dynamicObj["NameVN"];
                    productVM.NameFr = dynamicObj["NameFr"];
                    productVM.Code = dynamicObj["Code"];
                    productVM.Alias = dynamicObj["Alias"];
                    productVM.CategoryID = dynamicObj["CategoryID"];
                    productVM.Image = dynamicObj["Image"];
                    productVM.MoreImages = dynamicObj["MoreImages"];
                    productVM.Price = dynamicObj["Price"];
                    productVM.PriceVN = dynamicObj["PriceVN"];
                    productVM.PriceFr = dynamicObj["PriceFr"];
                    productVM.Scale = dynamicObj["Scale"];
                    productVM.PromotionPrice = dynamicObj["PromotionPrice"];
                    productVM.Quantity = 0;
                    productVM.Warranty = dynamicObj["Warranty"];
                    productVM.Description = dynamicObj["Content"];
                    productVM.Content = dynamicObj["Content"];
                    productVM.ViewCount = dynamicObj["ViewCount"];
                    productVM.Tags = "";
                    productVM.UpdatedDate = DateTime.Parse(DateTime.Now.ToString("MM/dd/yyyy"));
                    productVM.UpdatedBy = dynamicObj["UpdatedBy"];
                    productVM.MetaKeyword = "";
                    productVM.MetaDescription = "";
                    productVM.HomeFlag = dynamicObj["HomeFlag"];
                    productVM.HotFlag = dynamicObj["HotFlag"];
                    productVM.Status = dynamicObj["Status"];
                    int result = _productDetailService.DeleteProductDetail(productVM.ID);
                    if (result > 0)
                    {
                        //Call method add product in folder extensions
                        newProduct.UpdateProduct(productVM, 2);
                        //Update data
                        _productService.Update(newProduct);
                        //Save change
                        _productService.Save();
                        //Mapping data to dataView
                        var responseData = Mapper.Map<Product, ProductViewModel>(newProduct);
                        //check list color
                        if (listColor.Length > 0)
                        {
                            for (int i = 0; i < listColor.Length; i++)
                            {
                                //Call method add product category in folder extensions
                                newProductDetailVM.ColorID = int.Parse(listColor[i].ToString());
                                newProductDetailVM.Quantity = int.Parse(listQuantity[i].ToString());
                                newProductDetailVM.Inventory = newProductDetailVM.Quantity;
                                for (int j = 0; j < listSize.Length; j++)
                                {
                                    newProductDetailVM.ProductID = productVM.ID;
                                    newProductDetailVM.SizeID = int.Parse(listSize[j].ToString());
                                    newProductDetailVM.CreatedBy = productVM.UpdatedBy;
                                    newProductDetailVM.CreatedDate = DateTime.Parse(DateTime.Now.ToString("MM/dd/yyyy"));
                                    newProductDetailVM.UpdatedBy = productVM.UpdatedBy;
                                    newProductDetailVM.UpdatedDate = newProductDetailVM.CreatedDate;
                                    newProductDetail.UpdateProductDetail(newProductDetailVM, 1);
                                    //Add data
                                    _productDetailService.Add(newProductDetail);
                                    //Save change
                                    _productDetailService.Save();
                                }
                            }
                        }
                        //Check request
                        response = request.CreateResponse(HttpStatusCode.Created, responseData);
                    }
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
                        //Delete product
                        var reponse = _productService.Delete(id);
                        //Save change
                        _productService.Save();
                        //Mapping data to dataView
                        var responseData = Mapper.Map<Product, ProductViewModel>(reponse);
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
                    var listProduct = new JavaScriptSerializer().Deserialize<List<int>>(listId);
                    foreach (var id in listProduct)
                    {
                        _productService.Delete(id);
                    }
                    //Save change
                    _productService.Save();
                    //Check request
                    response = request.CreateResponse(HttpStatusCode.OK, listProduct.Count);
                }
                return response;
            });
        }
    }
}