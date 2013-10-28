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
            doc.AddContent(content, 1);
            doc.GetContent().Count().Should().Be(1);
        }

        [Fact]
        public void should_hold_element_added()
        {
            var doc = CreateDocument();
            var content = CreateNewContentElement();
            doc.AddContent(content, 1);
            var contentElements = doc.GetContent();
            contentElements.First().ContentElementId.Should().Be(content.ContentElementId);
        }

        [Fact]
        public void should_have_at_least_one_section()
        {
            CreateDocument().GetSections().Count.Should().BeGreaterOrEqualTo(1);
        }

        [Fact]
        public void should_throw_ArgumentException_when_no_section_provided()
        {
            Assert.Throws<ArgumentException>(() => new Document(-1, "test", new List<IDocumentSection>()));
        }

        [Fact]
        public void should_throw_ArgumentNullException_when_name_not_provided()
        {
            Assert.Throws<ArgumentNullException>(() => new Document(-1, null, new[] { new SectionStub(1) }));
        }

        [Fact]
        public void should_throw_ArgumentException_when_name_is_empty()
        {
            Assert.Throws<ArgumentException>(() => new Document(-1, "", new[] { new SectionStub(1) }));
        }

        [Fact]
        public void should_accept_new_section()
        {
            var doc = CreateDocument();
            doc.AddSection(new SectionStub(1).WithName("new name"));
            doc.GetSections().Count().Should().Be(2);
        }

        [Fact]
        public void should_throw_ArgumentException_when_adding_section_with_existing_section_name()
        {
            var doc = CreateDocument();
            Assert.Throws<ArgumentException>(() => doc.AddSection(new SectionStub(1)));
        }

        private static ContentElement CreateNewContentElement()
        {
            return new ContentElementFactory().Create(Language.InvariantCode, ContentType.Text);
        }

        private static Document CreateDocument(string name = "test")
        {
            return new Document(-1, name, new[] { new SectionStub(1) });
        }

        private class SectionStub : IDocumentSection
        {
            public const string DefaultName = "section name";

            private List<ContentElement> _content = new List<ContentElement>();
            private int _id;
            private string _name = DefaultName;

            public int SectionId { get { return _id; } }
            public string Name { get { return _name; } }

            public SectionStub(int id)
            {
                _id = id;
            }

            public void AddContent(ContentElement content)
            {
                _content.Add(content);
            }

            public IImmutableList<ContentElement> GetContent()
            {
                return ImmutableList.CreateRange(_content);
            }

            public SectionStub WithName(string name)
            {
                _name = name;
                return this;
            }
        }
    }
}
