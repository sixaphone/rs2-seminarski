﻿using Source.net.infrastructure.Enums;

namespace Source.net.infrastructure.Views
{
    public class UserView
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public bool Active{ get; set; }
        public Role RoleId { get; set; }
        public Package Package { get; set; }

        public bool isAdmin()
        {
            return RoleId <= Enums.Role.ADMIN;
        }

        public bool isSA()
        {
            return RoleId == Enums.Role.SUPER_USER;
        }
    }
}
