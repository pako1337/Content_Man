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
                catch (DomainException ex)
                {
                    return Response.FromException(ex);
                }
            };

            Put["/{elementId}"] = args =>
            {
                var contentElement = this.Bind<ContentElementDto>();
                var service = new ContentDomain.ApplicationServices.ContentElementService();
                try
                {
                    return HttpStatusCode.OK;
                }
                catch (DomainException ex)
                {
                    return Response.FromException(ex);
                }
            };
        }
    }
}