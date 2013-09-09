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
        [Fact]
        public void should_return_the_same_object_when_getting_invariant_language()
        {
            var lang1 = Language.InvariantLanguage();
            var lang2 = Language.InvariantLanguage();
            lang1.Should().Be(lang2);
        }
    }
}
