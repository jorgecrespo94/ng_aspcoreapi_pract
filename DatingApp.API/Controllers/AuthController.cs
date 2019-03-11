using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController] // automatically validate and inferre model information through the request
  public class AuthController : ControllerBase
  {
    private readonly IAuthRepository _repo;
    private readonly IConfiguration _config;
    public AuthController(IAuthRepository repo, IConfiguration config)
    {
      _repo = repo;
      _config = config;
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

      if (await _repo.UserExists(userDTO.Username))
        return BadRequest("Username already exists");

      var userToCreate = new User
      {
        Username = userDTO.Username
      };

      var createdUser = await _repo.Register(userToCreate, userDTO.Password);

      return StatusCode(201);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
    {
      User userFromRepo = await GetDatabaseUser(userForLoginDto);

      if (ObjNotFound(userFromRepo))
        return Unauthorized();

      var loginClaims = GetLoginClaims(userFromRepo);

      var creds = GetSigningCredentials();

      var tokenHandler = new JwtSecurityTokenHandler();

      var token = GetLoginToken(tokenHandler, loginClaims, creds);

      return Ok(new { token = tokenHandler.WriteToken(token) });
    }

    private async Task<User> GetDatabaseUser(UserForLoginDto userForLoginDto)
    {
      return await _repo.Login(userForLoginDto.Username.ToLower(), userForLoginDto.Password);
    }

    private bool ObjNotFound(object obj)
    {
      return obj == null;
    }

    private Claim[] GetLoginClaims(User userFromRepo)
    {
      return new[]
      {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.Username),
      };
    }

    private SigningCredentials GetSigningCredentials()
    {
       var key = new SymmetricSecurityKey(Encoding.UTF8
        .GetBytes(_config.GetSection("AppSettings:Token").Value));//encode key and add to appsetings
       
       return new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
    }

    private SecurityToken GetLoginToken(JwtSecurityTokenHandler tokenHandler, Claim[] loginClaims, SigningCredentials creds)
    {
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims: loginClaims),
            Expires = DateTime.Now.AddDays(1),
            SigningCredentials = creds
        };

        return tokenHandler.CreateToken(tokenDescriptor);
    }
  }//end class
}//end namespace