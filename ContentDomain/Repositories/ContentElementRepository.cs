using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContentDomain.Dto;
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
            List<ContentElementDto> dbElements = db.ContentElements.All().WithTextContents();

            var elements = new List<ContentElement>();
            foreach (var element in dbElements)
                elements.Add(factory.Create(element));

            return elements;
        }

        public ContentElement Get(int contentElementId)
        {
            var db = Database.Open();
            List<ContentElementDto> elements = db.ContentElements
                .FindAllByContentElementId(contentElementId)
                .WithTextContents();
            return factory.Create(elements.FirstOrDefault());
        }

        public void Insert(ContentElement contentElement)
        {
            var contentElementDb = new ContentElementDto(contentElement);
            var db = Database.Open();
            using (var tx = db.BeginTransaction())
            {
                var ce = tx.ContentElements.Insert(contentElementDb);
                foreach (var textContent in contentElementDb.TextContents)
                    textContent.ContentElementId = ce.ContentElementId;

                tx.TextContents.Insert(contentElementDb.TextContents);

                tx.Commit();
            }
        }
    }
}
