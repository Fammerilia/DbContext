using AutoMapper;
using DAL.Services;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using DAL.Interfaces;
using DLL.DTOS;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.GlobalException;

namespace DAL.Services
{
    public class ClientContactService : IClientContactService
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _dbContext;

        public ClientContactService(IMapper mapper, ApplicationDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }
        public async Task AddContact(ClientContactCreateDTO clientContactCreateDTO, int clientId)
        {
            var client = await _dbContext.Clients.FindAsync(clientId);



            var contact = _mapper.Map<ClientContact>(clientContactCreateDTO);
            contact.ClientId = clientId;

            _dbContext.ClientContacts.Add(contact);
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteClientContact(int clientId, int clientContactId)
        {
            var client = await _dbContext.Clients
                .Include(c => c.ClientAddresses)
                .FirstOrDefaultAsync(c => c.ClientId == clientId);

            if (client == null)
            {
                throw new NotFoundException("Client not found.");
            }

            var clientContact = client.ClientContacts.FirstOrDefault(cc => cc.ClientContactId == clientContactId);
            if (clientContact == null)
            {
                throw new NotFoundException("Client Contact not found.");
            }

            _dbContext.ClientContacts.Remove(clientContact);
            await _dbContext.SaveChangesAsync();
        }
    }
}
