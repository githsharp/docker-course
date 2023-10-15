namespace customer_service.ViewModels
{
    public class MessageViewModel
    {
        public DateTimeOffset? AddedOn { get; set; }
        public string? Sender { get; set; }
        public string? Message { get; set; }
    }
}