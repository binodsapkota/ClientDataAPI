using Microsoft.AspNetCore.Mvc;
using ClientDataAPI.Services;
using ClientDataAPI.Data;

namespace ClientDataAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly ClientService _clientService;

        public ClientsController(ClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public IActionResult GetAllClients()
        {
            var clients = _clientService.GetAllClients();
            return Ok(clients);
        }

        [HttpGet("{id}")]
        public IActionResult GetClient(int id)
        {
            var client = _clientService.GetClientById(id);
            if (client == null) return NotFound();
            return Ok(client);
        }

        [HttpPost]
        public IActionResult CreateClient([FromBody] Client client)
        {
            _clientService.CreateClient(client);
            return CreatedAtAction(nameof(GetClient), new { id = client.Id }, client);
        }
    }
}
