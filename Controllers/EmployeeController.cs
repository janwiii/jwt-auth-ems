using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VertexEMSBackend.DTOs.EmployeeDTOs;
using VertexEMSBackend.Interfaces;
using VertexEMSBackend.Models;
using VertexEMSBackend.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VertexEMSBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly UserManager<Employee> _userManager;
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService, UserManager<Employee> userManager) 
        { 
            _userManager = userManager;
            _employeeService = employeeService;

        }
        // GET: api/<EmployeeController>
        [HttpGet("get-all-employees")]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _employeeService.GetAll();

            if (employees != null)
            {
                return Ok(employees);
            }
            else
            {
                return BadRequest("Something went wrong");
            }
        }

        // GET api/<EmployeeController>/5
        [HttpGet("get-employee")]
        public async Task<IActionResult> GetEmployeeById(string id)
        {
            var employee = await _userManager.FindByIdAsync(id);

            if (employee !=null)
            {
                return Ok(employee);
            }
            else
            {
                return NotFound("Employee Not Found");
            }            
        }

        // POST api/<EmployeeController>
        [HttpPost("add-employee")]        
        public async Task<IActionResult> AddEmployee(AddEmployeeDTO data)
        {
            var userExists = await _userManager.FindByEmailAsync(data.Email);
            if (userExists!=null) 
            {
                return BadRequest("User Already Exists");
            }

            if (await _employeeService.AddEmployee(data))
            {
                return Ok("User Successfully Added.");
            }
            return BadRequest("Failed to Add User");
        }

        // PUT api/<EmployeeController>/5
        [HttpPut("update-employee")]
        public async Task<IActionResult> UpdateEmployee(string employeeId, UpdateEmployeeDTO data)
        {
            if (await _employeeService.UpdateEmployee(employeeId, data))
            {
                return Ok("Successfully updated.");
            }
            return BadRequest("Something went wrong.");
        }

       
    }
}
