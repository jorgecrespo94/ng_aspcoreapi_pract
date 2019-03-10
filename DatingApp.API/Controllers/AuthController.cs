using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController] // automatically validate and inferre model information through the request
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        public AuthController(IAuthRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userDTO)
        {
            /*
                if(!ModelState.IsValid)
                return BadRequest(ModelState);

                Or 

                [ApiController]
             */
            
            userDTO.Username = userDTO.Username.ToLower();

            if(await _repo.UserExists(userDTO.Username))
                return BadRequest("Username already exists");

            var userToCreate = new User
            {
                Username =  userDTO.Username
            };

            var createdUser = await _repo.Register(userToCreate, userDTO.Username);

            return StatusCode(201);
        }
    }
}