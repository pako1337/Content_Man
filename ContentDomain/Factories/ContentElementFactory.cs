using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentDomain.Factories
{
    public class ContentElementFactory
    {
        public ContentElement Create(string language, ContentType type)
        {
            return new ContentElement(-1, Language.Create(language), type);
        }

        public ContentElement Create(dynamic dynamicElement)
        {
            return new ContentElement(
                dynamicElement.ContentElementId,
                Language.Create(dynamicElement.DefaultLanguage),
                (ContentType)dynamicElement.ContentType);
        }
    }
}
