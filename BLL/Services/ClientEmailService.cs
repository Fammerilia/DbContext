using DAL.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using API.GlobalException;
using BLL.DTOS.Emails;

namespace DAL.Services
{
    public class ClientEmailService : IClientEmailService
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _dbContext;

        public ClientEmailService(IMapper mapper, ApplicationDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }
        public async Task AddEmail(ClientEmailCreateDTO clientEmailCreateDTO, int clientId)
        {
            var client = await _dbContext.Clients.FindAsync(clientId);
            var email = _mapper.Map<ClientEmail>(clientEmailCreateDTO);
            email.ClientId = clientId;

            _dbContext.ClientEmails.Add(email);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteClientEmail(int clientId, int clientEmailId)
        {
            var client = await _dbContext.Clients
                .Include(c => c.ClientEmails)
                .FirstOrDefaultAsync(c => c.ClientId == clientId);

            if (client == null)
            {
                throw new NotFoundException("Client not found.");
            }

            var clientEmail = client.ClientEmails.FirstOrDefault(cc => cc.ClientEmailId == clientEmailId);
            if (clientEmail == null)
            {
                throw new NotFoundException("Client Email not found.");
            }

            _dbContext.ClientEmails.Remove(clientEmail);
            await _dbContext.SaveChangesAsync();
        }
    }

}