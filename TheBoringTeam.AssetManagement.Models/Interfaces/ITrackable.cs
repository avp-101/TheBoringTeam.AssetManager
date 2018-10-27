using System;
using System.Collections.Generic;
using System.Text;

namespace TheBoringTeam.AssetManagement.Models.Interfaces
{
    public interface ITrackable
    {
        DateTime CreatedOn { get; set; }
        DateTime? ModifiedOn { get; set; }
    }
}
