using System.ComponentModel.DataAnnotations;

namespace WebApiInEfCore.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }

        public int ManagerId { get; set; }
        public Manager Manager { get; set; } // Navigation property

        public ICollection<EmployeeProject> EmployeeProjects { get; set; } // Collection property


    }
}
