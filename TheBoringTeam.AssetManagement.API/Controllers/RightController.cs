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
    public class RightController : ControllerBase
    {
        private readonly IRightService _rightService;

        public RightController(IRightService rightService)
        {
            _rightService = rightService;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Insert([FromBody] RightCreateDTO request)
        {
            try
            {
                Right right = new Right()
                {
                    Name = request.Name
                };

                _rightService.Insert(right);
                return Ok(right);

            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}