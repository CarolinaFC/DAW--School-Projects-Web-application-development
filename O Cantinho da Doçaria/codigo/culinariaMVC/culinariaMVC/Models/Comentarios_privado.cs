using System;
using System.Collections.Generic;

#nullable disable

namespace culinariaMVC.Models
{
    public partial class Comentarios_privado
    {
        public int Id_comentariosPrivados { get; set; }
        public string descricao { get; set; }
        public int id_minhasReceitas { get; set; }

        public virtual MinhasReceita id_minhasReceitasNavigation { get; set; }
        
    }
}
