﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace VertexEMSBackend.Models
{
    public class Employee : IdentityUser
    {
        
        public string EmployeeId { get; set; }
        public string? FirstName { get; set;}
        public string? LastName { get; set;}
        public string? Address { get; set;}
        public string? Position { get; set;}
        public string? EmployementStatus { get; set;}
    }
}