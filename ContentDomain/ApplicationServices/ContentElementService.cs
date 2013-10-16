using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContentDomain.Dto;
using ContentDomain.Repositories;

namespace ContentDomain.ApplicationServices
{
    public class ContentElementService
    {
        public void InsertNewContentElement(ContentElementDto contentElement)
        {
            var repo = new ContentElementRepository();
            try
            {
                var ce = new ContentDomain.Factories.ContentElementFactory().Create(contentElement);
                repo.Insert(ce);
            }
            catch (ArgumentException ae)
            {
            }
        }
    }
}
