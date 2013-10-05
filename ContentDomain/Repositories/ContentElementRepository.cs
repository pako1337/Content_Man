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
            var e = Database.Opener
                .OpenConnection("host=localhost;port=5432;database=simple_test;user id=postgres;password=admin;pooling=false", "Npgsql")
                .ContentElement.All();
        }
    }
}
