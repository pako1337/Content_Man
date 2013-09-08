using System;
using FluentAssertions;
using Xunit;
using Xunit.Extensions;

namespace ContentDomain.Test
{
    public class TextContentTest
    {
        [Fact]
        public void should_be_created_in_draft_status()
        {
            var content = new TextContent();
            content.Status.Should().Be(ContentStatus.Draft);
        }

        [Fact]
        public void should_change_status_to_complete_on_mark_complete()
        {
            var content = new TextContent();
            content.MarkComplete();
            content.Status.Should().Be(ContentStatus.Complete);
        }
    }
}
