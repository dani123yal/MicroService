using Authentication.Helper;
using Authentication.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Services
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        User GetUserById(int id);


    }
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        //private AppDbContext _context;
        //private IFacebookService _facebookService;
        //private IGoogleService googleService;

        public UserService(IOptions<AppSettings> appSettings /*, AppDbContext appDbContext,*/ /*IFacebookService facebookService,*/
            /*IGoogleService googleService*/)
        {
            _appSettings = appSettings.Value;
            //_context = appDbContext;
            //_facebookService = facebookService;
            //_googleService = googleService;
        }



        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            if(model.Email == "daniyal" && model.Password == "1234")
            {
                User user = GetUser(model.Email);
                var token = generateJwtToken(user);

                return new AuthenticateResponse(true, token);

            }
            //var user = _context.Users.SingleOrDefault(x => x.Email == model.Email && x.Password == model.Password);

            // return null if user not found
            //if (user == null) return null;

            // authentication successful so generate jwt token
            return null;

        }

        private string generateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public User GetUser(string email)
        {
            return new User { Email = email, Id = 1, Name = email };
        }

        public User GetUserById(int id)
        {
            return new User { Email = "daniyal", Id = 1, Name = "Dani" };
        }
    }
}
