using System;
using System.Collections.Generic;

namespace Blogifier.Shared
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }


        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }

}
