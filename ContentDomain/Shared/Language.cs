using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentDomain.Shared
{
    public sealed class Language : IEquatable<Language>
    {
        public static readonly string InvariantCode = "Invariant";

        private static Dictionary<string, Language> _languages = new Dictionary<string, Language>();

        public static Language Invariant { get { return Language.Create(Language.InvariantCode); } }

        public string LanguageId { get; private set; }
        public bool IsRightToLeft { get; private set; }
        public string Name { get; private set; }

        public Language()
        {

        }

        public Language(string isoCode)
        {
            this.LanguageId = isoCode;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Language);
        }

        public bool Equals(Language other)
        {
            if (other == null) return false;
            return string.Equals(this.LanguageId, other.LanguageId, StringComparison.OrdinalIgnoreCase);
        }

        public override int GetHashCode()
        {
            return this.LanguageId.ToLower().GetHashCode();
        }

        public static Language Create(string isoCode)
        {
            if (!_languages.ContainsKey(isoCode))
            {
                if (!string.Equals(Language.InvariantCode, isoCode, StringComparison.CurrentCultureIgnoreCase)
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
