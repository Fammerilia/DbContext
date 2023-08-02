using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.GlobalException;
using System.ComponentModel.DataAnnotations;
using System.Net.Security;
using AutoMapper;
using WebApplication2;
using WebApplication2.Utility;
using DLL.DTOS;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using DAL;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _clientService;
    private readonly ApplicationDbContext _dbContext; 




    public OrderController(IOrderService clientService, ApplicationDbContext dbContext)
    {
        _clientService = clientService;
        _dbContext = dbContext;
    }

    [HttpGet("{clientId}/orders")]
    public IActionResult GetOrdersByClient(int clientId)
    {
        var orders = _clientService.GetOrdersByClient(clientId);
        return Ok(orders);
    }



    [HttpPost("addorder")]
    public async Task<IActionResult> AddOrder(OrderCreateDTO orderCreateDTO, int clientId)
    {
        var client = await _dbContext.Clients.FirstOrDefaultAsync(c => c.ClientId == clientId);

        if (client == null)
        {
            throw new NotFoundException($"Client with ID {clientId} not found.");
        }
        _clientService.AddOrder(orderCreateDTO, clientId);
        return Ok("Order added successfully.");
    }
}

