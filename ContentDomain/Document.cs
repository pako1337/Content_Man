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

        public int DocumentId { get; private set; }
        public string Name { get; private set; }
        public DocumentStatus Status { get; private set; }

        public Document(string name)
        {
            Status = DocumentStatus.Open;
            Name = name;
            _content = new List<ContentElement>();
        }

        public void AddContent(ContentElement content)
        {
            _content.Add(content);
        }

        public IImmutableList<ContentElement> GetContent()
        {
            return ImmutableList.CreateRange<ContentElement>(_content);
        }
    }
}
