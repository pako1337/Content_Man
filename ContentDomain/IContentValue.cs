using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentDomain
{
    public interface IContentValue
    {
        int ContentValueId { get; }
        ContentType ContentType { get; }
        ContentStatus Status { get; }
        Language Language { get; }
        void SetValue(object value);
        object GetValue();
        void MarkComplete();
    }
}
