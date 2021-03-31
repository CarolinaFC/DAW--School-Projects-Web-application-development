using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using culinariaMVC.Models;

#nullable disable

namespace culinariaMVC.Models
{
    public partial class CulinariaDBContext : DbContext
    {
        public CulinariaDBContext()
        {
        }

        public CulinariaDBContext(DbContextOptions<CulinariaDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin_pasteleiro> Admin_pasteleiros { get; set; }
        public virtual DbSet<Comentarios_privado> Comentarios_privados { get; set; }
        public virtual DbSet<Leitor> Leitors { get; set; }
        public virtual DbSet<MinhasReceita> MinhasReceitas { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=culinariaDB_MVC;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Admin_pasteleiro>(entity =>
            {
                entity.HasKey(e => e.Id_admin)
                    .HasName("PK__tmp_ms_x__F727A09EFA2D1D29");

                entity.ToTable("Admin_pasteleiro");

                entity.HasOne(d => d.id_userNavigation)
                    .WithMany(p => p.Admin_pasteleiros)
                    .HasForeignKey(d => d.id_user)
                    .HasConstraintName("FK_Admin_pasteleiro_Users");
            });

            modelBuilder.Entity<Comentarios_privado>(entity =>
            {
                entity.HasKey(e => e.Id_comentariosPrivados)
                    .HasName("PK__tmp_ms_x__3A8EEF2E8C5B9806");

                entity.Property(e => e.descricao)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.HasOne(d => d.id_minhasReceitasNavigation)
                    .WithMany(p => p.Comentarios_Privados)
                    .HasForeignKey(d => d.id_minhasReceitas)
                    .HasConstraintName("FK_MinhasReceitas_Leitor");
            });

            modelBuilder.Entity<Leitor>(entity =>
            {
                entity.HasKey(e => e.Id_leitor)
                    .HasName("PK__tmp_ms_x__34B5E088200AB187");

                entity.ToTable("Leitor");

                entity.HasOne(d => d.id_userNavigation)
                    .WithMany(p => p.Leitors)
                    .HasForeignKey(d => d.id_user)
                    .HasConstraintName("FK_Leitor_Users");
            });

            modelBuilder.Entity<MinhasReceita>(entity =>
            {
                entity.HasKey(e => e.Id_minhaReceita)
                    .HasName("PK__tmp_ms_x__1D6998C358F01CEA");

                entity.HasOne(d => d.id_leitorNavigation)
                    .WithMany(p => p.MinhasReceita)
                    .HasForeignKey(d => d.id_leitor)
                    .HasConstraintName("FK_MinhasReceitas_Leitor");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id_user)
                    .HasName("PK__tmp_ms_x__B607F2481A71C5CC");

                entity.Property(e => e.email)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.password)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.tipo_user)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.username)
                    .HasMaxLength(10)
                    .IsFixedLength(true);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public DbSet<culinariaMVC.Models.Receita> Receita { get; set; }
    }
}
