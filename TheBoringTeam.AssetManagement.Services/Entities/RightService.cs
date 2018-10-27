using System;
using System.Collections.Generic;
using System.Text;
using TheBoringTeam.AssetManagement.Models;
using TheBoringTeam.AssetManagement.Repositories.Interfaces;
using TheBoringTeam.AssetManagement.Services.Interfaces;

namespace TheBoringTeam.AssetManagement.Services.Entities
{
    public class RightService : BaseService<Right>, IRightService
    {
        public RightService(IBaseMongoRepository<Right> repository) : base(repository)
        {
        }
    }
}
