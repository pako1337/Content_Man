using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentDomain
{
    public class Document
    {
        public int DocumentId { get; set; }
        public DocumentStatus Status { get; set; }

        public Document()
        {
            Status = DocumentStatus.Open;
        }
    }
}
