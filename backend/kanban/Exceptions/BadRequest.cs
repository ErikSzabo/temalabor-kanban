using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kanban.Exceptions
{
    public class BadRequest : Exception
    {
        public BadRequest(string message) : base(message)
        {
        }
    }
}
