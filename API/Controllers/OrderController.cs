using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using API.GlobalException;
using System.ComponentModel.DataAnnotations;
using System.Net.Security;
using AutoMapper;
using API;
using API.Utility;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using DAL;
using BLL.DTOS.Order;


[Authorize]
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

