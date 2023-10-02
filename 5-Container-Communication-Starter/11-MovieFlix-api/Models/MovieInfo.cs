using System.Text.Json.Serialization;

namespace _11_MovieFlix_api.Models
{
    public class MovieInfo
    {
        public int Id { get; set; }
        [JsonPropertyName("original_language")]
        public string? Language { get; set; }
        // [JsonPropertyName("original_title")]
        public string? Title { get; set; }
        [JsonPropertyName("release_date")]
        public DateOnly? ReleaseDate { get; set; }
        public string? Overview { get; set; }
        [JsonPropertyName("poster_path")]
        public string? Poster { get; set; }
    }
}