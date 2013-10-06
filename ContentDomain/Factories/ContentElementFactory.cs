using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentDomain.Factories
{
    public class ContentElementFactory
    {
        public ContentElementFactory()
        {

        }

        public ContentElement Create()
        {
            return new ContentElement(1, null, ContentType.Text);
        }
    }
}
