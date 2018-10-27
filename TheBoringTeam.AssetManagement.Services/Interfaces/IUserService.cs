using System;
using System.Collections.Generic;
using System.Text;
using TheBoringTeam.AssetManagement.Models;
using TheBoringTeam.AssetManagement.Models.DTOs;

namespace TheBoringTeam.AssetManagement.Services.Interfaces
{
    public interface IUserService: IBaseService<User>
    {
        UserLoginResult Authenticate(string username, string password);
    }
}
