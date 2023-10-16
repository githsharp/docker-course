namespace vehicle_api.ViewModels
{
    public class PostVehiclesViewModel
    {
        public string? RegistrationNumber { get; set; }
        public string? Manufacturer { get; set; }
        public string? Model { get; set; }
        public int ModelYear { get; set; }
        public int Mileage { get; set; }
    }
}