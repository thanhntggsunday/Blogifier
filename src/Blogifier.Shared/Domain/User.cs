using System.Collections.Generic;

namespace Blogifier.Shared
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }

}
