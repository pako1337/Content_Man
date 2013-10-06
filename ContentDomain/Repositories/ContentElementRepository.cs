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
        public ContentElementRepository()
        {
            var db = Database.Open();
            dynamic e = db.ContentElements.All().WithTextContents();

            var elements = new List<ContentElement>();
            var factory = new ContentElementFactory();
            foreach (var element in e)
            {
                elements.Add(factory.Create(element));
                var l = element.TextContents;
            }
        }
    }
}
