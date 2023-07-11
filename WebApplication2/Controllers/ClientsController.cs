using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly IClientService _clientService;

    public ClientsController(IClientService clientService)
    {
        _clientService = clientService;
    }

    [HttpPost("add")]
    public IActionResult AddClient(Client client)
    {
        _clientService.AddClient(client);
        return Ok("Client Created");
    }

    [HttpPost("delete/{empId}")]
    public IActionResult DellClient(int empId)
    {
        var client = new Client { EmpId = empId };
        _clientService.DellClient(client);
        return Ok("Client Deleted");
    }


    [HttpPost("Update")]
    public IActionResult UpdateClient(Client client)
    {
        _clientService.UpdateClient(client);
        return Ok("Client updated");
    }

}
