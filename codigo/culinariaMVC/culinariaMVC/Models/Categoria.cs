using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace culinariaMVC.Models
{
    public partial class Categoria
    {
        public Categoria()
        {
            Receita = new HashSet<Receita>();
        }

        public int Id { get; set; }
        public string DescriçãoCat { get; set; }

        public virtual ICollection<Receita> Receita { get; set; }
    }
}
