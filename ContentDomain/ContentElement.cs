using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentDomain
{
    public sealed class ContentElement<T>
        where T : IContentValue
    {
        public void Add(T value)
        {
            if (value == null) throw new ArgumentNullException("value");
        }
    }
}
