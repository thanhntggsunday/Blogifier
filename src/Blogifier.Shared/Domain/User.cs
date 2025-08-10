using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blogifier.Shared
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public string DisplayName { get; set; }
        [StringLength(2000)]
        public string Bio { get; set; }
        [StringLength(400)]
        public string Avatar { get; set; }
        public bool IsAdmin { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }

}
