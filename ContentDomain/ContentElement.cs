using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentDomain
{
    public sealed class ContentElement
    {
        private Dictionary<Language, IContentValue> _values = new Dictionary<Language, IContentValue>();

        public int Id { get; private set; }

        public Language DefaultLanguage { get; private set; }

        public ContentElement(int id, Language defaultLanguage)
        {
            Id = id;
            DefaultLanguage = defaultLanguage;
        }

        public void SetValue(IContentValue value)
        {
            if (value == null)
                throw new ArgumentNullException("value");
            if (_values.Count == 0 && value.Language != DefaultLanguage)
                throw new ArgumentException("First added value must be of default language", "value");

            _values[value.Language] = value;
        }

        public IContentValue GetValue(Language language)
        {
            if (!_values.ContainsKey(language))
                throw new ArgumentException(
                    string.Format("Value for language {0} not present", language.Name),
                    "language");
            return _values[language];
        }

        public IEnumerable<IContentValue> GetValues()
        {
            return _values.Values;
        }
    }
}
