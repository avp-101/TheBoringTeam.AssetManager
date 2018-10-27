using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using TheBoringTeam.AssetManagement.Models;
using TheBoringTeam.AssetManagement.Repositories.Interfaces;
using TheBoringTeam.AssetManagement.Services.Interfaces;

namespace TheBoringTeam.AssetManagement.Services.Entities
{
    public class UserService : BaseService<User>, IUserService
    {
        public UserService(IBaseMongoRepository<User> repository) : base(repository)
        {
        }
    }
}
