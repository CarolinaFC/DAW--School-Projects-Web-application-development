using System;
using System.Collections.Generic;

#nullable disable

namespace culinariaMVC.Models
{
    public partial class Leitor
    {
        public Leitor()
        {
            MinhasReceita = new HashSet<MinhasReceita>();
        }

        public int Id_leitor { get; set; }
        public int id_user { get; set; }

        public virtual User id_userNavigation { get; set; }
        public virtual ICollection<MinhasReceita> MinhasReceita { get; set; }
    }
}
