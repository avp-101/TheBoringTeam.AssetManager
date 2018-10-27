using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheBoringTeam.AssetManagement.API.DTOs;
using TheBoringTeam.AssetManagement.Models;
using TheBoringTeam.AssetManagement.Services.Interfaces;

namespace TheBoringTeam.AssetManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Insert([FromBody] RoleCreateDTO request)
        {
            try
            {
                Role role = new Role()
                {
                    Name = request.Name,
                    Rights = request.Rights
                };

                _roleService.Insert(role);
                return Ok(role);

            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }

}
