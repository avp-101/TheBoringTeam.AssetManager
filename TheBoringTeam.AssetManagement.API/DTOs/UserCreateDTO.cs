using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TheBoringTeam.AssetManagement.API.DTOs
{
    public class UserCreateDTO
    {
        [Required]
        public string DisplayName { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
