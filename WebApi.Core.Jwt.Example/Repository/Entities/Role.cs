using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Core.Jwt.Example.Repository.Entities
{
    public class Role : BaseEntity
    {
        public Role()
        {
            UserRole = new HashSet<UserRole>();
        }

        public int IdRole { get; set; }
        public string Name { get; set; }

        public virtual ICollection<UserRole> UserRole { get; set; }
    }
}
