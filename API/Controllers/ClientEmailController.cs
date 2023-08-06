﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using API.GlobalException;
using System.ComponentModel.DataAnnotations;
using System.Net.Security;
using AutoMapper;
using API;
using API.Utility;
using DAL.Interfaces;
using BLL.DTOS.Emails;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ClientEmailController : ControllerBase
{
    private readonly IClientEmailService _clientService;


    public ClientEmailController(IClientEmailService clientService)
    {
        _clientService = clientService;
    }

    [HttpPost("{clientId}/addEmail")]
    public async Task<IActionResult> AddEmail(int clientId, ClientEmailCreateDTO clientEmailCreateDTO)
    {
        await _clientService.AddEmail(clientEmailCreateDTO, clientId);
        return Ok("Email added successfully.");
    }


    [HttpDelete("{clientId},{clientEmailId}/deleteemail")]
    public async Task<IActionResult> DeleteClientEmail(int clientId, int clientEmailId)
    {
        try
        {
            await _clientService.DeleteClientEmail(clientId, clientEmailId);
            return NoContent();
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

}


