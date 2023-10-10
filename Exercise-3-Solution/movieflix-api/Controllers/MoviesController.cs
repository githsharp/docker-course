using Microsoft.AspNetCore.Mvc;
using movieflix_api.Models;
using movieflix_api.ViewModels;
using RestSharp;

namespace movieflix_api.Controllers
{
    [ApiController]
    [Route("api/movies")]
    public class MoviesController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> ListMovies()
        {
            var options = new RestClientOptions("https://api.themoviedb.org/3/movie/popular?language=sv-SE&api_key=<your api-key>");
            var client = new RestClient(options);
            var request = new RestRequest();
            var response = await client.GetAsync<MovieResultModel>(request);

            if (response != null)
            {
                var result = response.Results?.Select(m => new MovieViewModel
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

        [HttpGet("{id}")]
        public async Task<IActionResult> FindMovie(string id)
        {

            var options = new RestClientOptions($"https://api.themoviedb.org/3/movie/{id}?language=sv-SE&api_key=<your api-key>");
            var client = new RestClient(options);
            var request = new RestRequest();
            var response = await client.GetAsync<MovieDetailsModel>(request);

            if (response != null)
            {
                var result = new MovieDetailViewModel
                {
                    Id = response.Id,
                    Language = response.Language,
                    Title = response.Title,
                    Genres = response.Genres,
                    Releasedate = response.Releasedate,
                    Runtime = response.Runtime,
                    Overview = response.Overview,
                    Poster = $"https://image.tmdb.org/t/p/w500{response.Poster}",
                    Background = $"https://image.tmdb.org/t/p/original{response.Background}"
                };
                return Ok(result);
            }

            return NotFound();
        }

    }
}