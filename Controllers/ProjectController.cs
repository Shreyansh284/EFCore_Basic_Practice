using Microsoft.AspNetCore.Mvc;
using WebApiEFCore.Data;
using WebApiInEfCore.Models;

namespace WebApiEFCore.Controllers
{
    public class ProjectController : Controller
    {

        private readonly AppDbContext context;

        public ProjectController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet("ProjectDetail")] // Only this needed
        public IActionResult Index()
        {
            var emp = context.Projects.ToList();
            return Ok(emp);
        }

        [HttpPost("AddProject")]

        public IActionResult Add(Project project)
        {
       
            context.Add(project);
            context.SaveChanges();
            return Ok();
        }


        [HttpPatch("EditProject")]
        public IActionResult Edit(Project projectDetail)
        {
            var project = context.Projects.Single(e => e.ProjectId == projectDetail.ProjectId);
            project.ProjectName = projectDetail.ProjectName;
            context.SaveChanges();
            return Ok();
        }
        [HttpDelete("DeleteProject/{projectId}")]
        public IActionResult Delete(int projectId)
        {
            var project = context.Projects.Single(e => e.ProjectId == projectId);
            context.Remove(project);
            context.SaveChanges();
            return Ok();
        }
    }
}
