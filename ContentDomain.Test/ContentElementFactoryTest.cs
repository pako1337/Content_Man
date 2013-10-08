using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContentDomain.Factories;
using FluentAssertions;
using Xunit;
using Xunit.Extensions;

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
            dynamic dynamicElement = new System.Dynamic.ExpandoObject();
            dynamicElement.ContentElementId = 1;
            dynamicElement.ContentType = (int)ContentType.List;
            dynamicElement.DefaultLanguage = "en";
            dynamicElement.TextContents = new object[0];

            ContentElement contentElement = factory.Create(dynamicElement);

            contentElement.ContentElementId.Should().Be(1);
            contentElement.ContentType.Should().Be(ContentType.List);
            contentElement.DefaultLanguage.Should().Be(Language.Create("en"));
        }

        [Fact]
        public void should_populate_ContentElement_with_text_values()
        {
            dynamic value1 = new System.Dynamic.ExpandoObject();
            value1.TextContentId = 1;
            value1.ContentStatus = (int)ContentStatus.Draft;
            value1.Language = "en";
            value1.Value = "english";
            
            dynamic dynamicElement = new System.Dynamic.ExpandoObject();
            dynamicElement.ContentElementId = 1;
            dynamicElement.ContentType = (int)ContentType.Text;
            dynamicElement.DefaultLanguage = "en";
            dynamicElement.TextContents = new[] { value1 };

            ContentElement contentElement = factory.Create(dynamicElement);
            var values = contentElement.GetValues();

            values.Count().Should().Be(1);
        }

        [Fact]
        public void should_not_throw_when_textContents_are_null()
        {
            dynamic dynamicElement = new System.Dynamic.ExpandoObject();
            dynamicElement.ContentElementId = 1;
            dynamicElement.ContentType = (int)ContentType.List;
            dynamicElement.DefaultLanguage = "en";
            dynamicElement.TextContents = null;

            ContentElement contentElement = factory.Create(dynamicElement);

            contentElement.ContentElementId.Should().Be(1);
            contentElement.ContentType.Should().Be(ContentType.List);
            contentElement.DefaultLanguage.Should().Be(Language.Create("en"));
        }
    }
}
