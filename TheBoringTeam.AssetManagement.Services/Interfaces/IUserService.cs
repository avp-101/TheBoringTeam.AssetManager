﻿using System;
using System.Collections.Generic;
using System.Text;
using TheBoringTeam.AssetManagement.Models;

namespace TheBoringTeam.AssetManagement.Services.Interfaces
{
    public interface IUserService: IBaseService<User>
    {
        UserLoginResult Authenticate(string username, string password);
    }
}
