using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TheBoringTeam.AssetManagement.Models;
using TheBoringTeam.AssetManagement.Models.Infrastructure;

namespace TheBoringTeam.AssetManagement.Services.Interfaces
{
    public interface IAssetService: IBaseService<Asset>
    {
        Task<ImageRecognitionResult> AnalyzeImage(string base64image);
        Task AnalyzeText(string base64image);
    }
}
