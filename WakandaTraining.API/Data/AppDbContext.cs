using Microsoft.EntityFrameworkCore;
using WakandaTraining.API.Models;

namespace WakandaTraining.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(
            DbContextOptions<AppDbContext> options
        ) : base(options)
        {
        }

        public DbSet<Curso> Cursos { get; set; } = null!;
        public DbSet<Usuario> Usuarios { get; set; } = null!;
    }
}