using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentDomain
{
    public sealed class Language
    {
        public static readonly string Invariant = "Invariant";

        public string IsoCode { get; private set; }
        public bool IsRightToLeft { get; private set; }
        public string Name { get; private set; }

        public Language(string isoCode)
        {
            this.IsoCode = isoCode;
        }
    }
}
