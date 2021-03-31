using System;
using System.Collections.Generic;

#nullable disable

namespace culinariaMVC.Models
{
    public partial class MinhasReceita
    {
        public int Id_minhaReceita { get; set; }
        public int id_receitaAPI { get; set; }
        public int? id_leitor { get; set; }

        public virtual Leitor id_leitorNavigation { get; set; }
        public virtual ICollection<Comentarios_privado> Comentarios_Privados { get; set; }
    }
}
