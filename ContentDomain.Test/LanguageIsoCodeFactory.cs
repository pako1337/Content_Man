using ContentDomain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentDomain.Test
{
    class LanguageIsoCodeFactory : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new[] { "pl-PL" };
            yield return new[] { "pl" };
            yield return new[] { "en-GB" };
            yield return new[] { "en" };
            yield return new[] { "EN" };
            yield return new[] { Language.InvariantCode };
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
