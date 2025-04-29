using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiEFCore.Data;
using WebApiInEfCore.Models;

namespace WebApiEFCore.Controllers
{
    [ApiController]
    public class ManagerController : Controller 
    {
        private readonly AppDbContext context;

        public ManagerController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet("ManagerDetail")] // Only this needed
        public async Task<IActionResult> Index()
        {
            var emp = await context.Managers
                .Select(e => new
            {
                e.ManagerId,
                e.ManagerName,
                Employees = e.Employees.Select(ep => new {
                    ep.EmployeeId,
                    ep.EmployeeName
                              })    
            }).ToListAsync();
            return Ok(emp);
        }

        [HttpPost("AddManager")]

        public async Task<IActionResult> Add([FromBody] Manager manager)
        {
            await context.AddAsync(manager);
            await context.SaveChangesAsync();
            return Ok();
        }
        [HttpPatch("EditManager/{managerId}")]
        public async Task<IActionResult> Edit([FromBody]Manager managerDetail, [FromRoute] int managerId)
        {

            var manager = await context.Managers.FindAsync(managerId);
            if(manager!=null)
            {
            manager.ManagerName = managerDetail.ManagerName;
            await context.SaveChangesAsync();
            return Ok();

            }
            return StatusCode(400, "Manager Not Found.");
        } 

        [HttpDelete("DeleteManager/{managerId}")]
        public async Task<IActionResult> Delete(int managerId)
        {
            var manager = await context.Managers.FindAsync(managerId);
            if(manager!=null)
            {

            context.Remove(manager);
            await context.SaveChangesAsync();
            return Ok();
            }
            return StatusCode(400, "Manager Not Found.");
        }
        }
}
