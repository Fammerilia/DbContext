using DLL.DTOS;

namespace DAL.Interfaces
{
    public interface IClientContactService
    {
        Task AddContact(ClientContactCreateDTO clientContactCreateDTO, int clientId);
        Task DeleteClientContact(int clientId, int clientAddressId);

    }
}
