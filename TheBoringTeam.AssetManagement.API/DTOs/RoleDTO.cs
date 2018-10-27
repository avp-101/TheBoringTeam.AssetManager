using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheBoringTeam.AssetManagement.API.DTOs
{
    public class RoleDTO
    {
        public string Name { get; set; }

        public IEnumerable<RightDTO> Rights { get; set; }
    }
}
