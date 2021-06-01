using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mingle.Exceptions
{
    public class AvatarNotFoundException : Exception
    {
        public AvatarNotFoundException()
        {
        }

        public AvatarNotFoundException(string message)
            : base(message)
        {
        }
    }
}
