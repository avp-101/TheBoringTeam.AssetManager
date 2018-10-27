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
    [Route("api/right")]
    [ApiController]
    public class RightController : ControllerBase
    {
        private readonly IRightService _rightService;

        public RightController(IRightService rightService)
        {
            _rightService = rightService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAll()
        {
            var rights = this._rightService.Search(f => true);

            IEnumerable<RightDTO> rightsReturned = rights.Select(r => new RightDTO() {Name = r.Name});
            return Ok(rightsReturned);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Insert([FromBody] RightDTO request)
        {
            try
            {
                if (this._rightService.Search(r => r.Name == request.Name).Any())
                {
                    return BadRequest("Name must be unique");
                }

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