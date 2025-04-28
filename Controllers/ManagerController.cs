using Microsoft.AspNetCore.Mvc;
using WebApiEFCore.Data;
using WebApiInEfCore.Models;

namespace WebApiEFCore.Controllers
{
    public class ManagerController : Controller 
    {
        private readonly AppDbContext context;

        public ManagerController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet("ManagerDetail")] // Only this needed
        public IActionResult Index()
        {
            var emp = context.Managers.ToList();
            return Ok(emp);
        }

        [HttpPost("AddManager")]

        public IActionResult Add(Manager manager)
        {
            context.Add(manager);
            context.SaveChanges();
            return Ok();
        }
        [HttpPatch("EditManager")]
        public IActionResult Edit(Manager managerDetail)
        {
            var manager = context.Managers.Single(e => e.ManagerId == managerDetail.ManagerId);
            manager.ManagerName = managerDetail.ManagerName;
            context.SaveChanges();
            return Ok();
        }
        [HttpDelete("DeleteManager/{managerId}")]
        public IActionResult Delete(int managerId)
        {
            var manager = context.Managers.Single(e => e.ManagerId == managerId);
            context.Remove(manager);
            context.SaveChanges();
            return Ok();
        }
        }
}
