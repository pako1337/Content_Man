﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentDomain
{
    public sealed class ContentElement
    {
        private Dictionary<Language, IContentValue> _values = new Dictionary<Language, IContentValue>();

        public int          ContentElementId { get; private set; }
        public ContentType  ContentType { get; private set; }
        public Language     DefaultLanguage { get; private set; }

        public ContentElement(int id, Language defaultLanguage, ContentType contentType)
        {
            ContentElementId = id;
            ContentType = contentType;
            DefaultLanguage = defaultLanguage;
        }

        public void AddValue(IContentValue value)
        {
            if (value == null)
                throw new ArgumentNullException("value");
            if (_values.Count == 0 && value.Language != DefaultLanguage)
                throw new ArgumentException("First added value must be of default language", "value");
            if (value.ContentType != ContentType)
                throw new ArgumentException("Value has different type than ContentElement", "value");

            _values[value.Language] = value;
        }

        public IContentValue GetValue(Language language)
        {
            CheckValueWithLanguagePresent(language);
            return _values[language];
        }

        public IEnumerable<IContentValue> GetValues()
        {
            return _values.Values;
        }

        public void UpdateValue(TextContent content)
        {
            CheckValueWithLanguagePresent(content.Language);
            var value = (TextContent)_values[content.Language];
            value.SetValue(content.Value);
        }

        public void AddValues(IEnumerable<IContentValue> values)
        {
            if (values == null) throw new ArgumentNullException("values");

            var defaultVal = values.SingleOrDefault(v => v.Language.Equals(this.DefaultLanguage));
            if (_values.Count == 0 && defaultVal == null)
                throw new ArgumentException("One of first added values must be of default language", "values");

            this.AddValue(defaultVal);

            foreach (var v in values.Where(v => !v.Language.Equals(this.DefaultLanguage)))
                this.AddValue(v);
        }

        private void CheckValueWithLanguagePresent(Language language)
        {
            if (!_values.ContainsKey(language))
                throw new ArgumentException(
                    string.Format("Value for language {0} not present", language.Name),
                    "language");
        }
    }
}
