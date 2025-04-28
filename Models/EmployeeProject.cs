namespace WebApiInEfCore.Models
{
    public class EmployeeProject
    {
        public int EmployeeProjectId { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; } // Navigation property
        public int ProjectId { get; set; }
        public Project Project { get; set; } // Navigation property

    }
}
