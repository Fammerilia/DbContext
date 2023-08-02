using DLL.DTOS;

namespace DLL.DTOS.Search
{
    public class SearchResultDTO
    {
        public ClientDDTO ClientInfo { get; set; }
        public IEnumerable<ClientContactDTO> ClientContacts { get; set; }
        public IEnumerable<ClientAddressesDTO> ClientAddresses { get; set; }
        public IEnumerable<ClientEmailDTO> ClientEmails { get; set; }
        public IEnumerable<OrderDTO> Orders { get; set; }
    }
}
