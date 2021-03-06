﻿using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TheBoringTeam.AssetManagement.Models;
using TheBoringTeam.AssetManagement.Models.Infrastructure;
using TheBoringTeam.AssetManagement.Repositories.Interfaces;
using TheBoringTeam.AssetManagement.Services.Interfaces;

namespace TheBoringTeam.AssetManagement.Services.Entities
{
    public class AssetService : BaseService<Asset>, IAssetService
    {
        private readonly IConfiguration _configuration;
        public AssetService(IBaseMongoRepository<Asset> repository, IConfiguration configuration) : base(repository)
        {
            _configuration = configuration;
        }

        public async Task AnalyzeText(string base64image)
        {
            //to remove metadata
            base64image = base64image.Substring(23);
            try
            {
                var subscriptionKey = _configuration["computerVisionKey"];
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add(
                    "Ocp-Apim-Subscription-Key", subscriptionKey);
                string uri = _configuration["textAnalysisUrl"];
                HttpResponseMessage response;
                byte[] byteData = Convert.FromBase64String(base64image);
                var stringarray = byteData.ToString();
                using (ByteArrayContent content = new ByteArrayContent(byteData))
                {
                    content.Headers.ContentType =
                        new MediaTypeHeaderValue("application/octet-stream");
                    response = await client.PostAsync(uri, content);
                }
                HttpHeaders headers = response.Headers;
                IEnumerable<string> values;

                if (headers.TryGetValues("operation-location", out values))
                {
                    string responseUrl = values.First();
                    Thread.Sleep(4000);
                    var operationResponse = await client.GetAsync(responseUrl);
                    string contentString = await operationResponse.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {

            }
        }

        public async Task<ImageRecognitionResult> AnalyzeImage(string base64image)
        {
            //to remove metadata
            base64image = base64image.Substring(23);
            try
            {
                var subscriptionKey = _configuration["computerVisionKey"];
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add(
                    "Ocp-Apim-Subscription-Key", subscriptionKey);
                string requestParameters =
                    "visualFeatures=Categories,Description,Color";
                string uri = _configuration["computerVisionUrl"] + "?" + requestParameters;
                HttpResponseMessage response;
                byte[] byteData = Convert.FromBase64String(base64image);
                var stringarray = byteData.ToString();
                using (ByteArrayContent content = new ByteArrayContent(byteData))
                {
                    content.Headers.ContentType =
                        new MediaTypeHeaderValue("application/octet-stream");
                    response = await client.PostAsync(uri, content);
                }
                string contentString = await response.Content.ReadAsStringAsync();
                ImageRecognitionResult imageResult = JsonConvert.DeserializeObject<ImageRecognitionResult>(contentString);
                return imageResult;
            }
            catch (Exception ex)
            {

            }
            return null;
        }
    }
}
