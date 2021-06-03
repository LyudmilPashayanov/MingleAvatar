using Mingle.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mingle.Repository
{
    public interface IAvatarsRepository
    {
        Avatar CreateAvatar(Avatar newAvatar);
        IEnumerable<Avatar> GetAllAvatars();
        Avatar GetAvatarById(string id);
        void DeleteAvatar(string id);
    }
}
