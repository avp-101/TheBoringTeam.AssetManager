using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using TheBoringTeam.AssetManagement.Models;
using TheBoringTeam.AssetManagement.Repositories.Interfaces;
using TheBoringTeam.AssetManagement.Services.Interfaces;

namespace TheBoringTeam.AssetManagement.Services.Entities
{
    public class RoleService : BaseService<Role>, IRoleService
    {
        private readonly IRightService _rightService;

        public RoleService(
            IBaseMongoRepository<Role> repository,
            IRightService rightService
        ) : base(repository)
        {
            _rightService = rightService;
        }

        public override Role GetById(string id)
        {
            var role = base.GetById(id);
            var rights = this._rightService.Search(r => role.RightIds.Contains(r.Id));
            role.Rights = rights;
            return role;
        }

        public override IEnumerable<Role> Search(Expression<Func<Role, bool>> filter)
        {
            var roles = base.Search(filter);
            var rights = this._rightService.Search(f => true);

            foreach (var role in roles)
            {
                role.Rights = rights.Where(f => role.RightIds.Contains(f.Id));
            }

            return roles;

        }
    }
}
