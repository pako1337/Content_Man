using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Extensions;
using FluentAssertions;

namespace ContentDomain.Test
{
    public class DocumentTest
    {
        [Fact]
        public void should_have_open_status_by_default()
        {
            var doc = new Document();
            doc.Status.Should().Be(DocumentStatus.Open);
        }
    }
}
