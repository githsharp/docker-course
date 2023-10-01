using _11_MovieFlix_api.Models;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace _11_MovieFlix_api.Controllers
{
    [ApiController]
    [Route("api/movies")]
    public class MoviesController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly string _apiUrl;
        private readonly string _apiKey;

        public MoviesController(IConfiguration config)
        {
            _config = config;
            _apiUrl = config["movieApi:baseUrl"] ?? "";
            _apiKey = config["movieApi:key"] ?? "";
        }

        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            if (_apiKey is null && _apiUrl is null)
            {
                return StatusCode(500, "Problem with external api occurred");
            }

            var options = new RestClientOptions($"{_apiUrl}/movie/popular?api_key={_apiKey}&language=sv-SE");
            var client = new RestClient(options);
            var request = new RestRequest();
            request.AddHeader("accept", "application/json");
            var result = await client.GetAsync<MovieResult>(request);

            return Ok(result);
        }
    }
}