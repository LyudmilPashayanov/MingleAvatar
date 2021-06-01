using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mingle.Exceptions
{
    public class AvatarAlreadyExistsException : Exception
    {
        public AvatarAlreadyExistsException()
        {
        }

        public AvatarAlreadyExistsException(string message)
            : base(message)
        {
        }
    }
}
