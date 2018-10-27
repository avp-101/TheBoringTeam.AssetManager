using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using TheBoringTeam.AssetManagement.Models;
using TheBoringTeam.AssetManagement.Repositories.Interfaces;
using TheBoringTeam.AssetManagement.Services.Interfaces;

namespace TheBoringTeam.AssetManagement.Services.Entities
{
    public class UserService : BaseService<User>, IUserService
    {

        private readonly IConfiguration _configuration;
        private readonly IRoleService _roleService;

        public UserService(IBaseMongoRepository<User> repository, IRoleService roleService,IConfiguration configuration) : base(repository)
        {
            _configuration = configuration;
            _roleService = roleService;
        }

        public UserLoginResult Authenticate(string username, string password)
        {
            var user = Search(f => f.Username == username && f.Password == password).FirstOrDefault();
            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["appSecret"]);
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.Id.ToString())
            };
            
            foreach(var right in user?.Role?.Rights)
            {
                claims.Add(new Claim(right.Name, "true"));
            }

            var identity = new ClaimsIdentity(claims);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var userResult = new UserLoginResult()
            {
                Id = user.Id,
                DisplayName = user.DisplayName,
                Email = user.Email,
                Username = user.Username,
                Role = user.Role,
                CreatedOn = user.CreatedOn,
                ModifiedOn = user.ModifiedOn,
                AccessToken = tokenHandler.WriteToken(token)
            };

            return userResult;
        }

        public override User GetById(string id)
        {
            var user = base.GetById(id);
            user.Role = this._roleService.GetById(user.RoleId);

            return user;
        }

        public override IEnumerable<User> Search(Expression<Func<User, bool>> filter)
        {
            var users = base.Search(filter);
            var rolesIds = users.Select(u => u.RoleId);
            var roles = this._roleService.Search(r => rolesIds.Contains(r.Id));

            foreach(var user in users)
            {
                user.Role = roles.FirstOrDefault(f => f.Id == user.RoleId);
            }

            return users;
        }
    }
}
