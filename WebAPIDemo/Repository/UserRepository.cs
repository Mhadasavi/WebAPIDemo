using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebAPIDemo.Data;
using WebAPIDemo.Models;
using WebAPIDemo.Repository.IRepository;

namespace WebAPIDemo.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly AppSettings _appSettings;

        public UserRepository(ApplicationDbContext applicationDbContext, IOptions<AppSettings> appSettings)
        {
            _applicationDbContext = applicationDbContext;
            _appSettings = appSettings.Value;
        }

        public User Authenticate(string username, string password)
        {
            var user = _applicationDbContext.Users.SingleOrDefault(x => x.Username == username && x.Password == password);
            if (user == null)
            {
                return null;
            }
            //if user if found, generate JWT token
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.id.ToString()),
                    new Claim(ClaimTypes.Role,user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            user.Token = jwtTokenHandler.WriteToken(token);
            user.Password = "";
            return user;
        }


        public bool isUserExist(string username)
        {
            var user = _applicationDbContext.Users.SingleOrDefault(x => x.Username == username);
            if (user == null)
            {
                return false;
            }
            return true;

        }

        public User Register(string username, string password)
        {
            User user = new User()
            {
                Username = username,
                Password = password,
                Role="Admin"
            };
            _applicationDbContext.Users.Add(user);
            _applicationDbContext.SaveChanges();
            user.Password = "";
            return user;
        }
    }
}
