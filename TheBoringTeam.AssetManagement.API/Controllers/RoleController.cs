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
        private readonly IRightService _rightService;

        public RoleController(IRoleService roleService, IRightService rightService)
        {
            _rightService = rightService;
            _roleService = roleService;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            Role role = _roleService.GetById(id);
            if (role == null)
            {
                return NotFound();
            }

            RoleDTO roleReturned = new RoleDTO();

            roleReturned.Name = role.Name;

            IEnumerable<Right> rights = this._rightService.Search(r => role.Rights.Contains(r.Id));

            roleReturned.Rights = rights.Select(r => new RightDTO() {Name = r.Name});
            return Ok(roleReturned);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Insert([FromBody] RoleInputDTO request)
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
