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
            CreateDefaultContentElement().Should().NotBeNull();
        }

        [Fact]
        public void should_throw_when_adding_null_value()
        {
            Assert.Throws<ArgumentNullException>(
                () => CreateDefaultContentElement().SetValue(null));
        }

        [Fact]
        public void should_accept_value_when_not_null()
        {
            CreateDefaultContentElement().SetValue(new EmptyValueStub());
        }

        [Fact]
        public void should_overwrite_old_value_when_new_one_is_set()
        {
            var newValue = new EmptyValueStub();
            var contentElement = CreateDefaultContentElement();
            contentElement.SetValue(new EmptyValueStub());
            contentElement.SetValue(newValue);
            contentElement.GetValue().Should().Be(newValue);
        }

        private ContentElement<EmptyValueStub> CreateDefaultContentElement()
        {
            return new ContentElement<EmptyValueStub>();
        }

        private class EmptyValueStub : IContentValue
        {
            public ContentStatus Status { get { return ContentStatus.Draft; } }

            public void MarkComplete() { }
        }
    }
}
