using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver.Core.Authentication;
using TheBoringTeam.AssetManagement.API.DTOs;
using TheBoringTeam.AssetManagement.Models;
using TheBoringTeam.AssetManagement.Services.Interfaces;

namespace TheBoringTeam.AssetManagement.API.Controllers
{
    [Route("api/user")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IRightService _rightService;

        public UserController(IUserService userService, IRoleService roleService, IRightService rightService)
        {
            _rightService = rightService;
            _roleService = roleService;
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]UserLoginDTO userParam)
        {
            var user = _userService.Authenticate(userParam.Username, userParam.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            User user = _userService.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            UserDTO userReturned = new UserDTO();

            userReturned.Id = user.Id;
            userReturned.DisplayName = user.DisplayName;

            Role role = this._roleService.GetById(user.RoleId);
            IEnumerable<Right> rights = this._rightService.Search(r => role.Rights.Contains(r.Id));

            userReturned.Role = new RoleDTO()
            {
                Name =  role.Name,
                Rights = rights.Select(r => new RightDTO() { Name = r.Name })
            };

            return Ok(userReturned);

        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Insert([FromBody] UserCreateDTO request)
        {
            try
            {

                if (String.IsNullOrEmpty(request.Username))
                {
                    ModelState.AddModelError("Username", "Username is empty");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                User user = new User()
                {
                    DisplayName = request.DisplayName,
                    Email = request.Email,
                    Username = request.Username,
                    Password = request.Password,
                    RoleId = request.RoleId
                };

                _userService.Insert(user);
                return Ok(user);

            }
            catch(Exception ex)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromBody]UserUpdateDTO request, [FromRoute]string id)
        {
            try
            {
                User user = _userService.GetById(id);
                if (user == null)
                {
                    return NotFound();
                }

                user.DisplayName = request.DisplayName;
                user.Email = request.Email;

                _userService.Update(user);
                return Ok(user);
            }
            catch
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdatePassword([FromBody]UserUpdatePasswordDTO request, [FromRoute]string id)
        {
            try
            {
                User user = _userService.GetById(id);
                if (user == null)
                {
                    return NotFound();
                }

                user.Password = request.Password;

                _userService.Update(user);
                return Ok(user);
            }
            catch
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete("{id}")]
        [Route("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                User user = _userService.GetById(id);
                if (user == null)
                {
                    return NotFound();
                }

                _userService.Delete(user);
                return NoContent();
            }
            catch
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

    }
}
