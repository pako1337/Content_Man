using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentDomain
{
    public sealed class ContentElement<T>
        where T : class, IContentValue
    {
        private Dictionary<Language, T> _values = new Dictionary<Language,T>();

        public int Id { get; private set; }

        public Language DefaultLanguage { get; private set; }

        public ContentElement(Language defaultLanguage)
        {
            DefaultLanguage = defaultLanguage;
        }

        public void SetValue(T value)
        {
            if (value == null)
                throw new ArgumentNullException("value");
            if (_values.Count == 0 && value.Language != DefaultLanguage)
                throw new ArgumentException("First added value must be of default language", "value");

            _values[value.Language] = value;
        }

        public T GetValue()
        {
            return _values.Values.First();
        }

        public IEnumerable<IContentValue> GetValues()
        {
            return _values.Values;
        }
    }
}
