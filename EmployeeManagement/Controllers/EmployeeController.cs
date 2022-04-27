using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly DataContext _dataContext;
        public EmployeeController(DataContext context)
        {
            _dataContext = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Employee>>> Get()
        {            
            return Ok(await _dataContext.Employees.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> Get(int id)
        {
            var employee = await _dataContext.Employees.FindAsync(id);
            if (employee == null) return BadRequest("Employee not found.");
            else return Ok(employee);
        }

       [HttpPost]
       public async Task<ActionResult<List<Employee>>> Post(Employee employee)
        {
            _dataContext.Employees.Add(employee);
            await _dataContext.SaveChangesAsync();

            return Ok(await _dataContext.Employees.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Employee>>> UpdateEmployee(Employee request)
        {
            var dbEmployee = await _dataContext.Employees.FindAsync(request.Id);
            if (dbEmployee == null) return BadRequest("Employee not found.");

            dbEmployee.FirstName = request.FirstName;
            dbEmployee.LastName = request.LastName;
            dbEmployee.Birthdate = request.Birthdate;

            await _dataContext.SaveChangesAsync();

            return Ok(await _dataContext.Employees.ToListAsync());
        }

        [HttpDelete]
        public async Task<ActionResult<List<Employee>>> Delete(int id)
        {
            var dbEmployee = await _dataContext.Employees.FindAsync(id);
            if (dbEmployee == null) return BadRequest("Employee not found.");

            _dataContext.Employees.Remove(dbEmployee);
            await _dataContext.SaveChangesAsync();

            return Ok(await _dataContext.Employees.ToListAsync());
        }
    }
}
