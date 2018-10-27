﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver.Core.Authentication;
using TheBoringTeam.AssetManagement.API.DTOs;
using TheBoringTeam.AssetManagement.Models;
using TheBoringTeam.AssetManagement.Services.Interfaces;

namespace TheBoringTeam.AssetManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetById(string id)
        {
            User user = _userService.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Insert([FromBody] UserCreateDTO request)
        {
            try
            {

                if (!String.IsNullOrEmpty(request.Username))
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