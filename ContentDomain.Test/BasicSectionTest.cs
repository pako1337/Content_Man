using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Extensions;
using FluentAssertions;
using ContentDomain.DocumentContext;
using ContentDomain.ContentContext;
using ContentDomain.Shared;

namespace ContentDomain.Test
{
    public class BasicSectionTest
    {
        [Fact]
        public void should_accept_any_content()
        {
            var section = new BasicSection(1, "name");
            section.AddContent(new ContentElement(-1, Language.Invariant, ContentType.Text));
            section.GetContent().Count().Should().Be(1);
        }

        [Fact]
        public void should_require_id()
        {
            var section = new BasicSection(1, "name");
            section.SectionId.Should().Be(1);
        }

        [Fact]
        public void should_require_name()
        {
            var section = new BasicSection(1, "name");
            section.Name.Should().Be("name");
        }
    }
}
