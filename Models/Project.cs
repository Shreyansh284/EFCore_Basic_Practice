using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace WebApiInEfCore.Models
{
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        [ValidateNever]
        [JsonIgnore]
        public ICollection<EmployeeProject> EmployeeProjects { get; set; } // Collection property

    }
}
