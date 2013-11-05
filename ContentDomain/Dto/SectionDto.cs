using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentDomain.Dto
{
    public class SectionDto
    {
        public int SectionId { get; set; }
        public int Type { get; set; }
        public string Name { get; set; }
        public int DocumentId { get; set; }
        public List<ContentElementDto> ContentElements { get; set; }

        public SectionDto(DocumentContext.IDocumentSection s)
        {
            SectionId = s.SectionId;
            Name = s.Name;
        }
    }
}
