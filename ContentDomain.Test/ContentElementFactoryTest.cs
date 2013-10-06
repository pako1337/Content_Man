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
            factory.Create().Should().NotBeNull();
        }
    }
}
