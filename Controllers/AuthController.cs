using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VertexEMSBackend.DTOs.EmployeeDTOs;
using VertexEMSBackend.Interfaces;
using VertexEMSBackend.Services;

namespace VertexEMSBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /*[HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO data)
        {
            
        }*/
    }
}
