using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using movieflix_api.Data;
using movieflix_api.Models;
using movieflix_api.ViewModels;

namespace movieflix_api.Controllers
{
    [ApiController]
    [Route("api/favorites")]
    public class FavoritesController : ControllerBase
    {
        private readonly MovieContext _context;
        public FavoritesController(MovieContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> ListFavorites()
        {
            string folderPath = Environment.CurrentDirectory + "/Favorites";
            var result = await System.IO.File.ReadAllTextAsync(Path.Combine(folderPath, "favorites.txt"));
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddFavorite(FavoriteViewModel model)
        {
            try
            {
                string folderPath = Environment.CurrentDirectory + "/Favorites";
                await System.IO.File.AppendAllTextAsync(Path.Combine(folderPath, "favorites.txt"), model.Title + Environment.NewLine);
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}