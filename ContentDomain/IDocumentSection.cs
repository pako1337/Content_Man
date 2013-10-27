using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace ContentDomain
{
    public interface IDocumentSection
    {
        int SectionId { get; }
        void AddContent(ContentElement content);
        IImmutableList<ContentElement> GetContent();
    }
}
