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
    public class LanguageTest
    {
        [Theory]
        [ClassData(typeof(LanguageIsoCodeFactory))]
        public void should_be_equal_when_the_same_code(string langCode)
        {
            var language1 = new Language(langCode);
            var language2 = new Language(langCode);

            language1.Should().Be(language2);
        }

        [Theory]
        [ClassData(typeof(LanguageIsoCodeFactory))]
        public void should_create_language_for_give_iso_code(string langCode)
        {
            var language1 = Language.Create(langCode);
            language1.Should().NotBeNull();
        }

        [Theory]
        [ClassData(typeof(LanguageIsoCodeFactory))]
        public void should_create_language_with_iso_code_filled(string langCode)
        {
            var language1 = Language.Create(langCode);
            language1.IsoCode.Should().Be(langCode);
        }

        [Fact]
        public void should_throw_when_unknown_language_passed()
        {
            Assert.Throws<ArgumentException>(() => Language.Create("abc"));
        }

        [Theory]
        [ClassData(typeof(LanguageIsoCodeFactory))]
        public void should_always_return_the_same_instance_for_the_same_iso_code(string langCode)
        {
            var language1 = Language.Create(langCode);
            var language2 = Language.Create(langCode);
            language1.Should().Be(language2);
        }
    }
}
