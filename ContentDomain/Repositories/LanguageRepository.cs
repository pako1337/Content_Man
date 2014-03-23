using System.Collections.Generic;
using System.Linq;
using ContentDomain.Shared;
using Simple.Data;
using ContentDomain.Dto;

namespace Content_Man.Web.Api
{
    public class LanguageRepository
    {
        public IEnumerable<Language> GetLanguages()
        {
            var database = Database.Open();
            List<LanguageDto> languages = database.Languages.All().OrderByLanguageId();

            foreach (var language in languages)
                yield return Language.Create(language.LanguageId);
        }
    }
}
