﻿using BLL.DTOS.Order;

namespace DAL.Interfaces
{
    public interface IOrderService
    {
        Task AddOrder(OrderCreateDTO orderCreateDTO, int clientId);
        IEnumerable<OrderDTO> GetOrdersByClient(int clientId);



    }
}
