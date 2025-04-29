using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiEFCore.Data;
using WebApiInEfCore.Models;

namespace WebApiEFCore.Controllers
{
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly AppDbContext context;

        public EmployeeController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet("EmployeeDetail")] // Only this needed
        public async Task<IActionResult> Index()
        {
            
            var emp = 
                   await context.Employees
                                .Select(e => new
                                {
                                        e.EmployeeId,   
                                        e.EmployeeName,
                                        ManagerName = e.Manager.ManagerName,
                                        Projects = e.EmployeeProjects.Select(ep => new {
                                        ep.Project.ProjectId,
                                        ep.Project.ProjectName
                                })
                    })
                    .ToListAsync();
            return Ok(emp);
        }

        [HttpPost("AddEmployee")]

        public async Task<IActionResult> Add(Employee employee)
        {
            await context.AddAsync(employee);
            await context.SaveChangesAsync();
            return Ok();
        }
        [HttpPatch("EditEmployee/{employeeId}")]
        public async Task<IActionResult> Edit([FromRoute]int employeeId,[FromBody] Employee employeeDetail)
        {
            var employee =await context.Employees.FindAsync(employeeId);
            employee.EmployeeName = employeeDetail.EmployeeName;
            employee.ManagerId = employeeDetail.ManagerId;
            await context.SaveChangesAsync();
            return Ok();

        }
        [HttpDelete("DeleteEmployee/{employeeId}")]
        public async Task<IActionResult> Delete(int employeeId)
        {   
            var employee = await context.Employees.FindAsync(employeeId);
             context.Remove(employee);
           await context.SaveChangesAsync();
            return Ok();
        }
    }
}
