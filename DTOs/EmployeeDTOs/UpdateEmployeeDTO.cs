using System.ComponentModel.DataAnnotations;

namespace VertexEMSBackend.DTOs.EmployeeDTOs
{
    public class UpdateEmployeeDTO
    {
        [Required(ErrorMessage = "First Name is required")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        public string? LastName { get; set; }
        
        [Required(ErrorMessage = "Address is required")]
        public string? Address { get; set; }
        
        [Required(ErrorMessage = "Employee Position is required")]
        public string? Position { get; set; }
        
        [Required(ErrorMessage = "Employement Status is required")]
        public string? EmployementStatus { get; set; }
        
        [Required (ErrorMessage ="Email Address is required")]
        [EmailAddress]
        public string? Email { get; set; }
        
        [Required(ErrorMessage ="Phone numebr is required")]
        [Phone]
        public string? PhoneNumber { get; set; }
    }
}
