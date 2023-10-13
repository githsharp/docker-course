using System.Text.Json;
using Azure;
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using customer_service.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace customer_service.Controllers
{
    [ApiController]
    [Route("api/customer-services")]
    public class CustomerSupportController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> ReadMessage()
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var connection = "Ange connectionString för ditt storage account";
            var client = new QueueClient(connection, "ange namnet på kön");

            var response = await client.ReceiveMessageAsync();
            var message = response.Value;

            if (message is null)
            {
                return Ok(new { message = "Kön är tom, fantastiskt jobbat" });
            }

            var model = message.Body.ToObjectFromJson<MessageViewModel>(options);
            model.AddedOn = message.InsertedOn;

            await client.DeleteMessageAsync(message.MessageId, message.PopReceipt);

            return Ok(model);
        }

        [HttpGet("list")]
        public async Task<IActionResult> ReadAllMessages()
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var connection = "Ange connectionString för ditt storage account";
            var client = new QueueClient(connection, "ange namnet på kön");

            Response<QueueMessage[]> responses = await client.ReceiveMessagesAsync(10);

            var messages = new List<MessageViewModel>();

            foreach (var message in responses.Value)
            {
                var model = message.Body.ToObjectFromJson<MessageViewModel>(options);
                model.AddedOn = message.InsertedOn;
                messages.Add(model);
            }

            return Ok(messages);
        }
    }
}