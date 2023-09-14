using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
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
        private Account _account;
        private readonly ApplicationDbContext _dbContext;
        private Cloudinary _cloudinary;
        private readonly UserManager<Employee> _userManager;

        public EmployeeService(ApplicationDbContext dbContext, UserManager<Employee> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _account = new Account { Cloud = "dnow8alub", ApiKey = "719549538397893", ApiSecret = "d793a2e110af388f063b53b117e555" };
            _cloudinary = new Cloudinary(_account);
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

        public async Task<bool> UploadEmployeeImage(Guid Id, IFormFile file)
        {
            
            var employee = await _dbContext.Employees.FindAsync(Id);
            if (employee == null)
            {
                return false;
            }
            var stream = new MemoryStream();
            await file.CopyToAsync(stream);
            stream.Position = 0;
            var imageId = Guid.NewGuid();

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, stream),
                PublicId = $"Employees/{imageId}",
                Transformation = new Transformation().FetchFormat("auto")
            };

            _cloudinary.Upload(uploadParams);

            var newProfilePicture = new ProfilePicture
            {
                ImageId = Guid.NewGuid(),
                ProfileIMG = imageId,
                Id = Id
            };

            _dbContext.ProfilePictures.Add(newProfilePicture);
            _dbContext.SaveChanges();
            return true;
        }

       
    }
}
