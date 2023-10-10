using movieflix_api.Models;

namespace movieflix_api.ViewModels
{
    public class MovieDetailViewModel : MovieViewModel
    {
        public IList<MovieGenreModel>? Genres { get; set; }
        public int Runtime { get; set; }
    }
}