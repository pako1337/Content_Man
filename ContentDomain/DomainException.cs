using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContentDomain
{
    public class DomainException : Exception
    {
        public DomainException(string message)
            : base(message)
        {
        }
    }
}
