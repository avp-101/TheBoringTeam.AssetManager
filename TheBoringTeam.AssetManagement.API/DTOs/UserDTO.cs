using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TheBoringTeam.AssetManagement.API.DTOs
{
    public class UserDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string DisplayName { get; set; }

        [Required]
        public IEnumerable<string> Rights { get; set; }

    }
}
