using System;
using System.Collections.Generic;

#nullable disable

namespace culinariaMVC.Models
{
    public partial class Admin_pasteleiro
    {
        public int Id_admin { get; set; }
        public int id_user { get; set; }

        public virtual User id_userNavigation { get; set; }
    }
}
