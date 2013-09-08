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
            var contentElement = GetDefaultContentElement();
            contentElement.Should().NotBeNull();
        }

        [Theory]
        [InlineData(ContentType.Text)]
        [InlineData(ContentType.List)]
        [InlineData(ContentType.Graphic)]
        public void should_have_type_assigned(ContentType type)
        {
            var contentElement = GetContentElementWithType(type);
            contentElement.Type.Should().Be(type);
        }

        private ContentElement GetDefaultContentElement()
        {
            return GetContentElementWithType(ContentType.Text);
        }

        private ContentElement GetContentElementWithType(ContentType type)
        {
            return new ContentElement(type);
        }
    }
}
