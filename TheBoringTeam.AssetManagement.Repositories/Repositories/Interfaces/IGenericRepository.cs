using System;
using System.Collections.Generic;
using System.Text;
using TheBoringTeam.AssetManagement.Models.Interfaces;

namespace TheBoringTeam.AssetManagement.Repositories.Repositories.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class, IIdentifiable, ITrackable
    {

    }
}
