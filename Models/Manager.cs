using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace WebApiInEfCore.Models
{
    public class Manager
    {
        [Key]
        public int ManagerId { get; set; }
        public string ManagerName { get; set; }
        [ValidateNever]
        [JsonIgnore]
        public ICollection<Employee> Employees { get; set; } // Collection property
    }
}
