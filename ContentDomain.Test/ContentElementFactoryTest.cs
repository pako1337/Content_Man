using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContentDomain.Dto;
using ContentDomain.Factories;
using FluentAssertions;
using Xunit;
using Xunit.Extensions;
using ContentDomain.ContentContext;

namespace ContentDomain.Test
{
    public class ContentElementFactoryTest
    {
        private ContentElementFactory factory = new ContentElementFactory();

        [Fact]
        public void should_create_new_ContentElement()
        {
            factory.Create(Language.InvariantCode, ContentType.Text).Should().NotBeNull();
        }

        [Fact]
        public void should_have_id_set_to_minus_1()
        {
            factory.Create(Language.InvariantCode, ContentType.Text).ContentElementId.Should().Be(-1);
        }

        [Theory]
        [ClassData(typeof(ContentTypeFactory))]
        public void should_have_type_set_correctly(ContentType type)
        {
            factory.Create(Language.InvariantCode, type).ContentType.Should().Be(type);
        }

        [Theory]
        [ClassData(typeof(LanguageIsoCodeFactory))]
        public void should_have_language_set_correctly(string languageCode)
        {
            factory.Create(languageCode, ContentType.List).DefaultLanguage.Should().Be(Language.Create(languageCode));
        }

        [Fact]
        public void should_create_ContentElement_based_on_dynamic()
        {
            dynamic dynamicElement = GetSampleDynamicContentElement(ContentType.List);

            ContentElement contentElement = factory.Create(dynamicElement);

            contentElement.ContentElementId.Should().Be(1);
            contentElement.ContentType.Should().Be(ContentType.List);
            contentElement.DefaultLanguage.Should().Be(Language.Create("en"));
        }

        [Fact]
        public void should_populate_ContentElement_with_text_values()
        {
            dynamic inputValue = new System.Dynamic.ExpandoObject();
            inputValue.TextContentId = 1;
            inputValue.ContentStatus = (int)ContentStatus.Draft;
            inputValue.Language = "en";
            inputValue.Value = "english";

            dynamic dynamicElement = GetSampleDynamicContentElement();
            dynamicElement.TextContents = new[] { inputValue };

            ContentElement contentElement = factory.Create(dynamicElement);
            var values = contentElement.GetValues();

            values.Count().Should().Be(1);

            var value = values.First();
            value.ContentValueId.Should().Be(1);
            value.Language.LanguageId.Should().Be("en");
            value.Status.Should().Be(ContentStatus.Draft);
        }

        [Fact]
        public void should_not_throw_when_textContents_are_null()
        {
            ContentElement contentElement = factory.Create(GetSampleDynamicContentElement());
        }

        [Fact]
        public void should_create_ContentElement_based_on_Dto()
        {
            var dto = new ContentElementDto()
            {
                ContentElementId = 1,
                ContentType = ContentType.Text,
                DefaultLanguage = "en",
                TextContents = new List<TextContentDto>
                {
                    new TextContentDto()
                    {
                        ContentElementId = 1,
                        ContentStatus = ContentStatus.Complete,
                        Language = "en",
                        TextContentId = 2,
                        Value = "test"
                    }
                }
            };

            ContentElement ce = factory.Create(dto);

            ce.ContentElementId.Should().Be(1);
            ce.ContentType.Should().Be(ContentType.Text);
            ce.DefaultLanguage.LanguageId.Should().Be("en");
        }

        [Fact]
        public void should_create_TextContent_from_dynamic()
        {
            dynamic inputValue = new System.Dynamic.ExpandoObject();
            inputValue.TextContentId = 1;
            inputValue.ContentStatus = (int)ContentStatus.Draft;
            inputValue.Language = "en";
            inputValue.Value = "english";

            TextContent textContent = factory.CreateTextContent(inputValue);

            textContent.ContentValueId.Should().Be(1);
            textContent.Status.Should().Be(ContentStatus.Draft);
            textContent.Language.LanguageId.Should().Be("en");
            textContent.Value.Should().Be("english");
        }

        private dynamic GetSampleDynamicContentElement(ContentType type = ContentType.Text)
        {
            dynamic dynamicElement = new System.Dynamic.ExpandoObject();
            dynamicElement.ContentElementId = 1;
            dynamicElement.ContentType = (int)type;
            dynamicElement.DefaultLanguage = "en";
            dynamicElement.TextContents = null;
            return dynamicElement;
        }
    }
}
