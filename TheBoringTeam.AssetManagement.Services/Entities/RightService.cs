using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheBoringTeam.AssetManagement.Models;
using TheBoringTeam.AssetManagement.Repositories.Interfaces;
using TheBoringTeam.AssetManagement.Services.Interfaces;

namespace TheBoringTeam.AssetManagement.Services.Entities
{
    public class RightService : BaseService<Right>, IRightService
    {
        private readonly IRoleService _roleService;
        public RightService(IBaseMongoRepository<Right> repository, IRoleService roleService) : base(repository)
        {
            _roleService = roleService;
        }

        public override void Delete(IEnumerable<Right> entities)
        {
            foreach(var entity in entities)
            {
                var roles = _roleService.Search(f => f.RightIds.Any(x => x == entity.Id)).ToList();
                if (roles.Count > 0)
                    throw new Exception("Cannot delete a right because it is assigned to a role");
            }
            base.Delete(entities);
        }

        public override void Delete(IEnumerable<string> ids)
        {
            foreach(var id in ids)
            {
                var roles = _roleService.Search(f => f.RightIds.Any(x => x == id)).ToList();
                if (roles.Count > 0)
                    throw new Exception("Cannot delete a right because it is assigned to a role");
            }
            base.Delete(ids);
        }

        public override void Delete(Right entity)
        {
            var roles = _roleService.Search(f => f.RightIds.Any(x => x == entity.Id)).ToList();
            if (roles.Count > 0)
                throw new Exception("Cannot delete a right because it is assigned to a role");
            base.Delete(entity);
        }

        public override void Delete(string id)
        {
            var roles = _roleService.Search(f => f.RightIds.Any(x => x == id)).ToList();
            if (roles.Count > 0)
                throw new Exception("Cannot delete a right because it is assigned to a role");
            base.Delete(id);
        }
    }
}
