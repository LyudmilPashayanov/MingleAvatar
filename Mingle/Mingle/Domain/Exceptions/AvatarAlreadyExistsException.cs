using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mingle.Exceptions
{
    public class AvatarAlreadyExistsException : Exception
    {
        public AvatarAlreadyExistsException(string avatarId)
            : base($"Avatar with such Id \"{avatarId}\" already exists.")
        {
        }
    }
}
