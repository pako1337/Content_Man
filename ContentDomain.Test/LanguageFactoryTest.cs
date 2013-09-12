using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using Xunit.Extensions;

namespace ContentDomain.Test
{
    public class LanguageFactoryTest
    {
        [Fact]
        public void should_create_language_for_give_iso_code()
        {
            var factory = new LanguageFactory();
            var language1 = factory.CreateLanguage("pl-PL");
            language1.Should().NotBeNull();
        }

        [Theory]
        [ClassData(typeof(LanguageIsoCodeFactory))]
        public void should_create_language_with_iso_code_filled(string langCode)
        {
            var factory = new LanguageFactory();
            var language1 = factory.CreateLanguage(langCode);
            language1.IsoCode.Should().Be(langCode);
        }

        [Fact]
        public void should_throw_when_unknown_language_passed()
        {
            var factory = new LanguageFactory();
            Assert.Throws<ArgumentException>(() => factory.CreateLanguage("abc"));
        }

        [Fact]
        public void should_always_return_the_same_instance_for_the_same_iso_code()
        {
            var factory = new LanguageFactory();
            var language1 = factory.CreateLanguage("pl-PL");
            var language2 = factory.CreateLanguage("pl-PL");
            language1.Should().Be(language2);
        }
    }
}
