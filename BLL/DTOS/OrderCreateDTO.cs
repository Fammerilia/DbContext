namespace DLL.DTOS
{
    public class OrderCreateDTO
    {

        public DateTime OrderDate { get; set; }

        public string OrderDescription { get; set; }

        public decimal OrderAmount { get; set; }

        public int OrderStatus { get; set; }
    }

}
