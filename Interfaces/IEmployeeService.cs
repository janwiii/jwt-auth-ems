using Microsoft.AspNetCore.Mvc;
using VertexEMSBackend.DTOs.EmployeeDTOs;
using VertexEMSBackend.Models;

namespace VertexEMSBackend.Interfaces
{
    public interface IEmployeeService
    {
        Task<List<Employee>?> GetAll();
        Task<bool> GetById(string id);
        Task<bool> AddEmployee(AddEmployeeDTO data);
        Task<bool> UpdateEmployee(string id, UpdateEmployeeDTO data);

        Task<bool> UploadEmployeeImage(string id, IFormFile file);
    
    }
}
