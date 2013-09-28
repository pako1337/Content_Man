using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ContentDomain;
using Nancy;

namespace Content_Man.Web.api
{
    public class ContentElementApiModule : Nancy.NancyModule
    {
        public ContentElementApiModule() : base("api/ContentElement")
        {
            Get["/"] = _ =>
            {
                var ce = new ContentElement<TextContent>(Language.Invariant);
                var textContent = new TextContent(Language.Invariant);
                textContent.SetValue("text");
                ce.SetValue(textContent);

                var obj = new
                {
                    Id = ce.Id,
                    DefaultLanguage = ce.DefaultLanguage,
                    Values = ce.GetValues()
                };

                var serializer = new Nancy.Json.JavaScriptSerializer();
                var ceString = serializer.Serialize(obj);
                var bytes = System.Text.Encoding.UTF8.GetBytes(ceString);

                return new Response()
                    {
                        ContentType = "application/json",
                        Contents = s => s.Write(bytes, 0, bytes.Length)
                    };
            };
        }
    }
}