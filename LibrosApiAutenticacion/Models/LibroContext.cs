using Microsoft.EntityFrameworkCore;

namespace LibrosApiAutenticacion.Models
{
    public class LibroContext : DbContext
    {
        public LibroContext(DbContextOptions<LibroContext> options)
           : base(options) { }
        public DbSet<Libro> Libros { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Libro>().HasData(
                new Libro
                {
                    Id = 1,
                    Titulo = "Cien años de soledad",
                    Autor = "Gabriel García Márquez",
                    AnioPublicacion = 1967
                },
                new Libro
                {
                    Id = 2,
                    Titulo = "Hija de la Tierra",
                    Autor = "Andrea Longarela",
                    AnioPublicacion = 2024
                },
                new Libro
                {
                    Id = 3,
                    Titulo = "El amor en tiempos del cólera",
                    Autor = "Gabriel García Márquez",
                    AnioPublicacion = 1985
                }
                );
        }
    }
}
