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
                return Response.AsJson<IEnumerable<ContentElementDto>>(list, HttpStatusCode.OK);
            };

            Get["/{elementId}"] = arg =>
            {
                var jsonModel = new ContentElementRepository().Get((int)arg.elementId).AsDto();
                return Response.AsJson<ContentElementDto>(jsonModel, HttpStatusCode.OK);
            };

            Post["/"] = _ =>
            {
                var repo = new ContentElementRepository();
                var contentElement = this.Bind<ContentElementDto>();
                repo.Insert(new ContentDomain.Factories.ContentElementFactory().Create(contentElement));

                return HttpStatusCode.OK;
            };
        }
    }
}