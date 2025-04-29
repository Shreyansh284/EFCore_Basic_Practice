using System.Linq;
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
            return Ok("Employees Details :" + emp);
        }

        [HttpPost("AddEmployee")]

        public async Task<IActionResult> Add(Employee employee)
        {
            try
            {
                await context.AddAsync(employee);
                await context.SaveChangesAsync();
                return Ok();
            }
            catch
            {
                return StatusCode(500, "An unexpected error occurred.");
            }

        
        }
        [HttpPatch("EditEmployee/{employeeId}")]
        public async Task<IActionResult> Edit([FromRoute]int employeeId,[FromBody] Employee employeeDetail)
        {
            var employee =await context.Employees.FindAsync(employeeId);
            if(employee!=null)
            {
                employee.EmployeeName = employeeDetail.EmployeeName;
                employee.ManagerId = employeeDetail.ManagerId;
                await context.SaveChangesAsync();
                return Ok("Edited");
            }
            return StatusCode(400, "Employee Not Found.");
        }

        [HttpPatch("EditEmployeeManager/{employeeId}/{managerId}")]
        public async Task<IActionResult> EditEmployeeManager([FromBody] Employee employeeDetail, [FromRoute] int employeeId, [FromRoute] int managerId)
        {
            var employee = await context.Employees
                                        .Where(e => e.EmployeeId == employeeId)
                                        .Where(e => e.ManagerId == managerId)
                                        .FirstAsync();
            ;
            if(employee!=null)
            {
                employee.ManagerId = employeeDetail.ManagerId;
                await context.SaveChangesAsync();
                return Ok();
            }
            return StatusCode(400, "Data Not Found.");
        }
        [HttpDelete("DeleteEmployee/{employeeId}")]
        public async Task<IActionResult> Delete(int employeeId)
        {   
            var employee = await context.Employees.FindAsync(employeeId);
            if(employee!=null)
            {
             context.Remove(employee);
           await context.SaveChangesAsync();
            return Ok();
            }
            return StatusCode(400, "Employee Not Found.");
        }
    }
}
