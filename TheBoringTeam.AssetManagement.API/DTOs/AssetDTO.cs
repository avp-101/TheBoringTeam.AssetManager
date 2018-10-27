using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TheBoringTeam.AssetManagement.API.DTOs
{
    public class AssetDTO
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public IEnumerable<string> Tags { get; set; }

        [Required]
        public IDictionary<string, string> Extra { get; set; }
    }
}
