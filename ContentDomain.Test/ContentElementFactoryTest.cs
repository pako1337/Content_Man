using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using ContentDomain.Factories;

namespace ContentDomain.Test
{
    public class ContentElementFactoryTest
    {
        [Fact]
        public void should_create_new_ContentElement()
        {
            var factory = new ContentElementFactory();
            factory.Create(Language.InvariantCode, ContentType.Text).Should().NotBeNull();
        }

        [Fact]
        public void should_have_id_set_to_minus_1()
        {
            var factory = new ContentElementFactory();
            factory.Create(Language.InvariantCode, ContentType.Text).ContentElementId.Should().Be(-1);
        }

        [Fact]
        public void should_have_type_set_correctly()
        {
            var factory = new ContentElementFactory();
            factory.Create(Language.InvariantCode, ContentType.List).ContentType.Should().Be(ContentType.List);
        }

        [Fact]
        public void should_have_language_set_correctly()
        {
            var factory = new ContentElementFactory();
            factory.Create("en", ContentType.List).DefaultLanguage.Should().Be(Language.Create("en"));
        }
    }
}
