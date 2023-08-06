using AutoMapper;
using BLL.DTOS.Search;
using Microsoft.EntityFrameworkCore;
using DAL.Services;
using BLL.DTOS.Addresses;
using BLL.DTOS.Contacts;
using BLL.DTOS.Client;
using BLL.DTOS.Emails;
using BLL.DTOS.Order;
using DAL;
using System.Linq;

public class ClientService : IClientService
{
    private readonly IMapper _mapper;
    private readonly ApplicationDbContext _dbContext;

    public ClientService(IMapper mapper, ApplicationDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }
    public Client AddClient(ClientCreateDTO clientCreateDTO)
    {
        var client = new Client
        {
            EmpName = clientCreateDTO.EmpName,
            EmpSurname = clientCreateDTO.EmpSurname,
            EmpMiddlename = clientCreateDTO.EmpMiddlename,
            EmpSex = clientCreateDTO.EmpSex,
        };

        _dbContext.Clients.Add(client);
        _dbContext.SaveChanges();

        return (client);
    }
    public ClientDTO GetClient(int clientId)
    {
        var client = _dbContext.Clients
            .Where(c => c.ClientId == clientId)
            .FirstOrDefault();

        var clientAddresses = _dbContext.ClientAddresses
            .Where(cc => cc.ClientId == clientId)
            .ToList();

        var clientContacts = _dbContext.ClientContacts
            .Where(cc => cc.ClientId == clientId)
            .ToList();

        var clientEmails = _dbContext.ClientEmails
            .Where(ce => ce.ClientId == clientId)
            .ToList();


        var clientDTO = _mapper.Map<ClientDTO>(client);
        clientDTO.ClientContact = _mapper.Map<List<ClientContactDTO>>(clientContacts);
        clientDTO.ClientEmail = _mapper.Map<List<ClientEmailDTO>>(clientEmails);
        clientDTO.ClientAddresses = _mapper.Map<List<ClientAddressesDTO>>(clientAddresses);

        return clientDTO;
    }





    public IEnumerable<SearchResultDTO> SearchClients(
        string? empName,
        string? empSurname,
        string? empMiddlename,
        string? contact,
        string? address,
        string? email,
        string? orderDescription,
        string? orderIdPartial,
        int? empSex,
        int? orderCount,
        int? status,
        int? discountType)
    {
        var clients = _dbContext.Clients
            .Include(c => c.ClientContacts)
            .Include(c => c.ClientAddresses)
            .Include(c => c.ClientEmails)
            .Include(c => c.Orders)
            .Where(c =>
                (string.IsNullOrEmpty(contact) || c.ClientContacts.Any(cc => cc.EmpContact.Contains(contact))) &&
                (string.IsNullOrEmpty(address) || c.ClientAddresses.Any(ca => ca.EmpAddress.Contains(address))) &&
                (string.IsNullOrEmpty(email) || c.ClientEmails.Any(ce => ce.EmpEmail.Contains(email))) &&
                (string.IsNullOrEmpty(orderIdPartial) || c.Orders.Any(o => o.OrderId.ToString().Contains(orderIdPartial))) &&
                (string.IsNullOrEmpty(orderDescription) || c.Orders.Any(o => o.OrderDescription.Contains(orderDescription))) &&
                (string.IsNullOrEmpty(empName) || c.EmpName.Contains(empName)) &&
                (string.IsNullOrEmpty(empSurname) || c.EmpSurname.Contains(empSurname)) &&
                (string.IsNullOrEmpty(empMiddlename) || c.EmpMiddlename.Contains(empMiddlename)) 
                && (!empSex.HasValue || c.EmpSex == empSex.Value)
                && (!orderCount.HasValue || c.OrderCount == orderCount.Value)
                && (!status.HasValue || c.Status == status.Value)
                && (!discountType.HasValue || c.DiscountType == discountType.Value)
            )
            .ToList();

        var searchResults = clients.Select(client => new SearchResultDTO
        {
            ClientInfo = _mapper.Map<ClientDDTO>(client),
            ClientContacts = _mapper.Map<IEnumerable<ClientContactDTO>>(client.ClientContacts),
            ClientAddresses = _mapper.Map<IEnumerable<ClientAddressesDTO>>(client.ClientAddresses),
            ClientEmails = _mapper.Map<IEnumerable<ClientEmailDTO>>(client.ClientEmails),
            Orders = _mapper.Map<IEnumerable<OrderDTO>>(client.Orders)
        }) ;

        return searchResults;
    }





    public async Task DellClient(Client client)
    {
        var existingClient = _dbContext.Clients.FirstOrDefault(c => c.ClientId == client.ClientId);

        if (existingClient != null)
        {
            _dbContext.Clients.Remove(existingClient);
            await _dbContext.SaveChangesAsync();
        }
    }

    public Client GetClientById(int clientId)
    {
        return _dbContext.Clients.FirstOrDefault(c => c.ClientId == clientId);
    }

    public async Task UpdateClient(Client client)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            _dbContext.Clients.Update(client);
            await _dbContext.SaveChangesAsync();
        }
}

