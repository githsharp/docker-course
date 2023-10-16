using Azure.Messaging.ServiceBus;
using Microsoft.AspNetCore.Mvc;
using valuation_api.ViewModels;

namespace valuation_api.Controllers
{
    [ApiController]
    [Route("api/valuations")]
    public class ValuationsController : ControllerBase
    {
        private readonly ServiceBusClient _client;
        private readonly ServiceBusReceiver _receiver;

        public ValuationsController(IConfiguration config)
        {
            // Konfigurerar vi att det är websockets ska användas
            var options = new ServiceBusClientOptions()
            {
                TransportType = ServiceBusTransportType.AmqpWebSockets
            };

            // Hämtar vi anslutningssträngen till vår ServiceBus
            // appsetting.?.json
            var connection = config.GetSection("ServiceBusConfig:Connection").Value;
            // Skapar en anslutning till vår ServiceBus...
            _client = new ServiceBusClient(connection, options);
            // Skapar vi en läsare/mottagare av kön som vi hämtar ifrån appsetting.?.json
            _receiver = _client.CreateReceiver(config.GetSection("ServiceBusConfig:Queue").Value);
        }

        [HttpGet("message")]
        public async Task<IActionResult> ReadMessage()
        {
            try
            {
                // Vi hämtar ett stycken meddelande ifrån kön i vår ServiceBus...
                var message = await _receiver.ReceiveMessageAsync();

                // Defensiv programmering, vi kontrollerar om det finns något meddelande i kön.
                if (message is null) return Ok(new { message = "Det finns ingenting i kön" });

                // Nu säger vi att vi har hämta meddelandet så nu kan ServiceBus ta bort det ur kön...
                await _receiver.CompleteMessageAsync(message);

                // Tar vi meddelandet som är i Json format och gör om det till ett objekt av typen GetVehicleViewModel...
                // Returnerar vi ett Json objekt tillbaka med egenskaperna success = true och data med GetVehicle
                // objektet omgjort till Json...
                return Ok(new { success = true, data = message.Body.ToObjectFromJson<GetVehicleViewModel>() });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            finally
            {
                await _receiver.DisposeAsync();
                await _client.DisposeAsync();
            }
        }

        [HttpGet("messages")]
        public async Task<IActionResult> ReadMessages()
        {
            try
            {
                var messages = await _receiver.ReceiveMessagesAsync(maxMessages: 10);
                var messageList = new List<GetVehicleViewModel>();

                foreach (var message in messages)
                {
                    messageList.Add(message.Body.ToObjectFromJson<GetVehicleViewModel>());
                    await _receiver.CompleteMessageAsync(message);
                }

                return Ok(new { success = true, data = messageList });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            finally
            {
                await _receiver.DisposeAsync();
                await _client.DisposeAsync();
            }
        }
    }
}