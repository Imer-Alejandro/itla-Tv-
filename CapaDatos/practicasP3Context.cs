using Microsoft.EntityFrameworkCore;

namespace CapaDatos
{
    public class practicasP3Context : DbContext
    {
        public DbSet<Serie> Series { get; set; } 
        public DbSet<Productora> Productoras { get; set; } 
        public DbSet<Genero> Generos { get; set; }

        public practicasP3Context(DbContextOptions<practicasP3Context> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurar la relación entre Serie y Genero
            modelBuilder.Entity<Serie>()
                .HasOne(s => s.Genero)
                .WithMany(g => g.Series)
                .HasForeignKey(s => s.GeneroId)
                .OnDelete(DeleteBehavior.Restrict); 

            // Configurar la relación entre Serie y Productora
            modelBuilder.Entity<Serie>()
                .HasOne(s => s.Productora)
                .WithMany(p => p.Series)
                .HasForeignKey(s => s.ProductoraId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
