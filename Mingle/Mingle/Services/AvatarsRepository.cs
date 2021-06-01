using Mingle.Entinies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mingle.Services
{
    public class AvatarsRepository : IAvatarsRepository
    {
        private List<Avatar> Avatars { get; } = new List<Avatar>();
       
        /// <summary>
        /// It also returns the newly created Avatar, as during creation some fields may change, so we will have the correct created Avatar after POSTing.
        /// </summary>
        /// <param name="newAvatar"></param>
        /// <returns></returns>
        public Avatar CreateAvatar(Avatar newAvatar) 
        {
            foreach (Avatar a in Avatars)
            {
                if (a.Id == newAvatar.Id) 
                {
                    throw new ArgumentException("Avatar with such Id already exists. ", nameof(newAvatar.Id));
                }
            }
            Avatars.Add(newAvatar);
            return newAvatar;
        }
        
        public IEnumerable<Avatar> GetAllAvatars() 
        {
            return Avatars;
        }

        /// <summary>
        /// Returns the event with that Id, if no such Id is available, returns null.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Avatar GetAvatarFromId(string id) 
        {
            return Avatars.FirstOrDefault(a => a.Id == id);
        }

        public void DeleteAvatar(string id) 
        {
            Avatar avatarToDelete = GetAvatarFromId(id);
            if(avatarToDelete != null) 
            {
                Avatars.Remove(avatarToDelete);
            }
            else
            {
                throw new ArgumentException("No Avatar exists with the given Id", nameof(id));
            }
        }
    }
}
