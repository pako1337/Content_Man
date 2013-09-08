using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentDomain
{
    public class TextContent : IContentValue
    {
        public ContentStatus Status { get; private set; }

        public void MarkComplete()
        {
            Status = ContentStatus.Complete;
        }
    }
}
