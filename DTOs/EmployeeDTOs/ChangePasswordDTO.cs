using System.ComponentModel.DataAnnotations;

namespace VertexEMSBackend.DTOs.EmployeeDTOs
{
    public class ChangePasswordDTO
    {
        [Required(ErrorMessage ="Please enter the current password")]
        [DataType(DataType.Password)]
        public string? CurrentPassword { get; set; }
        [Required(ErrorMessage ="Please enter the new password")]
        [DataType(DataType.Password)]
        public string? NewPassword { get; set; }
    }
}
