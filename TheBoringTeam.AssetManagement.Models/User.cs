using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TheBoringTeam.AssetManagement.Models.Interfaces;

namespace TheBoringTeam.AssetManagement.Models
{
    public class User: IIdentifiable, ITrackable
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string DisplayName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string RoleId { get; set; }

        [Required]
        public IEnumerable<string> Rights { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
