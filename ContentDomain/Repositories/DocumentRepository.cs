using ContentDomain.DocumentContext;
using ContentDomain.Dto;
using Simple.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentDomain.Repositories
{
    public class DocumentRepository
    {
        public IEnumerable<DocumentDto> GetAll()
        {
            var database = Database.Open();
            return database.Documents.All().OrderByDocumentId();
        }

        public void Insert(Document document)
        {
            var documentDb = document.AsDto();
            var db = Database.Open();
            using (var tx = db.BeginTransaction())
            {
                var ce = tx.Documents.Insert(documentDb);
                foreach (var section in documentDb.Sections)
                    section.DocumentId = ce.DocumentId;

                tx.TextContents.Insert(documentDb.Sections);
                tx.Commit();
            }
        }
    }

    public static class DocumentDtoExtension
    {
        public static DocumentDto AsDto(this Document document)
        {
            return new DocumentDto(document);
        }

        public static IEnumerable<DocumentDto> AsDto(this IEnumerable<Document> documents)
        {
            return documents.Select(doc => new DocumentDto(doc));
        }
    }
}
