using System.Threading.Tasks;
using Messages;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Publisher.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ValueController : ControllerBase
    {
        private readonly DaprClient _client;

        private readonly ILogger<ValueController> _logger;

        public ValueController(DaprClient client, ILogger<ValueController> logger)
        {
            _client = client;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult> Post(string text)
        {
            await _client.PublishEventAsync<TextMessage>("pubsub", "newOrder", new TextMessage()
            {
                Text = text
            });

            return Ok();
        }
    }
}
