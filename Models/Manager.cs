using System.ComponentModel.DataAnnotations;

namespace WebApiInEfCore.Models
{
    public class Manager
    {
        [Key]
        public int ManagerId { get; set; }
        public string ManagerName { get; set; }

        public ICollection<Employee> Employees { get; set; } // Collection property
    }
}
