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
            var e = Database.Open().ContentElement.FindByContentElementId(1);
        }
    }
}
