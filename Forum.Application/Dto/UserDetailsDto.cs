﻿using Forum.Domain.Models;

namespace Forum.Application.Dto
{
    public class UserDetailsDto
    {
        public long? Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? FullName { get; set; } 
        public Role Role { get; set; } = Role.User;
    }
}
