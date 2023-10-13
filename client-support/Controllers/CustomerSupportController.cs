using System.Text.Json;
using Azure.Storage.Queues;
using client_support.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace client_support.Controllers
{
    [ApiController]
    [Route("api/customersupport")]
    public class CustomerSupportController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> AddMessage(PostMessageViewModel model)
        {
            var connection = "Ange connectionString för ditt storage account";
            var client = new QueueClient(connection, "ange namnet på kön");

            await client.CreateIfNotExistsAsync();

            var supportMessage = new
            {
                sender = model.SenderEmail,
                message = model.Message
            };

            var message = JsonSerializer.Serialize(supportMessage);

            var response = await client.SendMessageAsync(message);
            var receipt = response.Value;

            return StatusCode(201, new { messageId = receipt.MessageId });
        }
    }
}