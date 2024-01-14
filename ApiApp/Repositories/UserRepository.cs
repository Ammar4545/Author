using ApiApp.Models;
using ApiApp.Models.Entities;
using ApiApp.Models.RequestDTO;
using ApiApp.Models.ResponseDTO;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ApiApp.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly LibraryDbContext dbcontext;
        private readonly IMapper mapper;
        private readonly IConfiguration config;

        public UserRepository(LibraryDbContext dbcontext, IMapper mapper,IConfiguration config)
        {
            this.dbcontext = dbcontext;
            this.mapper = mapper;
            this.config = config;
        }
        public AuthResult Registeration(UserAddTDO userAddTDO)
        {
            if (dbcontext.Users.Any(m => m.UserName.Equals(userAddTDO.UserName)))
            {
                return new AuthResult { Success = false, ErrorCode = "User001" };
            }

            if (dbcontext.Users.Any(m => m.Email.Equals(userAddTDO.Email)))
            {
                return new AuthResult { Success = false, ErrorCode = "User002" };
            }

            var CurAuthor = mapper.Map<Users>(userAddTDO);

            byte[] hash, salt;
            GenerateHashedPass(userAddTDO.Password, out  hash, out salt);
            CurAuthor.PasswordHash = hash;
            CurAuthor.PasswordSalt = salt;

            dbcontext.Users.Add(CurAuthor);
            dbcontext.SaveChanges();

            return new AuthResult { Success = true, UserId = CurAuthor.UserId, UserName = CurAuthor.UserName };

        }
        public AuthResult UserLogin(UserLoginTDO userLoginTDO)
        {
            var CurLogedUser= dbcontext.Users.Where(m => m.UserName.Equals(userLoginTDO.UserName)).SingleOrDefault();
            if (CurLogedUser==null)
            {
                return new AuthResult { Success = false, ErrorCode = "User003" };
            }

            if (!ValidateHashedPassword(userLoginTDO.Password, CurLogedUser.PasswordHash, CurLogedUser.PasswordSalt))
            {
                return new AuthResult { Success = false, ErrorCode = "User004" };

            }
            //generate Token using "Config" to read the value from appsetting
            var key= config.GetValue<string>("JWT");
            var KeyByte = Encoding.ASCII.GetBytes(key);
            var Desc = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(KeyByte), SecurityAlgorithms.HmacSha512Signature),
                Subject = new ClaimsIdentity(
                        new Claim[] {
                            new Claim(JwtRegisteredClaimNames.Sub,CurLogedUser.UserName),
                            new Claim(JwtRegisteredClaimNames.Email,CurLogedUser.Email),
                            new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                            new Claim("UserId",CurLogedUser.UserId)
                        }
                        )
            };
            var handler = new JwtSecurityTokenHandler();
            var token = handler.CreateToken(Desc);
            return new AuthResult { Success = true,UserId=CurLogedUser.UserId
                ,UserName=CurLogedUser.UserName, Token = handler.WriteToken(token) };

        }

        private bool ValidateHashedPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hash = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
               var newPasswordHash = hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < newPasswordHash.Length; i++)
                    if (newPasswordHash[i]!=passwordHash[i])
                        return false;
            }
            return true;
        }

        public void GenerateHashedPass(string Password,out byte [] PasswordHash,out byte[] PasswordSalt)
        {
            using(var hash = new System.Security.Cryptography.HMACSHA512())
            {
                PasswordHash = hash.ComputeHash(Encoding.UTF8.GetBytes(Password));
                PasswordSalt = hash.Key;

            }
        }
    }
}
