using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ContentDomain;
using ContentDomain.Dto;
using ContentDomain.Repositories;
using Nancy;
using Nancy.ModelBinding;

namespace Content_Man.Web.api
{
    public class ContentElementApiModule : Nancy.NancyModule
    {
        public ContentElementApiModule()
            : base("api/ContentElement")
        {
            Get["/"] = _ =>
            {
                var list = new ContentElementRepository().All().AsDto();

                var serializer = new Nancy.Json.JavaScriptSerializer();
                var ceString = serializer.Serialize(list);
                var bytes = System.Text.Encoding.UTF8.GetBytes(ceString);

                return new Response()
                    {
                        ContentType = "application/json",
                        Contents = s => s.Write(bytes, 0, bytes.Length)
                    };
            };

            Get["/{elementId}"] = arg =>
            {
                var jsonModel = new ContentElementRepository().Get((int)arg.elementId).AsDto();

                var serializer = new Nancy.Json.JavaScriptSerializer();
                var ceString = serializer.Serialize(jsonModel);
                var bytes = System.Text.Encoding.UTF8.GetBytes(ceString);

                return new Response()
                {
                    ContentType = "application/json",
                    Contents = s => s.Write(bytes, 0, bytes.Length)
                };
            };

            Post["/"] = _ =>
            {
                var repo = new ContentElementRepository();
                var contentElement = this.Bind<ContentElementDto>();
                //repo.Insert(ce);

                return HttpStatusCode.OK;
            };
        }
    }
}