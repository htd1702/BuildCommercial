using AutoMapper;
using Model.Model;
using Service;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Web.Infrastructure.Core;
using Web.Models;

namespace Web.Api
{
    [RoutePrefix("api/product")]
    public class ProductController : ApiControllerBase
    {
        private IProductService _productService;

        //Contructor
        public ProductController(IErrorService errorService, IProductService productService) : base(errorService)
        {
            this._productService = productService;
        }

        [Route("getall")]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _productService.GetAll();
                //mapp data
                var reponseData = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(model);
                var reponse = request.CreateResponse(HttpStatusCode.OK, reponseData);
                return reponse;
            });
        }
    }
}