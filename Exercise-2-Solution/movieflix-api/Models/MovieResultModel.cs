namespace movieflix_api.Models
{
    public class MovieResultModel
    {
        public int Page { get; set; }
        public IList<MovieModel>? Results { get; set; }
    }
}