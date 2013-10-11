using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ContentDomain;
using ContentDomain.Repositories;
using Nancy;

namespace Content_Man.Web.api
{
    public class ContentElementApiModule : Nancy.NancyModule
    {
        public ContentElementApiModule()
            : base("api/ContentElement")
        {
            Get["/"] = _ =>
            {
                var list = new ContentElementRepository().All();

                var bytes = list.ToJson();

                return new Response()
                    {
                        ContentType = "application/json",
                        Contents = s => s.Write(bytes, 0, bytes.Length)
                    };
            };

            Get["/{elementId}"] = arg =>
            {
                var ce = new ContentElementRepository().Get(arg.elementId);

                var jsonModel = 
                    new
                    {
                        Id = ce.ContentElementId,
                        DefaultLanguage = ce.DefaultLanguage,
                        Values = ce.GetValues()
                    };

                var serializer = new Nancy.Json.JavaScriptSerializer();
                var ceString = serializer.Serialize(jsonModel);
                var bytes = System.Text.Encoding.UTF8.GetBytes(ceString);

                return new Response()
                {
                    ContentType = "application/json",
                    Contents = s => s.Write(bytes, 0, bytes.Length)
                };
            };

            Get["/post"] = _ =>
            {
                var repo = new ContentElementRepository();
                var ce = repo.Get(1);
                repo.Insert(ce);

                return HttpStatusCode.OK;
            };
        }
    }

    internal static class ContentElementExtension
    {
        public static byte[] ToJson(this IEnumerable<ContentElement> elements)
        {
            var jsonModel = elements.Select(ce =>
                new
                {
                    Id = ce.ContentElementId,
                    DefaultLanguage = ce.DefaultLanguage,
                    Values = ce.GetValues()
                });

            var serializer = new Nancy.Json.JavaScriptSerializer();
            var ceString = serializer.Serialize(jsonModel);
            var bytes = System.Text.Encoding.UTF8.GetBytes(ceString);

            return bytes;
        }
    }
}