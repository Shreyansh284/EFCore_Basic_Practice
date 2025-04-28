using Microsoft.AspNetCore.Mvc;
using WebApiEFCore.Data;
using WebApiInEfCore.Models;

namespace WebApiEFCore.Controllers
{
    public class EmployeesProjectsController : Controller
    {
        private readonly AppDbContext context;
        public EmployeesProjectsController(AppDbContext context) {
        this.context=context;
        }
        [HttpGet("EmployeesProjectsDetail")] // Only this needed
        public IActionResult Index()
        {
            var employeesProjectsdDetails = context.EmployeeProjects.Join(context.Employees, ep => ep.EmployeeId, e => e.EmployeeId, (ep, e) => new { e.EmployeeName, ep.ProjectId }).Join(context.Projects, epp => epp.ProjectId, p => p.ProjectId, (epp, p) => new { epp.EmployeeName, p.ProjectName }).ToList();
            return Ok(employeesProjectsdDetails);       
        }
        [HttpPost("AddEmployeesProject")]
        public IActionResult Add(EmployeeProject employeeProject)
        {
            context.Add(employeeProject);
            context.SaveChanges();
            return Ok("Added Successfully");  
        }


    }
}
