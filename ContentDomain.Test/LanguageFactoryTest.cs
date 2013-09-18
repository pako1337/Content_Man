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
        [Theory]
        [ClassData(typeof(LanguageIsoCodeFactory))]
        public void should_create_language_for_give_iso_code(string langCode)
        {
            var language1 = Language.CreateLanguage(langCode);
            language1.Should().NotBeNull();
        }

        [Theory]
        [ClassData(typeof(LanguageIsoCodeFactory))]
        public void should_create_language_with_iso_code_filled(string langCode)
        {
            var language1 = Language.CreateLanguage(langCode);
            language1.IsoCode.Should().Be(langCode);
        }

        [Fact]
        public void should_throw_when_unknown_language_passed()
        {
            Assert.Throws<ArgumentException>(() => Language.CreateLanguage("abc"));
        }

        [Theory]
        [ClassData(typeof(LanguageIsoCodeFactory))]
        public void should_always_return_the_same_instance_for_the_same_iso_code(string langCode)
        {
            var language1 = Language.CreateLanguage(langCode);
            var language2 = Language.CreateLanguage(langCode);
            language1.Should().Be(language2);
        }
    }
}
