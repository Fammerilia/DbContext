using System.ComponentModel.DataAnnotations;

public class Client
{
    [Key]
    public int EmpId { get; set; }
    public string EmpName { get; set; }
    public string EmpAddress { get; set; }
}
