using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentDomain
{
    public class TextContent : IContentValue
    {
        public int ContentValueId { get; internal set; }

        public ContentType ContentType { get { return ContentType.Text; } }

        public ContentStatus Status { get; internal set; }

        public string Value { get; internal set; }

        public Language Language { get; internal set; }

        public TextContent(Language language)
        {
            Language = language;
        }

        public void MarkComplete()
        {
            Status = ContentStatus.Complete;
        }

        public void SetValue(object value)
        {
            if (!(value is string))
                throw new ArgumentException("Argument must be string", "value");
            string _value = (string)value;
            if (String.CompareOrdinal(_value, Value) == 0)
                return;

            Value = _value;
            Status = ContentStatus.Draft;
        }
    }
}
