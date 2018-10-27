using System;
using System.Collections.Generic;
using System.Text;
using TheBoringTeam.AssetManagement.Models;
using TheBoringTeam.AssetManagement.Repositories.Interfaces;
using TheBoringTeam.AssetManagement.Services.Interfaces;

namespace TheBoringTeam.AssetManagement.Services.Entities
{
    public class RoleService : BaseService<Role>, IRoleService
    {
        public RoleService(IBaseMongoRepository<Role> repository) : base(repository)
        {

        }
    }
}
