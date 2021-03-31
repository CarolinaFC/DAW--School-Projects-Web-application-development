using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace culinariaAPI.Models
{
    public partial class culinariaApiDBContext : DbContext
    {
        public culinariaApiDBContext()
        {
        }

        public culinariaApiDBContext(DbContextOptions<culinariaApiDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Avaliaco> Avaliacoes { get; set; }
        public virtual DbSet<Categoria> Categorias { get; set; }
        public virtual DbSet<ComentariosPublico> ComentariosPublicos { get; set; }
        public virtual DbSet<Receita> Receitas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=culinariaDB_API;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Avaliaco>(entity =>
            {
                entity.ToTable("avaliacoes");

                entity.Property(e => e.IdReceita).HasColumnName("id_receita");

                entity.Property(e => e.QuantidadeEstrelas).HasColumnName("quantidade_estrelas");

                entity.HasOne(d => d.IdReceitaNavigation)
                    .WithMany(p => p.Avaliacos)
                    .HasForeignKey(d => d.IdReceita)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_avaliacoes_receita");
            });

            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.ToTable("categorias");

                entity.Property(e => e.DescriçãoCat)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("descrição_Cat")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<ComentariosPublico>(entity =>
            {
                entity.ToTable("comentarios_publicos");

                entity.Property(e => e.DescricaoComentariosPub)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("descricao_comentariosPub")
                    .IsFixedLength(true);

                entity.Property(e => e.IdReceita).HasColumnName("id_receita");

                entity.HasOne(d => d.IdReceitaNavigation)
                    .WithMany(p => p.ComentariosPublicos)
                    .HasForeignKey(d => d.IdReceita)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_comentarios_publicos_receitas");
            });

            modelBuilder.Entity<Receita>(entity =>
            {
                entity.ToTable("receitas");

                entity.Property(e => e.CustoRefeicao)
                    .HasMaxLength(10)
                    .HasColumnName("custo_refeicao")
                    .IsFixedLength(true);

                entity.Property(e => e.DataPublicacao)
                    .HasColumnType("date")
                    .HasColumnName("data_publicacao");

                entity.Property(e => e.DescricaoReceita)
                    .HasMaxLength(1000)
                    .HasMaxLength(1000)
                    .HasColumnName("descricao_receita")
                    .IsFixedLength(true);

                entity.Property(e => e.Doses)
                    .HasMaxLength(10)
                    .HasColumnName("doses")
                    .IsFixedLength(true);

                entity.Property(e => e.GrauDificuldade)
                    .HasMaxLength(10)
                    .HasColumnName("grau_dificuldade")
                    .IsFixedLength(true);

                entity.Property(e => e.IdAdmin).HasColumnName("id_admin");

                entity.Property(e => e.IdCategoria).HasColumnName("id_categoria");

                entity.Property(e => e.ImgReceita)
                    .HasMaxLength(255)
                    .HasColumnName("img_receita")
                    .IsFixedLength(true);

                entity.Property(e => e.Ingredientes)
                    .HasMaxLength(1000)
                    .HasColumnName("ingredientes")
                    .IsFixedLength(true);

                entity.Property(e => e.Instrucoes)
                    .HasMaxLength(2000)
                    .HasColumnName("instrucoes")
                    .IsFixedLength(true);

                entity.Property(e => e.NomeReceita)
                    .HasMaxLength(100)
                    .HasColumnName("nome_receita")
                    .IsFixedLength(true);

                entity.Property(e => e.TempoPreparacao)
                    .HasMaxLength(10)
                    .HasColumnName("tempo_preparacao")
                    .IsFixedLength(true);

                entity.HasOne(d => d.IdCategoriaNavigation)
                    .WithMany(p => p.Receita)
                    .HasForeignKey(d => d.IdCategoria)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_receitas_categorias");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
