using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Web.Infrastructure.Core;

namespace Web.Api
{
    public class PostController : ApiControllerBase
    {
        private IPostService _postService;

        //Contructor
        public PostController(IErrorService errorService, IPostService postService) : base(errorService)
        {
            this._postService = postService;
        }
    }
}
