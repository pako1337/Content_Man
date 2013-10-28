using System;
using System.Linq;
using Xunit;
using FluentAssertions;
using Xunit.Extensions;
using ContentDomain.ContentContext;

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
                () => CreateDefaultContentElement().AddValue(null));
        }

        [Fact]
        public void should_accept_value_when_not_null()
        {
            CreateDefaultContentElement().AddValue(ValueStub.Create().WithLanguage(Language.Invariant));
        }

        [Fact]
        public void should_overwrite_old_value_when_new_one_is_set()
        {
            var newValue = ValueStub.Create().WithLanguage(Language.Invariant);
            var contentElement = CreateDefaultContentElement();
            contentElement.AddValue(ValueStub.Create().WithLanguage(Language.Invariant));
            contentElement.AddValue(newValue);
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
            Assert.Throws<ArgumentException>(() => contentElement.AddValue(content));
        }

        [Fact]
        public void should_accept_multiple_values_added_when_at_least_one_is_default_language()
        {
            var contentElement = CreateDefaultContentElement();
            var contents = new[]
            {
                ValueStub.Create().WithLanguage(new Language("en")),
                ValueStub.Create().WithLanguage(Language.Invariant)
            };

            contentElement.AddValues(contents);

            var actual = contentElement.GetValues();
            actual.Count().Should().Be(2);
        }

        [Fact]
        public void should_throw_null_exception_when_adding_null_collection()
        {
            Assert.Throws<ArgumentNullException>(() => CreateDefaultContentElement().AddValues(null));
        }

        [Fact]
        public void should_throw_argument_exception_when_adding_collection_with_null()
        {
            var contents = new[]
            {
                ValueStub.Create().WithLanguage(Language.Invariant),
                null
            };

            Assert.Throws<ArgumentException>(() => CreateDefaultContentElement().AddValues(contents));
        }

        [Fact]
        public void should_hold_multiple_values()
        {
            var contentElement = CreateDefaultContentElement();
            var content1 = ValueStub.Create().WithLanguage(Language.Invariant);
            var content2 = ValueStub.Create().WithLanguage(Language.Create("en"));
            contentElement.AddValue(content1);
            contentElement.AddValue(content2);
            contentElement.GetValues().Count().Should().Be(2);
        }

        [Fact]
        public void should_throw_when_trying_to_get_not_existing_value()
        {
            Assert.Throws<ArgumentException>(() => CreateDefaultContentElement().GetValue(Language.Invariant));
        }

        [Theory]
        [ClassData(typeof(ContentTypeFactory))]
        public void should_have_content_type_defined(ContentType type)
        {
            (new ContentElement(0, Language.Invariant, type)).ContentType.Should().Be(type);
        }

        [Fact]
        public void should_throw_argument_exception_when_value_is_of_invalid_type()
        {
            var contentElement = CreateDefaultContentElement();
            var content = ValueStub
                .Create()
                .WithLanguage(Language.Invariant)
                .WithContentType(ContentType.List);
            Assert.Throws<ArgumentException>(() => contentElement.AddValue(content));
        }

        [Fact]
        public void should_update_content_value()
        {
            var contentElement = CreateDefaultContentElement();
            var content = new TextContent(Language.Invariant);
            content.SetValue("before");

            contentElement.AddValue(content);

            content = new TextContent(Language.Invariant);
            content.SetValue("after");

            contentElement.UpdateValue(content);

            TextContent result = (TextContent)contentElement.GetValue(Language.Invariant);
            result.Value.Should().Be("after");
        }

        [Fact]
        public void should_throw_ArgumentException_when_value_to_update_has_different_type()
        {
            var contentElement = CreateDefaultContentElement();
            var content = ValueStub.Create().WithContentType(ContentType.Text).WithLanguage(Language.Invariant);
            contentElement.AddValue(content);

            var updateContent = ValueStub.Create().WithContentType(ContentType.List).WithLanguage(Language.Invariant);
            Assert.Throws<ArgumentException>(() => contentElement.UpdateValue(updateContent));
        }

        private ContentElement CreateDefaultContentElement()
        {
            return new ContentElement(0, Language.Invariant, ContentType.Text);
        }

        private class ValueStub : IContentValue
        {
            private Language _language = null;
            public int ContentValueId { get; private set; }
            public ContentType ContentType { get; private set; }
            public ContentStatus Status { get { return ContentStatus.Draft; } }
            public Language Language { get { return _language; } }
            public void MarkComplete() { }

            public static ValueStub Create()
            {
                return new ValueStub() { ContentType = ContentType.Text };
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

            public void SetValue(object value) { }
            public object GetValue() { return null; }
        }
    }
}
