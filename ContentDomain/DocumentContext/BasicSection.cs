using ContentDomain.ContentContext;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentDomain.DocumentContext
{
    public class BasicSection : IDocumentSection
    {
        private List<ContentElement> _content = new List<ContentElement>();
        private int _id;
        private string _name;

        public int SectionId { get { return _id; } }
        public string Name { get { return _name; } }

        public BasicSection(int id,  string name)
        {
            _id = id;
            _name = name;
        }

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
