using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IAvatarsRepository repository;

        /// <summary>
        /// Constructed by following the Dependency Injection pattern. Injected from Startup.cs - ConfigureServices().
        /// </summary>
        /// <param name="avatarsRepository"></param>
        public AvatarsController(IAvatarsRepository avatarsRepository) 
        {
            this.repository = avatarsRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type= typeof(IEnumerable<Avatar>))]
        public IActionResult GetAll() 
        {
            return Ok(repository.GetAllAvatars());
        }

        [HttpGet("GetById", Name = nameof(GetById))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Avatar))]

        public IActionResult GetById(string IdToGet)
        {
            Avatar existingAvatar = repository.GetAvatarFromId(IdToGet);
            if(existingAvatar == null) 
            {
                return NotFound();
            }
            return Ok(existingAvatar);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Avatar))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]

        public IActionResult AddAvatar([FromBody] Avatar newAvatar) 
        {
            try
            {
                if (string.IsNullOrEmpty(newAvatar.Id) )
                {
                    return BadRequest("Please specify an Id- Id can not be empty.");
                }
                repository.CreateAvatar(newAvatar);
            }
            catch (ArgumentException) 
            {
                return BadRequest(string.Format("Avatar with Id \"{0}\" already exists. Please type in another Id.", newAvatar.Id));
            }

            return CreatedAtAction("GetById", new { id = newAvatar.Id }, newAvatar);
        }

        [HttpDelete]
        [Route("{IdToDelete}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult DeveleAvatar(string IdToDelete) 
        {
            try 
            {
                repository.DeleteAvatar(IdToDelete);            
            }
            catch(ArgumentException argEx)
            {
                return NotFound(argEx);
            }

            return NoContent();
        }
    }
}
