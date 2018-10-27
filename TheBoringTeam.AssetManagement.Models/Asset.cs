using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TheBoringTeam.AssetManagement.Models.Interfaces;

namespace TheBoringTeam.AssetManagement.Models
{
    public class Asset: IIdentifiable, ITrackable
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string Tag { get; set; }

        [Required]
        public IDictionary<string, string> Extra { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
