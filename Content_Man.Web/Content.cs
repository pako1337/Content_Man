using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Content_Man.Web
{
    public class Content : Nancy.NancyModule
    {
        public Content() : base("/")
        {
            Get["Content"] = _ => View["Content"];
        }
    }
}