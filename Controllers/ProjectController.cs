using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiEFCore.Data;
using WebApiInEfCore.Models;

namespace WebApiEFCore.Controllers
{
    [ApiController]
    public class ProjectController : Controller
    {

        private readonly AppDbContext context;

        public ProjectController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet("ProjectDetail")] // Only this needed
        public async Task<IActionResult> Index()
        {
            
            var emp = await context.Projects.Select(e => new
            {
                e.ProjectId,
                e.ProjectName,
                
                Employee = e.EmployeeProjects.Select(ep => new {
                    ep.Employee.EmployeeId,
                    ep.Employee.EmployeeName
                })
            }).ToListAsync();
            return Ok(emp);
        }

        [HttpPost("AddProject")]

        public async Task<IActionResult> Add([FromBody] Project project)
        {
       
            await context.AddAsync(project);
            await context.SaveChangesAsync();
            return Ok();
        }


        [HttpPatch("EditProject/{projectId}")]
        public async Task<IActionResult> Edit([FromRoute] int projectId,[FromBody] Project projectDetail)
        {
            var project = await context.Projects.FindAsync(projectId);
            if(project!=null)
            {

            project.ProjectName = projectDetail.ProjectName;
            await context.SaveChangesAsync();
            return Ok();
            }
            return StatusCode(400, "Project Not Found.");
        }
        [HttpDelete("DeleteProject/{projectId}")]
        public async Task<IActionResult> Delete(int projectId)
        {
            var project =await context.Projects.FindAsync(projectId);
            if(project!=null)
            {

            context.Remove(project);
            await context.SaveChangesAsync();
            return Ok();
            }
            return StatusCode(400, "Project Not Found.");
        }
    }
}
