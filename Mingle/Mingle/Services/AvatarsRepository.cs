using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mingle.Services
{
    public class Avatar 
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public float Shoesize { get; set; }
        public bool Color { get; set; }
        public bool CanMineUnobtainium { get; set; }
    }

    public class AvatarsRepository
    {
        private List<Avatar> Avatars { get; } = new List<Avatar>();
       
        /// <summary>
        /// It also returns the newly created Avatar, as during creation some fields may change, so we will have the correct created Avatar after POSTing.
        /// </summary>
        /// <param name="newAvatar"></param>
        /// <returns></returns>
        public Avatar Create(Avatar newAvatar) 
        {
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
