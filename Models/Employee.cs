using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace WebApiInEfCore.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }    

        public int ManagerId { get; set; }

        [ValidateNever]
        [JsonIgnore]
        public Manager Manager { get; set; } // Navigation property
        [ValidateNever]
        [JsonIgnore]
        public ICollection<EmployeeProject> EmployeeProjects { get; set; } // Collection property


    }
}
