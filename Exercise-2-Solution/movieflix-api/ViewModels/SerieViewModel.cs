namespace movieflix_api.ViewModels
{
    public class SerieViewModel
    {
        public int Id { get; set; }
        public string? Language { get; set; }
        public string? Title { get; set; }
        public DateOnly? Releasedate { get; set; }
        public string? Overview { get; set; }
        public string? Poster { get; set; }
        public string? Background { get; set; }
    }
}