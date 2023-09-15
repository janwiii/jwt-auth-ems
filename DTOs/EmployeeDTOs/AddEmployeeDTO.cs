using System.ComponentModel.DataAnnotations;

namespace VertexEMSBackend.DTOs.EmployeeDTOs
{
    public class AddEmployeeDTO
    {
        public string EmployeeNo { get; set; }
        public string? FirstName { get; set;}
        public string? LastName { get; set;}
        public string? Address { get; set;}
        public string? Position { get; set;}
        [EmailAddress]
        public string? Email { get; set; }
        [Phone]
        public string? PhoneNumber { get; set; }
        public string? Password { get; set; }
        public string? EmployementStatus { get; set; }

    }
}
