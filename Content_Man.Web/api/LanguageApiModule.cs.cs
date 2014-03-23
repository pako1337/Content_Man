using ContentDomain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using Nancy.ModelBinding;

namespace Content_Man.Web.Api
{
    public class LanguageApiModule : Nancy.NancyModule
    {
        public LanguageApiModule()
            : base("api/Language")
        {
            Get["/"] = _ =>
            {
                var languages = new LanguageRepository().GetLanguages();
                return Response.AsJson<IEnumerable<Language>>(languages);
            };
        }
    }
}