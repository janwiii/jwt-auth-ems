using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace VertexEMSBackend.Models
{
    public class Employee : IdentityUser
    {
        [Required(ErrorMessage ="Employee ID is required")]
        public string EmployeeId { get; set; }
        [Required(ErrorMessage = "First Name is required")]
        public string? FirstName { get; set;}
        [Required(ErrorMessage = "Last Name is required")]
        public string? LastName { get; set;}
        [Required(ErrorMessage = "Address is required")]
        public string? Address { get; set;}
        [Required(ErrorMessage = "Employee Position is required")]
        public string? Position { get; set;}
        [Required(ErrorMessage = "Employement Status is required")]
        public string? EmployementStatus { get; set;}
    }
}
