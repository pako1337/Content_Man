﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContentDomain.Factories;
using Simple.Data;

namespace ContentDomain.Repositories
{
    public class ContentElementRepository
    {
        ContentElementFactory factory = new ContentElementFactory();

        public IEnumerable<ContentElement> All()
        {
            var db = Database.Open();
            List<ContentElementDb> dbElements = db.ContentElements.All().WithTextContents();

            var elements = new List<ContentElement>();
            foreach (var element in dbElements)
                elements.Add(factory.Create(element));

            return elements;
        }

        public ContentElement Get(int contentElementId)
        {
            var db = Database.Open();
            List<ContentElementDb> elements = db.ContentElements
                .FindAllByContentElementId(contentElementId)
                .WithTextContents();
            return factory.Create(elements.FirstOrDefault());
        }

        public void Insert(ContentElement contentElement)
        {
            var contentElementDb = new ContentElementDb(contentElement);
            var db = Database.Open();
            using (var tx = db.BeginTransaction())
            {
                var ce = tx.ContentElements.Insert(contentElementDb);
                foreach (var textContent in contentElementDb.TextContents)
                {
                    textContent.ContentElementId = ce.ContentElementId;
                    tx.TextContents.Insert(textContent);
                }

                tx.Commit();
            }
        }
    }

    internal class ContentElementDb
    {
        public int ContentElementId { get; set; }
        public ContentType ContentType { get; set; }
        public string DefaultLanguage { get; set; }
        public List<TextContentDb> TextContents { get; set; }

        public ContentElementDb()
        { }

        public ContentElementDb(ContentElement ce)
        {
            ContentElementId = ce.ContentElementId;
            ContentType = ce.ContentType;
            DefaultLanguage = ce.DefaultLanguage.LanguageId;
            TextContents = ce
                .GetValues()
                .OfType<TextContent>()
                .Select(v => new TextContentDb(v, this))
                .ToList();
        }
    }

    internal class TextContentDb
    {
        public int TextContentId { get; set; }
        public int ContentElementId { get; set; }
        public ContentStatus ContentStatus { get; set; }
        public string Value { get; set; }
        public string Language { get; set; }

        public TextContentDb()
        { }

        public TextContentDb(TextContent tc, ContentElementDb ce)
        {
            TextContentId = tc.ContentValueId;
            ContentElementId = ce.ContentElementId;
            ContentStatus = tc.Status;
            Value = tc.Value;
            Language = tc.Language.LanguageId;
        }
    }
}
