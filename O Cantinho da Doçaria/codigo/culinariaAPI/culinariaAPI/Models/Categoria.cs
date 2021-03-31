using System;
using System.Collections.Generic;

#nullable disable

namespace culinariaAPI.Models
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
