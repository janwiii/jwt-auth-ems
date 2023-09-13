using System.ComponentModel.DataAnnotations;

namespace VertexEMSBackend.DTOs.EmployeeDTOs
{
    public class LoginDTO
    {
        [Required(ErrorMessage ="Email is required")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage ="Password is required")]
        public string Password { get; set; }
    }
}
