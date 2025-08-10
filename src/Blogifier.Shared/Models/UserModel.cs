using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogifier.Shared.Models
{
    public class UserModel
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

        public List<Role> Roles { get; set; }
    }
}
