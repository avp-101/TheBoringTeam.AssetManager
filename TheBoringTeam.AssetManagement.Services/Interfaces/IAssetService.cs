using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TheBoringTeam.AssetManagement.Models;

namespace TheBoringTeam.AssetManagement.Services.Interfaces
{
    public interface IAssetService: IBaseService<Asset>
    {
        Task AnalyzeImage(string base64image);
    }
}
