using Microsoft.AspNetCore.Mvc;
using WebApiEFCore.Data;
using WebApiInEfCore.Models;

namespace WebApiEFCore.Controllers
{
    [ApiController]
    public class EmployeesProjectsController : Controller
    {
        private readonly AppDbContext context;
        public EmployeesProjectsController(AppDbContext context) {
        this.context=context;
        }

        [HttpPost("AddEmployeesProject")]
        public async Task<IActionResult> Add([FromBody]EmployeeProject employeeProject)
        {
            await context.AddAsync(employeeProject);
            await context.SaveChangesAsync();
            return Ok("Added Successfully");  
        }


    }
}
