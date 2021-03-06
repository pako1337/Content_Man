﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using Xunit.Extensions;
using ContentDomain.Shared;

namespace ContentDomain.Test
{
    public class LanguageTest
    {
        [Theory]
        [ClassData(typeof(LanguageIsoCodeFactory))]
        public void should_be_equal_when_the_same_code(string langCode)
        {
            var language1 = Language.Create(langCode);
            var language2 = Language.Create(langCode);

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
            language1.LanguageId.Should().Be(langCode);
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

        [Theory]
        [ClassData(typeof(LanguageIsoCodeFactory))]
        public void should_fill_name(string langCode)
        {
            var language = Language.Create(langCode);
            language.Name.Should().NotBeNull();
        }

        [Fact]
        public void should_fill_language_name_in_english()
        {
            var language = Language.Create("en");
            language.Name.Should().Be("English");
        }

        [Fact]
        public void should_return_false_when_comparing_language_with_other_type()
        {
            Language.Invariant.Equals(1).Should().BeFalse();
        }

        [Fact]
        public void should_return_false_when_comparing_to_different_language()
        {
            Language.Invariant.Equals(Language.Create("en")).Should().BeFalse();
        }

        [Fact]
        public void should_return_invariant_language_by_static_property()
        {
            Language.Invariant.LanguageId.Should().Be(Language.InvariantCode);
        }
    }
}
