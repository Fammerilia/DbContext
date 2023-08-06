namespace BLL.DTOS.Client
{
    public class ClientDDTO
    {
        public int ClientId { get; set; }
        public string EmpName { get; set; }

        public string EmpSurname { get; set; }
        public string EmpMiddlename { get; set; }

        public int EmpSex { get; set; }
        public int OrderCount { get; set; }
        public int Status { get; set; }
        public int DiscountType { get; set; }
    }
}
