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
        public ContentElementApiModule()
            : base("api/ContentElement")
        {
            Get["/"] = _ =>
            {
                var ce = new ContentElement<TextContent>(Language.Invariant);
                var textContent = new TextContent(Language.Invariant);
                textContent.SetValue("text invariant");
                ce.SetValue(textContent);

                textContent = new TextContent(Language.Create("en-GB"));
                textContent.SetValue("text in english");
                ce.SetValue(textContent);

                var list = new[] { ce };
                var bytes = list.ToJson();

                return new Response()
                    {
                        ContentType = "application/json",
                        Contents = s => s.Write(bytes, 0, bytes.Length)
                    };
            };
        }
    }

    internal static class ContentElementExtension
    {
        public static byte[] ToJson<T>(this IEnumerable<ContentElement<T>> elements) where T : class, IContentValue
        {
            var jsonModel = elements.Select(ce =>
                new
                {
                    Id = ce.Id,
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