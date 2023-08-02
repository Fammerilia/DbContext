using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace DLL.DTOS
{
    public class OrderDTO
    {
        public int ClientId { get; set; }
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderDescription { get; set; }
        public decimal OrderAmount { get; set; }
        public int OrderStatus { get; set; }
    }
}
