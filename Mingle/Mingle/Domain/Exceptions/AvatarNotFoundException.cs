using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mingle.Exceptions
{
    public class AvatarNotFoundException : Exception
    {
        public AvatarNotFoundException(string avatarId)
            : base($"No such avatar with that Id \"{avatarId}\" was found.")
        {
        }
    }
}
