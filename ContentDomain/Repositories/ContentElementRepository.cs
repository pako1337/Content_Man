using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContentDomain.Factories;
using Simple.Data;

namespace ContentDomain.Repositories
{
    public class ContentElementRepository
    {
        public IEnumerable<ContentElement> All()
        {
            var db = Database.Open();
            List<ContentElementDb> e = db.ContentElements.All().WithTextContents();

            var elements = new List<ContentElement>();
            var factory = new ContentElementFactory();
            foreach (var element in e)
                elements.Add(factory.Create(element));

            return elements;
        }
    }

    internal class ContentElementDb
    {
        public int ContentElementId { get; set; }
        public ContentType ContentType { get; set; }
        public string DefaultLanguage { get; set; }
        public List<TextContentDb> TextContents { get; set; }
    }

    internal class TextContentDb
    {
        public int TextContentId { get; set; }
        public int ContentStatus { get; set; }
        public string Value { get; set; }
        public string Language { get; set; }
    }
}
