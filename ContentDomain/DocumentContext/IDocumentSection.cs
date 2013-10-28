using ContentDomain.ContentContext;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace ContentDomain.DocumentContext
{
    public interface IDocumentSection
    {
        int SectionId { get; }
        string Name { get; }
        void AddContent(ContentElement content);
        IImmutableList<ContentElement> GetContent();
    }
}
