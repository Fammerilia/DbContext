using AutoMapper;
using DAL.Interfaces;
using DLL.DTOS;
using Microsoft.EntityFrameworkCore;
using WebApplication2.GlobalException;

namespace DAL.Services
{
    public class ClientAddressService : IClientAddressService
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _dbContext;

        public ClientAddressService(IMapper mapper, ApplicationDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task DeleteClientAddress(int clientId, int clientAddressId)
        {
            var client = await _dbContext.Clients
                .Include(c => c.ClientAddresses)
                .FirstOrDefaultAsync(c => c.ClientId == clientId);

            if (client == null)
            {
                throw new NotFoundException("Client not found.");
            }

            var clientAddress = client.ClientAddresses.FirstOrDefault(ca => ca.ClientAddressId == clientAddressId);
            if (clientAddress == null)
            {
                throw new NotFoundException("Client address not found.");
            }

            _dbContext.ClientAddresses.Remove(clientAddress);
            await _dbContext.SaveChangesAsync();
        }
        public async Task AddAddress(ClientAddressCreateDTO clientAddressCreateDTO, int clientId)
        {
            var client = await _dbContext.Clients.FindAsync(clientId);

            var address = _mapper.Map<ClientAddresses>(clientAddressCreateDTO);
            address.ClientId = clientId;

            _dbContext.ClientAddresses.Add(address);
            await _dbContext.SaveChangesAsync();
        }
    }
}
