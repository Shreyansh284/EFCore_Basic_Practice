using Microsoft.AspNetCore.Mvc;
using WebApiEFCore.Data;
using WebApiInEfCore.Models;

namespace WebApiEFCore.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly AppDbContext context;

        public EmployeeController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet("EmployeeDetail")] // Only this needed
        public IActionResult Index()
        {
            var emp = context.Employees
    .Join(context.Managers,
        e => e.ManagerId,
        m => m.ManagerId,
        (e, m) => new { e.EmployeeId, e.EmployeeName, ManagerName = m.ManagerName })
    .Join(context.EmployeeProjects,
        em => em.EmployeeId,
        ep => ep.EmployeeId,
        (em, ep) => new { em.EmployeeName, em.ManagerName, ep.ProjectId })
    .Join(context.Projects,
        empEp => empEp.ProjectId,
        p => p.ProjectId,
        (empEp, p) => new
        {
            EmployeeName = empEp.EmployeeName,
            ManagerName = empEp.ManagerName,
            ProjectName = p.ProjectName
        })
    .ToList();
            return Ok(emp);
        }

        [HttpPost("AddEmployee")]

        public IActionResult Add(Employee employee)
        {
            //var employee = new Employee();
            //employee.EmployeeName = "Xemp";
            //employee.ManagerId = 1;
         

            context.Add(employee);
            context.SaveChanges();
            return Ok();
        }
        [HttpPatch("EditEmployee")]
        public IActionResult Edit(Employee employeeDetail)
        {
            var employee = context.Employees.Single(e=>e.EmployeeId== employeeDetail.EmployeeId);
            employee.EmployeeName = employeeDetail.EmployeeName;
            context.SaveChanges();
            return Ok();

        }
        [HttpDelete("DeleteEmployee/{employeeId}")]
        public IActionResult Delete(int employeeId)
        {
            var employee = context.Employees.Single(e => e.EmployeeId == employeeId);
            context.Remove(employee);
            context.SaveChanges();
            return Ok();
        }
    }
}
