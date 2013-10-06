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
    }
}
