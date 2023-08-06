using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using DAL.Interfaces;
using BLL.DTOS.Order;

namespace DAL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _dbContext;

        public OrderService(IMapper mapper, ApplicationDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }


        public IEnumerable<OrderDTO> GetOrdersByClient(int clientId)
        {
            var client = _dbContext.Clients
                .Include(c => c.Orders)
                .FirstOrDefault(c => c.ClientId == clientId);

            if (client == null)
            {
                throw new KeyNotFoundException("Client not found.");
            }

            var ordersDTO = client.Orders.Select(order => _mapper.Map<OrderDTO>(order, opt => opt.Items["ClientId"] = clientId));
            return ordersDTO;
        }
 
        public async Task AddOrder(OrderCreateDTO orderCreateDTO, int clientId)
        {
            var client = _dbContext.Clients.FirstOrDefault(c => c.ClientId == clientId);
            var order = new Order
            {
                OrderDate = orderCreateDTO.OrderDate,
                OrderDescription = orderCreateDTO.OrderDescription,
                OrderAmount = orderCreateDTO.OrderAmount,
                OrderStatus = orderCreateDTO.OrderStatus,
                ClientId = clientId
            };
            _dbContext.Orders.Add(order);
            client.OrderCount++;
            await _dbContext.SaveChangesAsync();

            int orderCount = _dbContext.Orders.Count(o => o.ClientId == clientId);
            if (orderCount > 0)
            {
                client.Status = 0;
                client.DiscountType = 10;
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}
