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
    public class AssetController : ControllerBase
    {
        private readonly IAssetService _assetService;
        private readonly IAssetErrorService _assetErrorService;

        public AssetController(IAssetService assetService, IAssetErrorService assetErrorService)
        {
            _assetService = assetService;
            _assetErrorService = assetErrorService;
        }

        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> Upload([FromBody]ImageUploadDTO data)
        {
            var imageResult = await _assetService.AnalyzeImage(data.base64image);
            return Ok(imageResult);
        }

        [HttpPost]
        [Route("analyzetext")]
        public async Task<IActionResult> AnalyzeText([FromBody] ImageUploadDTO data)
        {
            await _assetService.AnalyzeText(data.base64image);
            return Ok();
        }

        [HttpPost]
        [Route("reporterror")]
        public async Task<IActionResult> ReportError([FromBody] AssetErrorDTO error)
        {
            try
            {
                AssetError assetError = new AssetError()
                {
                    Title = error.Title,
                    Description = error.Description
                };
                _assetErrorService.Insert(assetError);
                return Ok(assetError);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            Asset asset = _assetService.GetById(id);
            if (asset == null)
            {
                return NotFound();
            }
            return Ok(asset);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Insert([FromBody] AssetInputDTO request)
        {
            try
            {
                Asset asset = new Asset()
                {
                    Tags = request.Tags,
                    Extra = request.Extra
                };

                _assetService.Insert(asset);
                return Ok(asset);

            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromBody]AssetInputDTO request, [FromRoute]string id)
        {
            try
            {
                Asset asset = _assetService.GetById(id);
                if (asset == null)
                {
                    return NotFound();
                }

                asset.Tags = request.Tags;
                asset.Extra = request.Extra;

                _assetService.Update(asset);
                return Ok(asset);
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
                Asset asset = _assetService.GetById(id);
                if (asset == null)
                {
                    return NotFound();
                }

                _assetService.Delete(asset);
                return NoContent();
            }
            catch
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
