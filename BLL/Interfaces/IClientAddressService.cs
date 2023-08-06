using BLL.DTOS.Addresses;
using Microsoft.AspNetCore.Mvc;

namespace DAL.Interfaces
{
    public interface IClientAddressService
    {
        Task AddAddress(ClientAddressCreateDTO clientAddressCreateDTO, int clientId);
        Task DeleteClientAddress(int clientId, int clientAddressId);

    }
}
