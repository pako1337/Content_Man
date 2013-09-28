using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Content_Man.Web
{
    public class ContentModule : Nancy.NancyModule
    {
        public ContentModule() : base("/")
        {
            Get[""] = _ => View["Content"];
            Get["{section}"] = param => View[param.section];
        }
    }
}