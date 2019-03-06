using System;
using System.Threading.Tasks;
using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
  public class AuthRepository : IAuthRepository
  {
    private readonly DataContext _context;
    public AuthRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<User> Login(string username, string password)
    {
       //get user from db
       var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);

       //stop if user not found
       if(user == null)
        return null;

      //verify if given password matches
      if(!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
        return null;

      return user;  
    }

    //created a new hash with the users stored key
    //then compare both have to see if they are equal.
    private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
       byte[] computedHash = null;

       using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
        {
          computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }

        return (IsPasswordHashEquals(passwordHash, computedHash));
    }

    private bool IsPasswordHashEquals(byte[] passwordHash, byte[] computedHash)
    {
        for(int i = 0; i < computedHash.Length; i++)          
          if(computedHash[i] != passwordHash[i]) return false;
          
        return true;//all byte are equal
    }

    public async Task<User> Register(User user, string password)
    {
        //convert password to password hash/salt
        byte[] passswordHash, passwordSalt;
        CreatePasswordHash(password, out passswordHash, out passwordSalt);
        
        //get values
        user.PasswordHash = passswordHash;
        user.PasswordSalt = passwordSalt;

        //save values
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        
        return user;
    }

    //create password hash with random salt
    private void CreatePasswordHash(string password, out byte[] passswordHash, out byte[] passwordSalt)
    {
        using (var hmac = new System.Security.Cryptography.HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }

    //return whether or not the username is present on the database
    public async Task<bool> UserExists(string username, string password)
    {
      return (await _context.Users.AnyAsync( x=> x.Username == username));
    }

  }
}