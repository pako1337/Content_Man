using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Extensions;
using FluentAssertions;
using ContentDomain.Factories;

namespace ContentDomain.Test
{
    public class DocumentTest
    {
        [Fact]
        public void should_have_open_status_by_default()
        {
            var doc = CreateDocument();
            doc.Status.Should().Be(DocumentStatus.Open);
        }

        [Fact]
        public void should_hold_content()
        {
            var doc = CreateDocument();
            var content = CreateNewContentElement();
            doc.AddContent(content);
            doc.GetContent().Count().Should().Be(1);
        }

        [Fact]
        public void should_hold_element_added()
        {
            var doc = CreateDocument();
            var content = CreateNewContentElement();
            doc.AddContent(content);
            var contentElements = doc.GetContent();
            contentElements.First().ContentElementId.Should().Be(content.ContentElementId);
        }

        [Fact]
        public void should_have_name()
        {
            var doc = CreateDocument("Test name");
            doc.Name.Should().Be("Test name");
        }

        private static ContentElement CreateNewContentElement()
        {
            return new ContentElementFactory().Create(Language.InvariantCode, ContentType.Text);
        }

        private static Document CreateDocument(string name = "test")
        {
            return new Document(name);
        }
    }
}
