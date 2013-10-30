using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentDomain.Dto
{
    public class DocumentDto
    {
        public int DocumentId { get; set; }
        public string Name { get; set; }
        public List<SectionDto> Sections { get; set; }
    }
}
