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
            catch (Exception ex)
            {
                if (ex is ArgumentException || ex is ArgumentNullException)
                    throw new DomainException("Error occured when trying to insert ContentElement: " + ex.Message);

                throw;
            }
        }

        public void UpdateContentElement(int contentElementId, ContentElementDto contentElement)
        {
            var repo = new ContentElementRepository();
            try
            {
                var factory = new ContentDomain.Factories.ContentElementFactory();
                var ce = factory.Create(contentElement);
                var originalContentElement = repo.Get(contentElementId);
                foreach (var value in contentElement.TextContents)
                {                    
                    originalContentElement.UpdateValue(factory.CreateTextContent(value));
                }

                repo.Update(ce);
            }
            catch (Exception ex)
            {
                if (ex is ArgumentException || ex is ArgumentNullException)
                    throw new DomainException("Error occured when trying to insert ContentElement: " + ex.Message);

                throw;
            }
        }
    }
}
