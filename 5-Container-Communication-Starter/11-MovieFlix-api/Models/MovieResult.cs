namespace _11_MovieFlix_api.Models
{
    public class MovieResult
    {
        public int Page { get; set; }
        public IList<MovieInfo>? Results { get; set; }
    }
}