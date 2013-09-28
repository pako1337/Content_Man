using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;

namespace Content_Man.Web.api
{
    public class ContentElementApiModule : Nancy.NancyModule
    {
        public ContentElementApiModule() : base("api/ContentElement")
        {
            Get["/"] = _ =>
            {
                return HttpStatusCode.OK;
            };
        }
    }
}