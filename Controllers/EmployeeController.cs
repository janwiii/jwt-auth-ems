using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VertexEMSBackend.Context;
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
        private Account _account;
        private Cloudinary _cloudinary;
        private readonly UserManager<Employee> _userManager;
        private readonly IEmployeeService _employeeService;
        
        private readonly ApplicationDbContext _dbContext;

        public EmployeeController(IEmployeeService employeeService, UserManager<Employee> userManager, ApplicationDbContext dbContext) 
        { 
            _userManager = userManager;
            _employeeService = employeeService;
            _account = new Account { Cloud = "dnow8alub", ApiKey = "719549538397893", ApiSecret = "d793a2e110af388f063b53b117e555" };
            _cloudinary =new Cloudinary(_account) ;
            _dbContext = dbContext;

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
        public async Task<IActionResult> UpdateEmployee(string Id, UpdateEmployeeDTO data)
        {
            if (await _employeeService.UpdateEmployee(Id, data))
            {
                return Ok("Successfully updated.");
            }
            return BadRequest("Something went wrong.");
        }

        [HttpPost("upload-employee-image")]
        public async Task<IActionResult> UploadEmployeeImage(string Id, IFormFile file)
        {
            if (file == null)
            {
                throw new Exception("Profile Image not found");
            }
            if(file.ContentType!= "image/png" && file.ContentType != "image/jpeg")
            {
                return BadRequest("Invalid file type");
            }
            if(await _employeeService.UploadEmployeeImage(Id, file))
            {
                return Ok("Image successfully added");
            }
            return BadRequest("Something went wrong");
        }

       
    }
}
