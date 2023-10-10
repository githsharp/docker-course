using System.Text.Json.Serialization;

namespace movieflix_api.Models
{
    public class SerieModel
    {
        public int Id { get; set; }
        [JsonPropertyName("original_language")]
        public string? Language { get; set; }
        [JsonPropertyName("name")]
        public string? Title { get; set; }
        [JsonPropertyName("first_air_date")]
        public DateOnly? Releasedate { get; set; }
        [JsonPropertyName("overview")]
        public string? Overview { get; set; }
        [JsonPropertyName("poster_path")]
        public string? Poster { get; set; }
        [JsonPropertyName("backdrop_path")]
        public string? Background { get; set; }
    }
}