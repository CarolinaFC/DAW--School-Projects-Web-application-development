using System;
using System.Collections.Generic;

#nullable disable

namespace culinariaAPI.Models
{
    public partial class Avaliaco
    {
        public int Id { get; set; }
        public int QuantidadeEstrelas { get; set; }
        public int IdReceita { get; set; }

        public virtual Receita IdReceitaNavigation { get; set; }
    }
}
