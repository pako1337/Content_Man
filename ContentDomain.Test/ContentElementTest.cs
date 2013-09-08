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

        [Fact]
        public void should_throw_when_adding_null_value()
        {
            Assert.Throws<ArgumentNullException>(
                () => GetDefaultContentElement().Add(null));
        }

        [Fact]
        public void should_accept_value_when_not_null()
        {
            GetDefaultContentElement().Add(new EmptyValueStub());
        }

        private ContentElement<EmptyValueStub> GetDefaultContentElement()
        {
            return new ContentElement<EmptyValueStub>();
        }

        private class EmptyValueStub : IContentValue
        { }
    }
}
