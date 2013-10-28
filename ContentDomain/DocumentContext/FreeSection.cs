using ContentDomain.ContentContext;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentDomain.DocumentContext
{
    public class FreeSection : IDocumentSection
    {
        public List<ContentElement> _content = new List<ContentElement>();

        public int SectionId { get { return 1; } }
        public string Name { get { return ""; } }

        public void AddContent(ContentContext.ContentElement contentElement)
        {
            _content.Add(contentElement);
        }

        public IImmutableList<ContentContext.ContentElement> GetContent()
        {
            return _content.ToImmutableList();
        }
    }
}
