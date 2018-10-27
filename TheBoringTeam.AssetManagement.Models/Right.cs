using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TheBoringTeam.AssetManagement.Models.Interfaces;

namespace TheBoringTeam.AssetManagement.Models
{
    public class Right: IIdentifiable, ITrackable
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public IEnumerable<string> Rights { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

    }
}
