using System;
using System.Collections.Generic;
using System.Text;

namespace WebAPI.Core
{
    class User
    {
        public int Id { get; set; }
        public string  Name { get; set; }
        public string  Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string passwordHash { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
