namespace DLL.DTOS
{
    public class ClientDTO
    {
        public int ClientId { get; set; }
        public string EmpName { get; set; }
        public int EmpSex { get; set; }
        public int OrderCount { get; set; }
        public int Status { get; set; }
        public int DiscountType { get; set; }
        public List<ClientEmailDTO> ClientEmail { get; set; }
        public List<ClientContactDTO> ClientContact { get; set; }

        public List<ClientAddressesDTO> ClientAddresses { get; set; }
    }
}
