using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ContentDomain;
using ContentDomain.Dto;
using ContentDomain.Repositories;
using Nancy;
using Nancy.ModelBinding;

namespace Content_Man.Web.Api
{
    public class ContentElementApiModule : Nancy.NancyModule
    {
        public ContentElementApiModule()
            : base("api/ContentElement")
        {
            Get["/"] = _ =>
            {
                var list = new ContentElementRepository().All().AsDto();
                return Response.AsJson<IEnumerable<ContentElementDto>>(list);
            };

            Get["/{elementId}"] = arg =>
            {
                var jsonModel = new ContentElementRepository().Get((int)arg.elementId).AsDto();
                return Response.AsJson<ContentElementDto>(jsonModel);
            };

            Post["/"] = _ =>
            {
                var contentElement = this.Bind<ContentElementDto>();
                var service = new ContentDomain.ApplicationServices.ContentElementService();
                try
                {
                    service.InsertNewContentElement(contentElement);
                    return HttpStatusCode.OK;
                }
                catch (Exception ex)
                {
                    if (ex is ArgumentException || ex is ArgumentNullException)
                        return new Response()
                        {
                            StatusCode = HttpStatusCode.BadRequest,
                            ContentType = "text/plain",
                            Contents = s => (new System.IO.StreamWriter(s) { AutoFlush = true }).Write(ex.Message)
                        };

                    throw;
                }
            };
        }
    }
}