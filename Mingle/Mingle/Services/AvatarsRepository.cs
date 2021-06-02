using Mingle.Entinies;
using System;
using System.Collections.Generic;
using System.Linq;
using Mingle.Exceptions;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Mingle.Services
{
    [Serializable]
    public class AvatarsRepository : IAvatarsRepository
    {
        public List<Avatar> avatars { get; set; } = new List<Avatar>();
       
        /// <summary>
        /// It also returns the newly created Avatar, as during creation some fields may change, so we will have the correct created Avatar after POSTing.
        /// </summary>
        /// <param name="newAvatar"></param>
        /// <returns></returns>
        public Avatar CreateAvatar(Avatar newAvatar) 
        {
            if (avatars.FirstOrDefault(a => a.Id == newAvatar.Id) != null)
            {
                throw new AvatarAlreadyExistsException("Avatar with such Id already exists. ");
            }
            avatars.Add(newAvatar);
            SaveState();
            return newAvatar;
        }
        
        public IEnumerable<Avatar> GetAllAvatars() 
        {
            return avatars;
        }

        /// <summary>
        /// Returns the event with that Id, if no such Id is available, returns null.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Avatar GetAvatarById(string id) 
        {
            return avatars.FirstOrDefault(a => a.Id == id);
        }

        public void DeleteAvatar(string id) 
        {
            Avatar avatarToDelete = GetAvatarById(id);
            if(avatarToDelete != null) 
            {
                avatars.Remove(avatarToDelete);
                SaveState();
            }
            else
            {
                throw new AvatarNotFoundException("No such avatar exists with the given Id.");
            }
        }

        private void SaveState()
        {
            MingleSerializer.SerializeAvatars(this);
        }
    }
}
