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
public class ClientContactController : ControllerBase
{
    private readonly IClientContactService _clientService;


    public ClientContactController(IClientContactService clientService)
    {
        _clientService = clientService;
    }
    [HttpPost("{clientId}/addContact")]
    public async Task<IActionResult> AddContact(int clientId, ClientContactCreateDTO clientContactCreateDTO)
    {
        await _clientService.AddContact(clientContactCreateDTO, clientId);
        return Ok("Contact added successfully.");
    }

    [HttpDelete("{clientId},{clientContactId}/deletecontact")]
    public async Task<IActionResult> DeleteClientContact(int clientId, int clientContactId)
    {
        try
        {
            await _clientService.DeleteClientContact(clientId, clientContactId);
            return NoContent();
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}

