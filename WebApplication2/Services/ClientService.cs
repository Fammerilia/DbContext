using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using WebApplication2;

public class ClientService : IClientService
{
    private readonly YourDbContext _dbContext;

    public ClientService(YourDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void AddClient(Client client)
    {
        _dbContext.Clients.Add(client);
        _dbContext.SaveChanges();
    }

    public void DellClient(Client client)
    {
        var existingClient = _dbContext.Clients.FirstOrDefault(c => c.EmpId == client.EmpId);
        if (existingClient != null)
        {
            _dbContext.Clients.Remove(existingClient);
            _dbContext.SaveChanges();
        }
    }

    public void UpdateClient(Client client) {

        _dbContext.Clients.Update(client);
        _dbContext.SaveChanges();
    }


}
