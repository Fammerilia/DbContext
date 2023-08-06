using BLL.DTOS.Emails;

namespace DAL.Interfaces
{
    public interface IClientEmailService
    {
        Task AddEmail(ClientEmailCreateDTO clientEmailCreateDTO, int clientId);
        Task DeleteClientEmail(int clientId, int clientEmailId);

    }
}
