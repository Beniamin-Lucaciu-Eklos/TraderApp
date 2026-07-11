using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraderApp.Domain.Models
{
    public class User : DomainObject
    {
        public string Email { get; set; }

        public string UserName { get; set; }

        public string PasswordHash { get; set; }

        public DateTime DateTimeJoined { get; set; }
    }
}
