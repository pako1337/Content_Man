using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentDomain
{
    public interface IContentValue
    {
        int Id { get; }
        ContentType ContentType { get; }
        ContentStatus Status { get; }
        Language Language { get; }
        void MarkComplete();
    }
}
