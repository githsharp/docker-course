using System.ComponentModel.DataAnnotations;

namespace movieflix_api.Models
{
    public class FavoriteModel
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public string? Title { get; set; }
    }
}