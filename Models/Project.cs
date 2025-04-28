using System.ComponentModel.DataAnnotations;

namespace WebApiInEfCore.Models
{
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }

        public ICollection<EmployeeProject> EmployeeProjects { get; set; } // Collection property

    }
}
