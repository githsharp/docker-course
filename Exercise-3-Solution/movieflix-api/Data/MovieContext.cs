using Microsoft.EntityFrameworkCore;
using movieflix_api.Models;

namespace movieflix_api.Data
{
    public class MovieContext : DbContext
    {
        public DbSet<FavoriteModel> Favorites { get; set; }

        public MovieContext(DbContextOptions options) : base(options) { }
    }
}