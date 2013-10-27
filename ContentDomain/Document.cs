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

        public int DocumentId { get; set; }
        public DocumentStatus Status { get; set; }

        public Document()
        {
            Status = DocumentStatus.Open;
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
