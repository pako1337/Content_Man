using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Extensions;
using FluentAssertions;
using ContentDomain.ContentContext;
using ContentDomain.Shared;
using ContentDomain.DocumentContext;

namespace ContentDomain.Test
{
    public class FreeSectionTest
    {
        [Fact]
        public void should_accept_any_content()
        {
            var section = new FreeSection();
            section.AddContent(new ContentElement(-1, Language.Invariant, ContentType.Text));
            section.GetContent().Count().Should().Be(1);
        }
    }
}
