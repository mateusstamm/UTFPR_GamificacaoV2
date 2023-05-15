using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<CategoriaModel>? Categorias { get; set; }
        public DbSet<MesaModel>? Mesas { get; set; }
        public DbSet<GarconModel>? Garcons { get; set; }
        public DbSet<ProdutoModel>? Produtos { get; set; }
        public DbSet<AtendimentoModel>? Atendimentos { get; set; }
        public DbSet<AtendimentoProdutoModel>? AtendimentoProduto { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("DataSource=gerenRest.db;Cache=Shared");

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<ProdutoModel>().ToTable("Produtos").HasKey(l => l.ProdutoID);
            modelBuilder.Entity<ProdutoModel>().Property(o => o.ProdutoID).ValueGeneratedOnAdd();

            modelBuilder.Entity<CategoriaModel>().ToTable("Categorias").HasKey(i => i.CategoriaID);
            modelBuilder.Entity<CategoriaModel>().Property(j => j.CategoriaID).ValueGeneratedOnAdd();

            modelBuilder.Entity<ProdutoModel>()
                .HasOne(e => e.Categoria)
                .WithMany()
                .HasForeignKey("CategoriaID");
        
            modelBuilder.Entity<GarconModel>().ToTable("Garcons").HasKey(p => p.GarconID);
            modelBuilder.Entity<GarconModel>().Property(k => k.GarconID).ValueGeneratedOnAdd();
            
            modelBuilder.Entity<MesaModel>().ToTable("Mesas").HasKey(n => n.MesaID);
            modelBuilder.Entity<MesaModel>().Property(m => m.MesaID).ValueGeneratedOnAdd();

            modelBuilder.Entity<AtendimentoModel>().ToTable("Atendimentos").HasKey(c => c.AtendimentoID);
            modelBuilder.Entity<AtendimentoModel>().Property(v => v.AtendimentoID).ValueGeneratedOnAdd();

            modelBuilder.Entity<AtendimentoModel>()
                 .HasMany(k => k.ListaProdutos)
                 .WithMany()
                 .UsingEntity<AtendimentoProdutoModel>();

            modelBuilder.Entity<AtendimentoModel>()
                .HasOne(p => p.MesaAtendida)
                .WithMany()
                .HasForeignKey("MesaID");
            
            modelBuilder.Entity<AtendimentoModel>()
                .HasOne(g => g.GarconResponsavel)
                .WithMany()
                .HasForeignKey("GarconID");
            
            modelBuilder.Entity<AtendimentoModel>().Ignore(u => u.ListaQuantidade);
        }
    }
}