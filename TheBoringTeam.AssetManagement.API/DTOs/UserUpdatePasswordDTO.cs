using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TheBoringTeam.AssetManagement.API.DTOs
{
    public class UserUpdatePasswordDTO
    {
        [Required]
        public string Password { get; set; }
    }
}
