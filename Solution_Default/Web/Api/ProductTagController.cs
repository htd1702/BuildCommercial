using Service;
using Web.Infrastructure.Core;

namespace Web.Api
{
    public class ProductTagController : ApiControllerBase
    {
        private IProductTagService _productTagService;

        //contructor
        public ProductTagController(IErrorService errorService, IProductTagService productTagService) : base(errorService)
        {
            this._productTagService = productTagService;
        }
    }
}