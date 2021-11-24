using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Core.Jwt.Example.Repository.Entities
{
    public partial class User : BaseEntity
    {
        public User()
        {
            UserRole = new HashSet<UserRole>();
        }

        public int IdUser { get; set; }
        public string Login { get; set; }
        public string Pwd { get; set; }
        public string Name { get; set; }

        public virtual ICollection<UserRole> UserRole { get; set; }

    }
}
