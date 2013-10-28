using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentDomain.ContentContext
{
    public sealed class ContentElement
    {
        private Dictionary<Language, IContentValue> _values = new Dictionary<Language, IContentValue>();

        public int ContentElementId { get; private set; }
        public ContentType ContentType { get; private set; }
        public Language DefaultLanguage { get; private set; }

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

        public void AddValues(IEnumerable<IContentValue> values)
        {
            if (values == null)
                throw new ArgumentNullException("values");

            var listOfValues = values.ToList();
            if (listOfValues.Any(v => v == null))
                throw new ArgumentException("Value cannot be null", "values");

            var defaultVal = listOfValues.SingleOrDefault(v => v.Language.Equals(this.DefaultLanguage));
            if (_values.Count == 0 && defaultVal == null)
                throw new ArgumentException("One of first added values must be of default language", "values");

            this.AddValue(defaultVal);
            listOfValues.Remove(defaultVal);

            foreach (var v in values)
                this.AddValue(v);
        }

        public IContentValue GetValue(Language language)
        {
            ThrowIfValueWithLanguageNotPresent(language);
            return _values[language];
        }

        public IImmutableList<IContentValue> GetValues()
        {
            return ImmutableList.CreateRange(_values.Values);
        }

        public void UpdateValue(IContentValue content)
        {
            if (content.ContentType != this.ContentType)
                throw new ArgumentException("Value to update has different ContentType", "content");

            ThrowIfValueWithLanguageNotPresent(content.Language);
            var value = _values[content.Language];
            value.SetValue(content.GetValue());
        }

        private void ThrowIfValueWithLanguageNotPresent(Language language)
        {
            if (!_values.ContainsKey(language))
                throw new ArgumentException(
                    string.Format("Value for language {0} not present", language.Name),
                    "language");
        }
    }
}
