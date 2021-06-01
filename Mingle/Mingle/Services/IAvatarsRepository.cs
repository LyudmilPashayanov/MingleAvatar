using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mingle.Services
{
    public interface IAvatarsRepository
    {
        Avatar CreateAvatar(Avatar newAvatar);
        IEnumerable<Avatar> GetAllAvatars();

        Avatar GetAvatarFromId(string id);
        void DeleteAvatar(string id);
    }
}
