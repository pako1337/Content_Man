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
            if (name == null)
                throw new ArgumentNullException("name");
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty", "name");
            if (sections.Count() == 0)
                throw new ArgumentException("At least one section required", "sections");

            DocumentId = id;
            Name = name;
            Status = DocumentStatus.Open;

            _content = new List<ContentElement>();
            _sections = sections.ToList();
        }

        public void AddContent(ContentElement content, int sectionId)
        {
            var section = _sections.FirstOrDefault(s => s.SectionId == sectionId);
            section.AddContent(content);
        }

        public IImmutableList<ContentElement> GetContent()
        {
            var result = ImmutableList<ContentElement>.Empty;
            foreach (var section in _sections)
                result = result.AddRange(section.GetContent());

            return result;
        }

        public IImmutableList<IDocumentSection> GetSections()
        {
            return ImmutableList.CreateRange(_sections);
        }
    }
}
