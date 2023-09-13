using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VertexEMSBackend.Context;
using VertexEMSBackend.DTOs.EmployeeDTOs;
using VertexEMSBackend.Interfaces;
using VertexEMSBackend.Models;

namespace VertexEMSBackend.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<Employee> _userManager;

        public EmployeeService(ApplicationDbContext dbContext, UserManager<Employee> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }
        public async Task<List<Employee>?> GetAll()
        {
            try
            {
                var employees = await _dbContext.Employees.ToListAsync();
                return employees;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> GetById(string id)
        {
            var currentEmployee = await _userManager.FindByIdAsync(id);
            var employee = await _dbContext.Employees.FindAsync(id);

            if (employee == null || currentEmployee == null)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> AddEmployee(AddEmployeeDTO data)
        {
            
            var iemployee = new Employee()
            {
                EmployeeId = data.EmployeeId,
                FirstName = data.FirstName,
                LastName = data.LastName,
                Email = data.Email,
                UserName = data.Email,
                Address = data.Address,
                Position = data.Position,
                PhoneNumber = data.PhoneNumber,
                EmployementStatus = data.EmployementStatus
            };
            var result = await _userManager.CreateAsync(iemployee, data.Password);
            if (result.Succeeded)
            {
                return true;
            }
            return false;   
        }
        public async Task<bool> UpdateEmployee(string id, UpdateEmployeeDTO data)
        {
            var employee = await _dbContext.Employees.FindAsync(id);

            employee.FirstName = data.FirstName;
            employee.LastName = data.LastName;
            employee.Position = data.Position;
            employee.Address = data.Address;
            employee.Email = data.Email;
            employee.PhoneNumber = data.PhoneNumber;


            _dbContext.Employees.Update(employee);

            var result = await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
