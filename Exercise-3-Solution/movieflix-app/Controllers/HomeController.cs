using Microsoft.AspNetCore.Mvc;
using movieflix_app.Models;
using RestSharp;

namespace movieflix_app.Controllers
{
    [Route("[controller]")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            // var options = new RestClientOptions("http://host.docker.internal:5001/api/movies");
            var options = new RestClientOptions("http://movieflix-backend:5001/api/movies");
            var client = new RestClient(options);
            var request = new RestRequest();
            var movies = await client.GetAsync<IList<MovieViewModel>>(request);

            if (movies is not null)
            {
                return View(movies);
            }
            else
            {
                return View("NotFound");
            }
        }
    }
}