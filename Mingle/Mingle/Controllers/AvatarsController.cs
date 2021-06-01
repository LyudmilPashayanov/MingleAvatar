using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mingle.Entinies;
using Mingle.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mingle.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvatarsController : ControllerBase
    {
        private readonly IAvatarsRepository avatarsRepository;

        /// <summary>
        /// Constructed by following the Dependency Injection pattern.
        /// </summary>
        /// <param name="avatarsRepository"> Injected from Startup.cs - ConfigureServices().</param>
        public AvatarsController(IAvatarsRepository avatarsRepository) 
        {
            this.avatarsRepository = avatarsRepository;
        }

        /// <summary>
        /// Returns all currently available avatars.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type= typeof(IEnumerable<Avatar>))]
        public IActionResult GetAll() 
        {
            return Ok(avatarsRepository.GetAllAvatars());
        }

        /// <summary>
        /// Returns the avatar with that Id. If no such Id is available, returns null.
        /// </summary>
        [HttpGet("GetById", Name = nameof(GetById))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Avatar))]

        public IActionResult GetById(string IdToGet)
        {
            Avatar existingAvatar = avatarsRepository.GetAvatarFromId(IdToGet);
            if(existingAvatar == null) 
            {
                return NotFound();
            }
            return Ok(existingAvatar);
        }

        /// <summary>
        /// Creates a new avatar - Requirements: Id can not be null or empty.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Avatar))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]

        public IActionResult AddAvatar([FromBody] Avatar newAvatar) 
        {
            try
            {
                if (string.IsNullOrEmpty(newAvatar.Id) )
                {
                    return BadRequest("Please specify an Id: Id can not be empty.");
                }
                avatarsRepository.CreateAvatar(newAvatar);
            }
            catch (ArgumentException) 
            {
                return BadRequest(string.Format("Avatar with Id \"{0}\" already exists. Please type in another Id.", newAvatar.Id));
            }

            return CreatedAtAction("GetById", new { id = newAvatar.Id }, newAvatar);
        }

        /// <summary>
        /// Deletes an already existing Avatar.
        /// </summary>
        [HttpDelete]
        [Route("{IdToDelete}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult DeveleAvatar(string IdToDelete) 
        {
            try 
            {
                avatarsRepository.DeleteAvatar(IdToDelete);            
            }
            catch(ArgumentException argEx)
            {
                return NotFound(argEx);
            }

            return NoContent();
        }
    }
}
