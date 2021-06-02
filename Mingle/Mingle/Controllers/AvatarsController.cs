using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mingle.Entinies;
using Mingle.Services;
using System;
using Mingle.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

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
        public IActionResult GetAllAvatars() 
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
            try
            {
                Avatar existingAvatar = avatarsRepository.GetAvatarById(IdToGet);
                return Ok(existingAvatar);
            }
            catch(AvatarNotFoundException ex) 
            {
                return NotFound(ex.Message);
            }
            
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
            catch (AvatarAlreadyExistsException arg) 
            {
                return BadRequest(arg.Message);
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
        public IActionResult DeleteAvatar(string IdToDelete) 
        {
            try 
            {
                avatarsRepository.DeleteAvatar(IdToDelete);            
            }
            catch(AvatarNotFoundException arg)
            {
                return NotFound(arg.Message);
            }
            return NoContent();
        }
    }
}
