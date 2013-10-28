using ContentDomain.ContentContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentDomain.Dto
{
    public class ContentElementDto
    {
        public int ContentElementId { get; set; }
        public ContentType ContentType { get; set; }
        public string DefaultLanguage { get; set; }
        public List<TextContentDto> TextContents { get; set; }

        public ContentElementDto()
        { }

        public ContentElementDto(ContentElement ce)
        {
            ContentElementId = ce.ContentElementId;
            ContentType = ce.ContentType;
            DefaultLanguage = ce.DefaultLanguage.LanguageId;
            TextContents = ce
                .GetValues()
                .OfType<TextContent>()
                .Select(v => new TextContentDto(v, this))
                .ToList();
        }
    }
}
