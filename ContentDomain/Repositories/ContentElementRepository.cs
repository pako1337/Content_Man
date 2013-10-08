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
        ContentElementFactory factory = new ContentElementFactory();

        public IEnumerable<ContentElement> All()
        {
            var db = Database.Open();
            List<ContentElementDb> dbElements = db.ContentElements.All().WithTextContents();

            var elements = new List<ContentElement>();
            foreach (var element in dbElements)
                elements.Add(factory.Create(element));

            return elements;
        }

        public ContentElement Get(int contentElementId)
        {
            var db = Database.Open();
            List<ContentElementDb> elements = db.ContentElements
                .FindAllByContentElementId(contentElementId)
                .WithTextContents();
            return factory.Create(elements.FirstOrDefault());
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
