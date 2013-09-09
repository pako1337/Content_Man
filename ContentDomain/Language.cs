using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentDomain
{
    public class Language
    {
        private static Language _invariantLanguage;

        public static Language InvariantLanguage()
        {
            if (_invariantLanguage == null)
                _invariantLanguage = new _InvariantLanguage();

            return _invariantLanguage;
        }

        private class _InvariantLanguage : Language
        { }
    }
}
