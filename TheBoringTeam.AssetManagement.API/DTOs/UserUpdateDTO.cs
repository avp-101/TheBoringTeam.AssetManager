using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TheBoringTeam.AssetManagement.API.DTOs
{
    public class UserUpdateDTO
    {
        [Required]
        public string DisplayName { get; set; }

        [Required]
        public string Email { get; set; }
    }
}
