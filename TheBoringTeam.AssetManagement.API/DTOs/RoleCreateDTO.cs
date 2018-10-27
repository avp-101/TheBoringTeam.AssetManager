using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TheBoringTeam.AssetManagement.API.DTOs
{
    public class RoleCreateDTO
    {
        public string Name { get; set; }

        public IEnumerable<string> Rights { get; set; }
    }
}