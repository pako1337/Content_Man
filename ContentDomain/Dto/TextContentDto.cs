using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentDomain.Dto
{
    public class TextContentDto
    {
        public int TextContentId { get; set; }
        public int ContentElementId { get; set; }
        public ContentStatus ContentStatus { get; set; }
        public string Value { get; set; }
        public string Language { get; set; }

        public TextContentDto()
        { }

        public TextContentDto(TextContent tc, ContentElementDto ce)
        {
            TextContentId = tc.ContentValueId;
            ContentElementId = ce.ContentElementId;
            ContentStatus = tc.Status;
            Value = tc.Value;
            Language = tc.Language.LanguageId;
        }
    }
}
