using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentDomain
{
    public class Document
    {
        private List<ContentElement> _content;
        private List<IDocumentSection> _sections;

        public int DocumentId { get; private set; }
        public string Name { get; private set; }
        public DocumentStatus Status { get; private set; }

        public Document(int id, string name, IEnumerable<IDocumentSection> sections)
        {
            DocumentId = id;
            Name = name;
            Status = DocumentStatus.Open;
            _content = new List<ContentElement>();
            _sections = sections.ToList();
        }

        public void AddContent(ContentElement content)
        {
            _content.Add(content);
        }

        public IImmutableList<ContentElement> GetContent()
        {
            return ImmutableList.CreateRange<ContentElement>(_content);
        }

        public IImmutableList<IDocumentSection> GetSections()
        {
            return ImmutableList.CreateRange(_sections);
        }
    }
}
