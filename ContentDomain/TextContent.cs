using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentDomain
{
    public class TextContent : IContentValue
    {
        public int Id { get; private set; }

        public ContentStatus Status { get; private set; }

        public string Value { get; private set; }

        public Language Language { get; private set; }

        public TextContent(Language language)
        {
            Language = language;
        }

        public void MarkComplete()
        {
            Status = ContentStatus.Complete;
        }

        public void SetValue(string value)
        {
            if (String.CompareOrdinal(value, Value) == 0)
                return;

            Value = value;
            Status = ContentStatus.Draft;
        }
    }
}
