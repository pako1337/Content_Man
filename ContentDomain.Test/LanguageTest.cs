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
    }
}
