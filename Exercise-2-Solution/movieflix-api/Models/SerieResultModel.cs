namespace movieflix_api.Models
{
    public class SerieResultModel
    {
        public int Page { get; set; }
        public IList<SerieModel>? Results { get; set; }
    }
}