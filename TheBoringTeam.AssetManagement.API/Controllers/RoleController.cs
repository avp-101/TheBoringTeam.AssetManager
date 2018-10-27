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
        [Route("")]
        public async Task<IActionResult> GetAll()
        {
            var roles = this._roleService.Search(f => true);
            var rights = this._rightService.Search(f => true);

            foreach (var role in roles)
            {
                role.Rights = rights.Where(f => role.RightIds.Contains(f.Id));
            }

            IEnumerable<RoleDTO> rolesReturned = roles.Select(r => new RoleDTO() { Name = r.Name });
            return Ok(rolesReturned);
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
            roleReturned.Rights = role.Rights.Select(r => new RightDTO() { Name = r.Name });
            return Ok(roleReturned);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Insert([FromBody] RoleInputDTO request)
        {
            try
            {
                if (this._roleService.Search(r => r.Name == request.Name).Any())
                {
                    return BadRequest("Name must be unique");
                }

                if (this._rightService.Search(r => request.Rights.Contains(r.Id)).Count() != request.Rights.Count())
                {
                    return BadRequest("You are trying to insert rights that do not exist.");
                }

                Role role = new Role()
                {
                    Name = request.Name,
                    RightIds = request.Rights
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
