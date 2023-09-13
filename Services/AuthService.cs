using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VertexEMSBackend.DTOs.EmployeeDTOs;
using VertexEMSBackend.Interfaces;
using VertexEMSBackend.Models;

namespace VertexEMSBackend.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<Employee> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<Employee> _signInManager;
        private readonly IConfiguration _config;

        public AuthService(UserManager<Employee> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, SignInManager<Employee> signInManager) 
        { 
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _config = configuration;
        }

        public async Task<string> Login(LoginDTO data)
        {
            var Employee = await _userManager.FindByEmailAsync(data.Email);
            if (Employee is null)
            {
                return "Invalid User Details!!";
            }

            var result = await _userManager.CheckPasswordAsync(Employee, data.Password);

            if (!result)
            {
                return "Invalid User Credentials!!";
            }

            return "Login Success";
        }

        public async Task<String?> GenerateTokenString(string email)
        {
            var employee = await _userManager.FindByNameAsync(email);
            var employeeRoles = await _userManager.GetRolesAsync(employee);
            var authClaims = new List<Claim>
               {
                    new(ClaimTypes.Email, employee.Email),
                    new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
               };

            foreach (var employeeRole in employeeRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, employeeRole));
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
            var credentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["JWT:ValidIssuer"],
                audience: _config["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(5),
                claims: authClaims,
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<bool> ChangePassword(string id, ChangePasswordDTO data)
        {
            var currentEmployee = await _userManager.FindByIdAsync(id);
            if (currentEmployee == null)
            {
                throw new Exception("Current User not found");
            }
            var result = await _signInManager.CheckPasswordSignInAsync(currentEmployee, data.CurrentPassword, false);
            if (result.Succeeded)
            {
                throw new Exception("Current password is invalid");
            }
            var changePasswordResult = await _userManager.ChangePasswordAsync(currentEmployee, data.CurrentPassword, data.NewPassword);
            if(changePasswordResult.Succeeded)
            {
                return true;
            }
            return false;
        }
    }
}
