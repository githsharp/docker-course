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
            var result = await _context.Favorites.ToListAsync();

            var favorites = result.Select(c => new FavoriteViewModel
            {
                MovieId = c.MovieId,
                Title = c.Title
            });
            return Ok(favorites);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> FindFavorite(int id)
        {
            var result = await _context.Favorites.SingleOrDefaultAsync(c => c.MovieId == id);

            if (result == null) return NotFound();

            var favorite = new FavoriteViewModel
            {
                MovieId = result.MovieId,
                Title = result.Title
            };

            return Ok(favorite);
        }

        [HttpPost]
        public async Task<IActionResult> AddFavorite(FavoriteViewModel model)
        {
            var favorite = new FavoriteModel
            {
                MovieId = model.MovieId,
                Title = model.Title
            };

            try
            {
                await _context.Favorites.AddAsync(favorite);

                if (await _context.SaveChangesAsync() > 0)
                {
                    return CreatedAtAction(nameof(FindFavorite), new { id = favorite.MovieId },
                    new
                    {
                        Id = favorite.Id,
                        MovieId = favorite.MovieId,
                        Title = favorite.Title
                    });
                }
                else
                {
                    return StatusCode(500, "Internal server error");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}