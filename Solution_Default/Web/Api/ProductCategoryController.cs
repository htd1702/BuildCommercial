using AutoMapper;
using Model.Model;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Web.Infrastructure.Core;
using Web.Infrastructure.Extensions;
using Web.Models;

namespace Web.Api
{
    [RoutePrefix("api/productcategory")]
    public class ProductCategoryController : ApiControllerBase
    {
        private IProductCategoryService _productCategoryService;

        #region Initialize

        //khai bao contructor
        public ProductCategoryController(IErrorService errorService, IProductCategoryService productCategoryService)
            : base(errorService)
        {
            this._productCategoryService = productCategoryService;
        }

        #endregion Initialize

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyword, int page, int pageSize)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var model = _productCategoryService.GetAll(keyword);
                //count model
                totalRow = model.Count();
                //sap xep giam dan
                var query = model.OrderByDescending(x => x.CreatedDate).Skip(page * pageSize).Take(pageSize);
                //mapp data
                var responseData = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(query);
                //create pageination and set value
                var paginationSet = new PaginationSet<ProductCategoryViewModel>()
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
                var model = _productCategoryService.GetAll();
                //mapp data
                var responseData = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(model);
                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }

        [Route("getid")]
        [HttpGet]
        public HttpResponseMessage GetId(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _productCategoryService.GetById(id);
                //mapp data
                var responseData = Mapper.Map<ProductCategory, ProductCategoryViewModel>(model);
                //check status
                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                //return status
                return response;
            });
        }

        [Route("create")]
        [HttpPost]
        //nặc danh
        [AllowAnonymous]
        public HttpResponseMessage Create(HttpRequestMessage request, ProductCategoryViewModel productCategoryVM)
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
                    ProductCategory newProductCategory = new ProductCategory();
                    //Call method add product category in folder extensions
                    newProductCategory.UpdateProductCategory(productCategoryVM);
                    //Add data
                    _productCategoryService.Add(newProductCategory);
                    //Save change
                    _productCategoryService.Save();
                    //Mapping data to dataView
                    var responseData = Mapper.Map<ProductCategory, ProductCategoryViewModel>(newProductCategory);
                    //Check request
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }
                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        //nặc danh
        [AllowAnonymous]
        public HttpResponseMessage Update(HttpRequestMessage request, ProductCategoryViewModel productCategoryVM)
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
                    ProductCategory dbProductCategory = _productCategoryService.GetById(productCategoryVM.ID);
                    //Call method add product category in folder extensions
                    dbProductCategory.UpdateProductCategory(productCategoryVM);
                    //Add data
                    _productCategoryService.Update(dbProductCategory);
                    //Save change
                    _productCategoryService.Save();
                    //Mapping data to dataView
                    var responseData = Mapper.Map<ProductCategory, ProductCategoryViewModel>(dbProductCategory);
                    //Check request
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }
                return response;
            });
        }
    }
}