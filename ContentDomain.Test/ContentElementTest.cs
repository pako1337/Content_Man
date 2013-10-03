using System;
using System.Linq;
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
            CreateDefaultContentElement().SetValue(ValueStub.Create().WithLanguage(Language.Invariant));
        }

        [Fact]
        public void should_overwrite_old_value_when_new_one_is_set()
        {
            var newValue = ValueStub.Create().WithLanguage(Language.Invariant);
            var contentElement = CreateDefaultContentElement();
            contentElement.SetValue(ValueStub.Create().WithLanguage(Language.Invariant));
            contentElement.SetValue(newValue);
            contentElement.GetValue(Language.Invariant).Should().Be(newValue);
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

        [Fact]
        public void should_hold_multiple_values()
        {
            var contentElement = CreateDefaultContentElement();
            var content1 = ValueStub.Create().WithLanguage(Language.Invariant);
            var content2 = ValueStub.Create().WithLanguage(Language.Create("en"));
            contentElement.SetValue(content1);
            contentElement.SetValue(content2);
            contentElement.GetValues().Count().Should().Be(2);
        }

        [Fact]
        public void should_throw_when_trying_to_get_not_existing_value()
        {
            Assert.Throws<ArgumentException>(() => CreateDefaultContentElement().GetValue(Language.Invariant));
        }

        [Fact]
        public void should_have_content_type_defined()
        {
            (new ContentElement(0, Language.Invariant, ContentType.List)).ContentType.Should().Be(ContentType.List);
        }

        [Fact]
        public void should_throw_argument_exception_when_value_is_of_invalid_type()
        {
            var contentElement = CreateDefaultContentElement();
            var content = ValueStub
                .Create()
                .WithLanguage(Language.Invariant)
                .WithContentType(ContentType.List);
            Assert.Throws<ArgumentException>(() => contentElement.SetValue(content));
        }

        private ContentElement CreateDefaultContentElement()
        {
            return new ContentElement(0, Language.Invariant, ContentType.Text);
        }
        
        private class ValueStub : IContentValue
        {
            private Language _language = null;
            public int Id { get; private set; }
            public ContentType ContentType { get; private set; }
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

            public ValueStub WithContentType(ContentType contentType)
            {
                ContentType = contentType;
                return this;
            }
        }
    }
}
