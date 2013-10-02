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
                var list = new List<ContentElement<TextContent>>();
                var ce = new ContentElement<TextContent>(0, Language.Invariant);
                var textContent = new TextContent(Language.Invariant);
                textContent.SetValue("text invariant");
                ce.SetValue(textContent);

                textContent = new TextContent(Language.Create("en-GB"));
                textContent.SetValue("text in english");
                ce.SetValue(textContent);

                list.Add(ce);

                ce = new ContentElement<TextContent>(1, Language.Invariant);
                textContent = new TextContent(Language.Invariant);
                textContent.SetValue("adsfasdfasdf asdfasdfasdf asdf asdf dasf dasfdasf");
                ce.SetValue(textContent);

                textContent = new TextContent(Language.Create("en-GB"));
                textContent.SetValue("eng dasfasdfas dfsdfas dfs dfas dfas dfas dfas");
                ce.SetValue(textContent);

                list.Add(ce);

                var bytes = list.ToJson();

                return new Response()
                    {
                        ContentType = "application/json",
                        Contents = s => s.Write(bytes, 0, bytes.Length)
                    };
            };

            Get["/{elementId}"] = arg =>
            {
                var list = new List<ContentElement<TextContent>>();
                var ce = new ContentElement<TextContent>(arg.elementId, Language.Invariant);
                var textContent = new TextContent(Language.Invariant);
                textContent.SetValue("text invariant");
                ce.SetValue(textContent);

                textContent = new TextContent(Language.Create("en-GB"));
                textContent.SetValue("text in english");
                ce.SetValue(textContent);

                list.Add(ce);

                var jsonModel = 
                    new
                    {
                        Id = ce.Id,
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