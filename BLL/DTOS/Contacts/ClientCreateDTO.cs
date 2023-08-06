using System.ComponentModel.DataAnnotations;

namespace BLL.DTOS.Contacts
{
    public class ClientCreateDTO
    {

        public string EmpName { get; set; }

        public string EmpSurname { get; set; }

        public string EmpMiddlename { get; set; }   


        public int EmpSex { get; set; }



    }

}
