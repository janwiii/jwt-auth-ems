using VertexEMSBackend.DTOs.EmployeeDTOs;

namespace VertexEMSBackend.Interfaces
{
    public interface IAuthService
    {
        public Task<string> Login(LoginDTO data);
        Task<string?> GenerateTokenString(string userName);
        public Task<bool> ChangePassword(string id, ChangePasswordDTO data);
        public Task<bool> ForgotPassword(string email);
    }
}
