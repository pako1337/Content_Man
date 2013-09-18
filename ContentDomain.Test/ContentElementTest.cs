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
            CreateDefaultContentElement().SetValue(ValueStub.Create().WithLanguage(Language.CreateLanguage(Language.Invariant)));
        }

        [Fact]
        public void should_overwrite_old_value_when_new_one_is_set()
        {
            var newValue = ValueStub.Create().WithLanguage(Language.CreateLanguage(Language.Invariant));
            var contentElement = CreateDefaultContentElement();
            contentElement.SetValue(ValueStub.Create().WithLanguage(Language.CreateLanguage(Language.Invariant)));
            contentElement.SetValue(newValue);
            contentElement.GetValue().Should().Be(newValue);
        }

        [Fact]
        public void should_have_default_language()
        {
            CreateDefaultContentElement().DefaultLanguage.Should().NotBeNull();
        }

        [Fact]
        public void should_throw_when_first_value_added_is_not_of_default_language()
        {
            var contentElement = CreateDefaultContentElement();
            var content = ValueStub.Create().WithLanguage(new Language("en"));
            Assert.Throws<ArgumentException>(() => contentElement.SetValue(content));
        }

        private ContentElement<ValueStub> CreateDefaultContentElement()
        {
            return new ContentElement<ValueStub>(Language.CreateLanguage(Language.Invariant));
        }
        
        private class ValueStub : IContentValue
        {
            private Language _language = null;
            public int Id { get; private set; }
            public ContentStatus Status { get { return ContentStatus.Draft; } }
            public Language Language { get { return _language; } }
            public void MarkComplete() { }

            public static ValueStub Create()
            {
                return new ValueStub();
            }

            public ValueStub WithLanguage(Language language)
            {
                _language = language;
                return this;
            }
        }
    }
}
