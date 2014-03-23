using ContentDomain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Content_Man.Web.Api
{
    public class LanguageApiModule : Nancy.NancyModule
    {
        public LanguageApiModule()
            : base("api/Language")
        {
            Get["/"] = _ =>
            {
                return new[] { Language.Invariant };
            };
        }
    }
}