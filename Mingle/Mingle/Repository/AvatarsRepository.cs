using Mingle.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Mingle.Exceptions;

namespace Mingle.Repository
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
            if (GetAllAvatars().FirstOrDefault(a => a.Id == newAvatar.Id) != null)
            {
                throw new AvatarAlreadyExistsException(newAvatar.Id);
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
            Avatar found = avatars.FirstOrDefault(a => a.Id == id);
            if(found != null) 
            {
                return found;
            }
            throw new AvatarNotFoundException(id);
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
                throw new AvatarNotFoundException(id);
            }
        }

        private void SaveState()
        {
            MingleSerializer.SerializeAvatars(this);
        }
    }
}
