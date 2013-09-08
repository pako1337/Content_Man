using System;
using Xunit;
using FluentAssertions;
using Xunit.Extensions;

namespace ContentDomain.Test
{
    public class ContentElementTest
    {
        [Fact]
        public void should_be_created()
        {
            var contentElement = CreateDefaultContentElement();
            contentElement.Should().NotBeNull();
        }

        [Fact]
        public void should_throw_when_adding_null_value()
        {
            Assert.Throws<ArgumentNullException>(
                () => CreateDefaultContentElement().Add(null));
        }

        [Fact]
        public void should_accept_value_when_not_null()
        {
            CreateDefaultContentElement().Add(new EmptyValueStub());
        }

        [Fact]
        public void should_be_created_in_draft_status()
        {
            CreateDefaultContentElement().Status.Should().Be(ContentStatus.Draft);
        }

        private ContentElement<EmptyValueStub> CreateDefaultContentElement()
        {
            return new ContentElement<EmptyValueStub>();
        }

        private class EmptyValueStub : IContentValue
        {
            public ContentStatus Status { get { return ContentStatus.Draft; } }
        }
    }
}
