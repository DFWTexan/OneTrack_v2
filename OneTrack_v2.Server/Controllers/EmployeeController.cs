using Microsoft.AspNetCore.Mvc;
using OneTrack_v2.DataModel;
using OneTrack_v2.Services;

namespace OneTrack_v2.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        //private Microsoft.Extensions.Configuration.IConfiguration _config;
        //private Microsoft.AspNetCore.Hosting.IWebHostEnvironment _env;
        //private Microsoft.Extensions.Logging.ILogger _logger;
        //private IHttpContextAccessor _httpContext;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
                    
        [HttpPut]
        public async Task<IActionResult> SearchEmployee(int CompanyID = 0, string? EmployeeSSN = null, string? GEID = null, string? SCORENumber = null, 
            int NationalProducerNumber = 0, string? LastName = null, string? FirstName = null,
            List<string>? AgentStatus = null, string? ResState = null, string? WrkState = null, string? BranchCode = null, int EmployeeLicenseID = 0, 
            string? LicStatus = null, string? LicState = null, string? LicenseName = null, int EmploymentID = 0)
        {
            var result = await Task.Run(() => _employeeService.SearchEmployee(CompanyID, EmployeeSSN, GEID, SCORENumber,  NationalProducerNumber, LastName, FirstName,
               AgentStatus, ResState, WrkState, BranchCode, EmployeeLicenseID, LicStatus, LicState, LicenseName, EmploymentID));

            return StatusCode(result.StatusCode, result);
        }

        //[HttpPut]
        //public async Task<IActionResult> SearchEmployee_v2([FromBody] EmployeeSearch vInput)
        //{
        //    var result = await Task.Run(() => _employeeService.SearchEmployee(vInput.CompanyID, vInput.EmployeeSSN, vInput.GEID, vInput.SCORENumber, vInput.NationalProducerNumber, vInput.LastName, vInput.FirstName,
        //       vInput.AgentStatus, vInput.ResState, vInput.WrkState, vInput.BranchCode, vInput.EmployeeLicenseID, vInput.LicStatus, vInput.LicState, vInput.LicenseName, vInput.EmploymentID));

        //    return StatusCode(result.StatusCode, result);
        //}

        //public async Task<ActionResult<IEnumerable<Employee>>> SearchEmployee_GEN(string searchString)
        //{
        //    if (_db.Employees == null)
        //    {
        //        return NotFound();
        //    }
        //    var employees = await _db.Employees.Where(x => x.FirstName.Contains(searchString) || x.LastName.Contains(searchString)).ToListAsync();
        //    if (employees == null)
        //    {
        //        return NotFound();
        //    }
        //    return employees;
        //}

        // GET: api/Employee
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        //{
        //  if (_db.Employees == null)
        //  {
        //      return NotFound();
        //  }
        //    return await _db.Employees.Take(100).ToListAsync();
        //}

        // GET: api/Employee/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Employee>> GetEmployee(int id)
        //{
        //  if (_db.Employees == null)
        //  {
        //      return NotFound();
        //  }
        //    var employee = await _db.Employees.FindAsync(id);

        //    if (employee == null)
        //    {
        //        return NotFound();
        //    }

        //    return employee;
        //}

        //// PUT: api/Employee/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutEmployee(int id, Employee employee)
        //{
        //    if (id != employee.EmployeeId)
        //    {
        //        return BadRequest();
        //    }
        //    _db.Entry(employee).State = EntityState.Modified;
        //    try
        //    {
        //        await _db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!EmployeeExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }
        //    return NoContent();
        //}

        //// POST: api/Employee
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        //{
        //  if (_db.Employees == null)
        //  {
        //      return Problem("Entity set 'LicenseContext.Employees'  is null.");
        //  }
        //    _db.Employees.Add(employee);
        //    await _db.SaveChangesAsync();
        //    return CreatedAtAction("GetEmployee", new { id = employee.EmployeeId }, employee);
        //}

        //// DELETE: api/Employee/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteEmployee(int id)
        //{
        //    if (_db.Employees == null)
        //    {
        //        return NotFound();
        //    }
        //    var employee = await _db.Employees.FindAsync(id);
        //    if (employee == null)
        //    {
        //        return NotFound();
        //    }
        //    _db.Employees.Remove(employee);
        //    await _db.SaveChangesAsync();
        //    return NoContent();
        //}
        //private bool EmployeeExists(int id)
        //{
        //    return (_db.Employees?.Any(e => e.EmployeeId == id)).GetValueOrDefault();
        //}
    }
}
