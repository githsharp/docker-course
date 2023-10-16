using System.Text.Json;
using Azure.Messaging.ServiceBus;
using Microsoft.AspNetCore.Mvc;
using vehicle_api.ViewModels;

namespace vehicle_api.Controllers
{
    [ApiController]
    [Route("api/vehicles")]
    public class VehiclesController : ControllerBase
    {
        private readonly ServiceBusClient _client;
        private readonly ServiceBusSender _sender;

        public VehiclesController(IConfiguration config)
        {
            var options = new ServiceBusClientOptions()
            {
                TransportType = ServiceBusTransportType.AmqpWebSockets
            };

            var connection = config.GetSection("ServiceBusConfig:Connection").Value;
            // Skapa anslutningen med anslutningssträng samt inställningar för att använda WebSockets som protokoll...
            _client = new ServiceBusClient(connection, options);
            // Skapa en avsändare till en utvald kö...
            _sender = _client.CreateSender(config.GetSection("ServiceBusConfig:Queue").Value);
        }

        [HttpPost]
        public async Task<IActionResult> AddVehicles(IList<PostVehiclesViewModel> vehicles)
        {
            // Skapa en meddelande batch(paket)...
            using ServiceBusMessageBatch batch = await _sender.CreateMessageBatchAsync();

            foreach (var vehicle in vehicles)
            {
                var message = JsonSerializer.Serialize(vehicle);

                // Vi försöker lägga till meddelandet(bilen) till paketet...
                if (!batch.TryAddMessage(new ServiceBusMessage(message)))
                {
                    throw new Exception("Meddelandet är för stort!!!");
                }
            }

            try
            {
                await _sender.SendMessagesAsync(batch);
                return StatusCode(201, new { message = "Bilarna är mottagna" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            finally
            {
                await _sender.DisposeAsync();
                await _client.DisposeAsync();
            }
        }
    }
}