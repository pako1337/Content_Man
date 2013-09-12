using System;
using System.Collections.Generic;
using System.Globalization;
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
                if (!string.Equals(Language.Invariant, isoCode, StringComparison.CurrentCultureIgnoreCase)
                    &&
                    CultureInfo
                    .GetCultures(CultureTypes.AllCultures)
                    .All(c => !string.Equals(c.Name, isoCode, StringComparison.CurrentCultureIgnoreCase)))
                    throw new ArgumentException("Unknown iso code", "isoCode");

                var language = new Language(isoCode);
                _languages[isoCode] = language;
            }
            
            return _languages[isoCode];
        }
    }
}
