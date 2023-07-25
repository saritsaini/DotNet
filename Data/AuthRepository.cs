using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DotNet.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace DotNet.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        public AuthRepository(DataContext context, IConfiguration configuration )
        {
            _configuration = configuration;
            _context = context;

        }
        public async Task<bool> IsUserExists(string userName)
        {
            if (await _context.Users.AnyAsync<User>(u => u.UserName.ToLower() == userName.ToLower()))
            {
                return true;
            }
            return false;
        }

        public async Task<ServiceResponse<string>> Login(string userName, string password)
        {
            var serviceResponse=new ServiceResponse<string>();
            var userDetails=await _context.Users.FirstOrDefaultAsync(u=>u.UserName.ToLower()==userName.ToLower());
            if(userDetails is null) {
                serviceResponse.Success=false;
                serviceResponse.Message="Username doesn't exists";
                return serviceResponse;
            }
            else {
                if(VerifyPassword(password,userDetails.passwordhash,userDetails.passwordSalt)) {
                    serviceResponse.Success=true;
                    serviceResponse.Data=CreateToken(userDetails);
                    serviceResponse.Message="User loggedin successfully";
                    return serviceResponse;
                }
                else {
                    serviceResponse.Success=false;
                    serviceResponse.Message="username and password mismatched";
                    return serviceResponse;
                }
            }
        }

        public async Task<ServiceResponse<int>> Register(User user, string password)
        {
            var serviceResponse = new ServiceResponse<int>();
            if(await IsUserExists(user.UserName)){
                serviceResponse.Message="User already exists";
                serviceResponse.Success=false;
                return serviceResponse;
            }
            CreateHashedPassword(password, out byte[] passwordHashed, out byte[] passwordSalt);
            user.passwordhash = passwordHashed;
            user.passwordSalt = passwordSalt;
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            serviceResponse.Data = user.Id;
            return serviceResponse;
        }

        private void CreateHashedPassword(string password, out byte[] passwordHashed, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHashed = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        private bool VerifyPassword(string password, byte[] passwordHashed, byte[] passwordSalt) {
            using(var hmac=new System.Security.Cryptography.HMACSHA512(passwordSalt)){
             var computeHash=hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
             return computeHash.SequenceEqual(passwordHashed);
            }
        }
        private string CreateToken(User user) {
            var claims=new List<Claim> {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString())
            };
            var appsettingToken=_configuration.GetSection("AppSettings:Token").Value;
            if(appsettingToken is null)
                 throw new Exception("appsettings is null!");
            
            SymmetricSecurityKey key= new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(appsettingToken));

            SigningCredentials cred= new SigningCredentials(key,SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor=new SecurityTokenDescriptor{
                Subject=new ClaimsIdentity(claims),
                Expires=DateTime.Now.AddDays(1),
                SigningCredentials=cred
            };
            JwtSecurityTokenHandler tokenHandler=new JwtSecurityTokenHandler();
            SecurityToken token= tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}