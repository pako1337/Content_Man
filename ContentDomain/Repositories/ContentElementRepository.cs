using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple.Data;

namespace ContentDomain.Repositories
{
    public class ContentElementRepository
    {
        public ContentElementRepository()
        {
            List<ContentElement> e = Database.Open().ContentElements.All().WithLanguages();
        }
    }
}
