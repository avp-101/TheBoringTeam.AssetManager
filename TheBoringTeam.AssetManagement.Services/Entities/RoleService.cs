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
        private readonly IUserService _userService;
        private readonly IBaseMongoRepository<Right> _rightRepository;

        public RoleService(
            IBaseMongoRepository<Role> repository,
            IBaseMongoRepository<Right> rightRepository,
            IUserService userService
        ) : base(repository)
        {
            _userService = userService;
            _rightRepository = rightRepository;
        }

        public override Role GetById(string id)
        {
            var role = base.GetById(id);
            var rights = this._rightRepository.Get(r => role.RightIds.Contains(r.Id));
            role.Rights = rights;
            return role;
        }

        public override IEnumerable<Role> Search(Expression<Func<Role, bool>> filter)
        {
            var roles = base.Search(filter);
            var rights = this._rightRepository.Get(f => true);

            foreach (var role in roles)
            {
                role.Rights = rights.Where(f => role.RightIds.Contains(f.Id));
            }

            return roles;
        }

        public override void Delete(IEnumerable<Role> entities)
        {
            foreach (var entity in entities)
            {
                if (entity.RightIds.Count() > 0)
                    throw new Exception("Cannot delete a role with a right");
                var user = _userService.Search(f => f.RoleId == entity.Id).FirstOrDefault();
                if (user != null)
                    throw new Exception("Cannot delete a role because it is assigned to an user");
            }
            base.Delete(entities);
        }

        public override void Delete(IEnumerable<string> ids)
        {
            foreach (var id in ids)
            {
                var role = GetById(id);
                if (role.RightIds.Count() > 0)
                    throw new Exception("Cannot delete a role with a right");
                var user = _userService.Search(f => f.RoleId == role.Id).FirstOrDefault();
                if (user != null)
                    throw new Exception("Cannot delete a role because it is assigned to an user");
            }
            base.Delete(ids);
        }

        public override void Delete(Role entity)
        {
            if (entity.RightIds.Count() > 0)
                throw new Exception("Cannot delete a role with a right");
            var user = _userService.Search(f => f.RoleId == entity.Id).FirstOrDefault();
            if (user != null)
                throw new Exception("Cannot delete a role because it is assigned to an user");
            base.Delete(entity);
        }

        public override void Delete(string id)
        {
            var role = GetById(id);
            if (role.RightIds.Count() > 0)
                throw new Exception("Cannot delete a role with a right");
            var user = _userService.Search(f => f.RoleId == role.Id).FirstOrDefault();
            if (user != null)
                throw new Exception("Cannot delete a role because it is assigned to an user");
            base.Delete(id);
        }
    }
}
