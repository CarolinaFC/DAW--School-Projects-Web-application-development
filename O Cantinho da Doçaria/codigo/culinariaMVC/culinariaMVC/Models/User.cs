using System;
using System.Collections.Generic;

#nullable disable

namespace culinariaMVC.Models
{
    public partial class User
    {
        public User()
        {
            Admin_pasteleiros = new HashSet<Admin_pasteleiro>();
            Leitors = new HashSet<Leitor>();
        }

        public int Id_user { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string tipo_user { get; set; }

        public virtual ICollection<Admin_pasteleiro> Admin_pasteleiros { get; set; }
        public virtual ICollection<Leitor> Leitors { get; set; }
    }
}
