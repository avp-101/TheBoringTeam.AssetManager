using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TheBoringTeam.AssetManagement.Models.Interfaces;

namespace TheBoringTeam.AssetManagement.Models
{
    public class Asset: IIdentifiable, ITrackable
    {
        public string Id { get; set; }
        
        public IEnumerable<string> Tags { get; set; }
        
        public IDictionary<string, string> Extra { get; set; }
        
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
