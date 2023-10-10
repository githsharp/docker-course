using System.Text.Json.Serialization;

namespace movieflix_api.Models
{
    public class MovieModel
    {
        public int Id { get; set; }
        [JsonPropertyName("original_language")]
        public string? Language { get; set; }
        public string? Title { get; set; }
        [JsonPropertyName("release_date")]
        public DateOnly? Releasedate { get; set; }
        public string? Overview { get; set; }
        [JsonPropertyName("poster_path")]
        public string? Poster { get; set; }
        [JsonPropertyName("backdrop_path")]
        public string? Background { get; set; }
    }
}