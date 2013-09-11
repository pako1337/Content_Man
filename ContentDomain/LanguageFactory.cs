using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentDomain
{
    public class LanguageFactory
    {
        private Dictionary<string, Language> _languages = new Dictionary<string,Language>();

        public Language CreateLanguage(string isoCode)
        {
            if (!_languages.ContainsKey(isoCode))
            {
                var language = new Language(isoCode);
                _languages[isoCode] = language;
            }
            
            return _languages[isoCode];
        }
    }
}
