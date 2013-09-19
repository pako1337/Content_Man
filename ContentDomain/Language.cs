using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentDomain
{
    public sealed class Language : IEquatable<Language>
    {
        public static readonly string Invariant = "Invariant";

        private static Dictionary<string, Language> _languages = new Dictionary<string, Language>();

        public string IsoCode { get; private set; }
        public bool IsRightToLeft { get; private set; }
        public string Name { get; private set; }

        public Language(string isoCode)
        {
            this.IsoCode = isoCode;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = obj as Language;
            if (other == null) return false;
            return Equals(other);
        }

        public override int GetHashCode()
        {
            return this.IsoCode.ToLower().GetHashCode();
        }

        public bool Equals(Language other)
        {
            if (other == null) return false;
            return string.Equals(this.IsoCode, other.IsoCode, StringComparison.OrdinalIgnoreCase);
        }

        public static Language CreateLanguage(string isoCode)
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
