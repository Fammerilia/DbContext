using BLL.DTOS.Client;
using BLL.DTOS.Contacts;
using BLL.DTOS.Search;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

public interface IClientService
{
    Client AddClient(ClientCreateDTO clientCreateDTO);



    IEnumerable<SearchResultDTO> SearchClients(
        string? empName,
        string? empSurname,
        string? empMiddlename,
        string? contact,
        string? address,
        string?  email,
        string? orderIdPartial,
        string? orderDescription,
        int? empSex,
        int? orderCount,
        int? status,
        int? discountType);
    Task DellClient(Client client);
    ClientDTO GetClient(int clientId);
    Task UpdateClient(Client client);
    public Client GetClientById(int clientId);
}