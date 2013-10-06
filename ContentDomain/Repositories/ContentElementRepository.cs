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
            dynamic e = db.ContentElements.All().WithTextContents();

            var elements = new List<ContentElement>();
            var factory = new ContentElementFactory();
            foreach (var element in e)
            {
                if (element.TextContents == null)
                    element.TextContents = new object[0];
                elements.Add(factory.Create(element));
            }

            return elements;
        }
    }
}
