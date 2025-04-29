using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace WebApiInEfCore.Models
{
    public class EmployeeProject
    {
        public int EmployeeProjectId { get; set; }
        public int EmployeeId { get; set; }
        [ValidateNever]
        [JsonIgnore]
        public Employee Employee { get; set; } // Navigation property
        public int ProjectId { get; set; }
        [ValidateNever]
        [JsonIgnore]
        public Project Project { get; set; } // Navigation property

    }
}
