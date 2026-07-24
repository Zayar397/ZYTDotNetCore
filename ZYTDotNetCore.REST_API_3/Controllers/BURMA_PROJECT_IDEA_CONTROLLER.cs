using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace ZYTDotNetCore.REST_API_3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BURMA_PROJECT_IDEA_CONTROLLER : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly RestClient _restClient;
        private readonly ISnakeApi _snakeApi;

        public BURMA_PROJECT_IDEA_CONTROLLER(HttpClient httpClient, RestClient restClient, ISnakeApi snakeApi)
        {
            _httpClient = httpClient;
            _restClient = restClient;
            _snakeApi = snakeApi;
        }
        [HttpGet("birds")]
        public async Task<IActionResult> BirdsAsync()
        {
            var response = await _httpClient.GetAsync("birds");
            string retStr = await response.Content.ReadAsStringAsync();
            return Ok(retStr);
        }
        [HttpGet("art-gallery")]
        public async Task<IActionResult> ArtGalleryAsync()
        {
            RestRequest restRequest = new RestRequest("art-gallery");
            var response = await _restClient.GetAsync(restRequest);
            return Ok(response.Content);
        }
        public async Task<IActionResult> SnakeAsync()
        {
            var response = await _snakeApi.GetSnakes();
            return Ok(response);
        }
    }
}
