using Microsoft.AspNetCore.Mvc;
using movieflix_api.Models;
using movieflix_api.ViewModels;
using RestSharp;

namespace movieflix_api.Controllers
{
    [ApiController]
    [Route("api/series")]
    public class SeriesController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> ListSeries()
        {
            var options = new RestClientOptions("https://api.themoviedb.org/3/tv/popular?language=en-US&api_key=<your api-key>");
            var client = new RestClient(options);
            var request = new RestRequest();
            var response = await client.GetAsync<SerieResultModel>(request);

            if (response != null)
            {
                var result = response.Results?.Select(m => new SerieViewModel
                {
                    Id = m.Id,
                    Language = m.Language,
                    Title = m.Title,
                    Releasedate = m.Releasedate,
                    Overview = m.Overview,
                    Poster = $"https://image.tmdb.org/t/p/w500{m.Poster}",
                    Background = $"https://image.tmdb.org/t/p/original{m.Background}"
                });
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
    }
}