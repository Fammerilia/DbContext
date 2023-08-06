using BLL.DTOS.Addresses;
using BLL.DTOS.Client;
using BLL.DTOS.Contacts;
using BLL.DTOS.Emails;
using BLL.DTOS.Order;

namespace BLL.DTOS.Search
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
