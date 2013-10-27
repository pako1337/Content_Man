using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Extensions;
using FluentAssertions;
using ContentDomain.Factories;
using System.Collections.Immutable;

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
        public void should_have_name()
        {
            var doc = CreateDocument("Test name");
            doc.Name.Should().Be("Test name");
        }

        [Fact]
        public void should_hold_content()
        {
            var doc = CreateDocument();
            var content = CreateNewContentElement();
            doc.AddContent(content, SectionStub.StubSectionId);
            doc.GetContent().Count().Should().Be(1);
        }

        [Fact]
        public void should_hold_element_added()
        {
            var doc = CreateDocument();
            var content = CreateNewContentElement();
            doc.AddContent(content, SectionStub.StubSectionId);
            var contentElements = doc.GetContent();
            contentElements.First().ContentElementId.Should().Be(content.ContentElementId);
        }

        [Fact]
        public void should_have_at_least_one_section()
        {
            CreateDocument().GetSections().Count.Should().BeGreaterOrEqualTo(1);
        }

        private static ContentElement CreateNewContentElement()
        {
            return new ContentElementFactory().Create(Language.InvariantCode, ContentType.Text);
        }

        private static Document CreateDocument(string name = "test")
        {
            return new Document(-1, name, new[] { new SectionStub() });
        }

        private class SectionStub : IDocumentSection
        {
            public const int StubSectionId = 1;
            private List<ContentElement> _content = new List<ContentElement>();

            public int SectionId { get { return StubSectionId; } }

            public void AddContent(ContentElement content)
            {
                _content.Add(content);
            }

            public IImmutableList<ContentElement> GetContent()
            {
                return ImmutableList.CreateRange(_content);
            }
        }
    }
}
