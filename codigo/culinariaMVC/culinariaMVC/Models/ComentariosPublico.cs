using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace culinariaMVC.Models
{
    public partial class ComentariosPublico
    {
        public int Id { get; set; }
        public string DescricaoComentariosPub { get; set; }
        public int IdReceita { get; set; }

        public virtual Receita IdReceitaNavigation { get; set; }
    }
}
