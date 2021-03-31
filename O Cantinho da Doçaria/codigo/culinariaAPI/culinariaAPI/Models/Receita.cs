using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace culinariaAPI.Models
{
    public partial class Receita
    {
        public Receita()
        {
            Avaliacos = new HashSet<Avaliaco>();
            ComentariosPublicos = new HashSet<ComentariosPublico>();
        }

        public int Id { get; set; }
        public string NomeReceita { get; set; }
        public DateTime? DataPublicacao { get; set; }
        public string ImgReceita { get; set; }
        public string DescricaoReceita { get; set; }
        public string GrauDificuldade { get; set; }
        public string CustoRefeicao { get; set; }
        public string TempoPreparacao { get; set; }
        public int Doses { get; set; }
        public string Ingredientes { get; set; }
        public string Instrucoes { get; set; }
        public int IdAdmin { get; set; }
        public int IdCategoria { get; set; }

        public virtual Categoria IdCategoriaNavigation { get; set; }
        public virtual ICollection<Avaliaco> Avaliacos { get; set; }
        public virtual ICollection<ComentariosPublico> ComentariosPublicos { get; set; }
    }
}
