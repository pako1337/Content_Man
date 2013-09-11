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
            CreateEmptyTextContent().Status.Should().Be(ContentStatus.Draft);
        }

        [Fact]
        public void should_change_status_to_complete_on_mark_complete()
        {
            var content = CreateEmptyTextContent();
            content.MarkComplete();
            content.Status.Should().Be(ContentStatus.Complete);
        }

        [Fact]
        public void should_hold_text_value()
        {
            var content = CreateEmptyTextContent();
            content.SetValue("text");
            content.Value.Should().Be("text");
        }

        [Fact]
        public void should_reset_status_to_draft_after_content_changed()
        {
            var content = CreateEmptyTextContent();
            content.MarkComplete();
            content.SetValue("text");
            content.Status.Should().Be(ContentStatus.Draft);
        }

        [Fact]
        public void should_not_reset_status_to_draft_after_content_changed_to_the_same_value()
        {
            var content = CreateEmptyTextContent();
            content.SetValue("text");
            content.MarkComplete();
            content.SetValue("text");
            content.Status.Should().Be(ContentStatus.Complete);
        }

        [Fact]
        public void should_have_language_assigned_at_all_times()
        {
            CreateEmptyTextContent().Language.Should().NotBeNull();
        }

        private TextContent CreateEmptyTextContent()
        {
            return new TextContent(new Language());
        }
    }
}
