using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.GlobalException;
using System.ComponentModel.DataAnnotations;
using System.Net.Security;
using AutoMapper;
using WebApplication2;
using WebApplication2.Utility;
using DLL.DTOS;

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
    public  IActionResult AddClient(ClientCreateDTO clientCreateDTO)
    {
        if (!ModelState.IsValid)
        {
            throw new BadRequestException("Something went wrong");
        }

        var client = _clientService.AddClient(clientCreateDTO);
        return Ok(client);
    }


    [HttpGet]
    public IActionResult SearchClients(
        [FromQuery] string? empName,
        [FromQuery] string? contact,
        [FromQuery] string? address,
        [FromQuery] string? email,
        [FromQuery] string? orderIdPartial,
        [FromQuery] string? orderDescription,
        [FromQuery] int? empSex,
        [FromQuery] int? orderCount,
        [FromQuery] int? status,
        [FromQuery] int? discountType)
    {
        var searchResults = _clientService.SearchClients(
            empName,
            contact,
            address,
            email,
            orderDescription,
            orderIdPartial,
            empSex,
            orderCount,
            status,
            discountType);

        if (searchResults == null)
        {
            throw new NotFoundException("Client not found.");
        }
        return Ok(searchResults);
    }


    [HttpPost("delete")]
    public async Task <IActionResult> DeleteClient(int clientId)
    {
        var client = new Client { ClientId = clientId };
        if (client == null)
            throw new NotFoundException("Client not found");
        return Ok("Client Deleted");
    }

    [HttpGet("{clientId}")]
    public IActionResult GetClient(int clientId)
    {
        var clientDTO = _clientService.GetClient(clientId);

        if (clientDTO == null)
        {
            throw new NotFoundException("There is no Client for this ClientId");
        }

        return Ok(clientDTO);
    }

    [HttpPut("update")]
    public async Task <IActionResult> UpdateClient(ClientUpdateDTO clientUpdateDTO)
    {
        var client = _clientService.GetClientById(clientUpdateDTO.ClientId);

        if (client == null)
        {
            throw new NotFoundException($"Client with ID {clientUpdateDTO.ClientId} not found.");
        }

        client.EmpName = clientUpdateDTO.EmpName;
        client.EmpSex = clientUpdateDTO.EmpSex;
        client.OrderCount = clientUpdateDTO.OrderCount;
        client.Status = clientUpdateDTO.Status;
        client.DiscountType = clientUpdateDTO.DiscountType;

        await _clientService.UpdateClient(client);

        return Ok("Client updated successfully.");
    }
}