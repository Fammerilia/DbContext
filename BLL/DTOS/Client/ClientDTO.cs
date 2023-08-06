using BLL.DTOS.Addresses;
using BLL.DTOS.Contacts;
using BLL.DTOS.Emails;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace BLL.DTOS.Client
{
    public class ClientDTO
    {
        public int ClientId { get; set; }
        public string EmpName { get; set; }
        
        public string EmpSurname { get; set; }

        public string EmpMiddlename { get; set; }
        public int EmpSex { get; set; }
        public int OrderCount { get; set; }
        public int Status { get; set; }
        public int DiscountType { get; set; }
        public List<ClientEmailDTO> ClientEmail { get; set; }
        public List<ClientContactDTO> ClientContact { get; set; }

        public List<ClientAddressesDTO> ClientAddresses { get; set; }
    }
}
