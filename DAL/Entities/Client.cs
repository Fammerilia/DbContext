using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

public class Client
{
    public int ClientId { get; set; }
    public string EmpName { get; set; }
    public int EmpSex { get; set; }
    public int OrderCount { get; set; }
    public int Status { get; set; }
    public int DiscountType { get; set; }
    public List<ClientContact> ClientContacts { get; set; }
    public List<ClientAddresses> ClientAddresses { get; set; }
    public List<ClientEmail> ClientEmails { get; set; }
    public List<Order> Orders { get; set; }


}
