using System;
using System.Collections.Generic;
using System.Text;
using TheBoringTeam.AssetManagement.Models.Interfaces;

namespace TheBoringTeam.AssetManagement.Models
{
    public class Right : IIdentifiable, ITrackable
    {
        public string Id { get; set; }

        public string Name { get; set; }
        
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

    }
}
