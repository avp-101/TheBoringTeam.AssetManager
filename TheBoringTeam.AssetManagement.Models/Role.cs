using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TheBoringTeam.AssetManagement.Models.Interfaces;

namespace TheBoringTeam.AssetManagement.Models
{
    public class Role: IIdentifiable, ITrackable
    {
        [BsonId]
        public string Id { get; set; }

        public string Name { get; set; }
        
        public IEnumerable<string> RightIds { get; set; }

        [BsonIgnore]
        public IEnumerable<Right> Rights { get; set; }
        
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
