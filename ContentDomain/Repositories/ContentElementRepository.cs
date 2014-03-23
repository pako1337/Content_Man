using System.Collections.Generic;
using System.Linq;
using ContentDomain.Dto;
using ContentDomain.Factories;
using ContentDomain.ContentContext;
using Simple.Data;

namespace ContentDomain.Repositories
{
    public class ContentElementRepository
    {
        ContentElementFactory factory = new ContentElementFactory();

        public IEnumerable<ContentElement> All()
        {
            var db = Database.Open();
            List<ContentElementDto> dbElements = db.ContentElements
                                                   .All()
                                                   .OrderByContentElementId()
                                                   .WithTextContents();

            foreach (var element in dbElements)
                yield return factory.Create(element);
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
            var contentElementDb = contentElement.AsDto();
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

        public void Update(ContentElement contentElement)
        {
            var contentElementDb = contentElement.AsDto();
            var db = Database.Open();
            using (var tx = db.BeginTransaction())
            {
                tx.ContentElements.Update(contentElementDb);
                tx.TextContents.Update(contentElementDb.TextContents);
                tx.Commit();
            }
        }
    }

    public static class ContentElementDtoExtension
    {
        public static ContentElementDto AsDto(this ContentElement contentElement)
        {
            return new ContentElementDto(contentElement);
        }

        public static IEnumerable<ContentElementDto> AsDto(this IEnumerable<ContentElement> contentElements)
        {
            return contentElements.Select(ce => new ContentElementDto(ce));
        }
    }
}
