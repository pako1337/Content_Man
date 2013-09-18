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
        private T _value;

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
            if (_value == null && value.Language != DefaultLanguage)
                throw new ArgumentException("First added value must be of default language", "value");

            _value = value;
        }

        public T GetValue()
        {
            return _value;
        }
    }
}
