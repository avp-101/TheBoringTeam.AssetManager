using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using TheBoringTeam.AssetManagement.Models.Interfaces;
using TheBoringTeam.AssetManagement.Repositories.Interfaces;
using TheBoringTeam.AssetManagement.Services.Interfaces;

namespace TheBoringTeam.AssetManagement.Services.Entities
{
    public class BaseService<TBaseEntity>: IBaseService<TBaseEntity>
        where TBaseEntity: IIdentifiable, ITrackable
    {
        private IBaseMongoRepository<TBaseEntity> _repository;

        public BaseService(IBaseMongoRepository<TBaseEntity> repository)
        {
            _repository = repository;
        }

        public virtual void Delete(string id)
        {
            _repository.Delete(id);
        }

        public virtual void Delete(IEnumerable<string> ids)
        {
            _repository.Delete(ids);
        }

        public virtual void Delete(TBaseEntity entity)
        {
            _repository.Delete(entity);
        }

        public virtual void Delete(IEnumerable<TBaseEntity> entities)
        {
            _repository.Delete(entities);
        }

        public virtual TBaseEntity GetById(string id)
        {
            return _repository.Get(f => f.Id == id).FirstOrDefault();
        }

        public virtual void Insert(IEnumerable<TBaseEntity> entities)
        {
            _repository.Insert(entities);
        }

        public virtual void Insert(TBaseEntity entity)
        {
            _repository.Insert(entity);
        }

        public virtual IEnumerable<TBaseEntity> Search(Expression<Func<TBaseEntity, bool>> filter)
        {
            return _repository.Get(filter);
        }

        public virtual void Update(TBaseEntity entity)
        {
            _repository.Update(entity);
        }
    }
}
