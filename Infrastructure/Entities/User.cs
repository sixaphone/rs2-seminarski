﻿using Infrastructure.Enums;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities
{
    public class User
    {
        [Key]
        public int id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public Role Role { get; set; }
        public bool Active { get; set; } = true;
    }
}
