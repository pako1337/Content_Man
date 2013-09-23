using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Content_Man.Web.api
{
    public class ContentElement : Nancy.NancyModule
    {
        public ContentElement() : base("api/")
        {
        }
    }
}