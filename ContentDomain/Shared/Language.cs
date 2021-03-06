﻿using System;
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
        
        public Language(string isoCode, string name)
        {
            LanguageId = isoCode;
            Name = name;
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
            CreateLanguageIfNeeded(isoCode);

            return _languages[isoCode];
        }

        private static void CreateLanguageIfNeeded(string isoCode)
        {
            if (!_languages.ContainsKey(isoCode))
            {
                if (IsUknownCulture(isoCode))
                    throw new ArgumentException("Unknown iso code", "isoCode");

                var language = new Language(isoCode, GetDisplayName(isoCode));
                _languages[isoCode] = language;
            }
        }

        private static string GetDisplayName(string isoCode)
        {
            if (string.Equals(Language.InvariantCode, isoCode, StringComparison.CurrentCultureIgnoreCase))
                return Language.InvariantCode;

            return CultureInfo.GetCultureInfo(isoCode).EnglishName;
        }

        private static bool IsUknownCulture(string isoCode)
        {
            return
                !string.Equals(Language.InvariantCode, isoCode, StringComparison.CurrentCultureIgnoreCase) &&
                 CultureInfo.GetCultures(CultureTypes.AllCultures)
                            .All(c => !string.Equals(c.Name, isoCode, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}