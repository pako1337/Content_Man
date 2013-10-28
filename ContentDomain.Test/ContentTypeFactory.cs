using ContentDomain.ContentContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentDomain.Test
{
    class ContentTypeFactory : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { ContentType.Text };
            yield return new object[] { ContentType.List };
            yield return new object[] { ContentType.Graphic };
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
