using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.GlobalException;
using System.ComponentModel.DataAnnotations;
using System.Net.Security;
using AutoMapper;
using WebApplication2;
using WebApplication2.Utility;
using DAL.Interfaces;
using DLL.DTOS;

[ApiController]
[Route("api/[controller]")]
public class ClientAddressController : ControllerBase
{
    private readonly IClientAddressService _clientService;

    public ClientAddressController(IClientAddressService clientService)
    {
        _clientService = clientService;
    }

    [HttpPost("{clientId}/addaddress")]
    public async Task<IActionResult> AddAddress(int clientId, ClientAddressCreateDTO clientAddressCreateDTO)
    {   
        await _clientService.AddAddress(clientAddressCreateDTO, clientId);
        return Ok("Address added successfully.");
    }


    [HttpDelete("{clientId},{clientAddressId}/deleteaddress")]
    public async Task<IActionResult> DeleteClientAddress(int clientId, int clientAddressId)
    {
        try
        {
            await _clientService.DeleteClientAddress(clientId, clientAddressId);
            return NoContent();
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}

