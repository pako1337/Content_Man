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
                var list = new List<ContentElement>();
                var ce = new ContentElement(0, Language.Invariant);
                var textContent = new TextContent(Language.Invariant);
                textContent.SetValue("text invariant");
                ce.SetValue(textContent);

                textContent = new TextContent(Language.Create("en-GB"));
                textContent.SetValue("text in english");
                ce.SetValue(textContent);

                list.Add(ce);

                ce = new ContentElement(1, Language.Invariant);
                textContent = new TextContent(Language.Invariant);
                textContent.SetValue("Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum");
                ce.SetValue(textContent);

                textContent = new TextContent(Language.Create("en-GB"));
                textContent.SetValue("But I must explain to you how all this mistaken idea of denouncing pleasure and praising pain was born and I will give you a complete account of the system, and expound the actual teachings of the great explorer of the truth, the master-builder of human happiness. No one rejects, dislikes, or avoids pleasure itself, because it is pleasure, but because those who do not know how to pursue pleasure rationally encounter consequences that are extremely painful");
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
                var list = new List<ContentElement>();
                var ce = new ContentElement(arg.elementId, Language.Invariant);
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
        public static byte[] ToJson(this IEnumerable<ContentElement> elements)
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