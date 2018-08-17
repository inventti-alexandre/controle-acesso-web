using ControleAcesso.Models;
using Microsoft.EntityFrameworkCore; 

namespace ControleAcesso.Data
{
    public class ApplicationDbContext : DbContext
    { 

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Evento> Eventos{ get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<CursoPresenca> CursoPresencas { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<TipoPresenca> TipoPresenca { get; set; }
        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<TipoUsuario> TipoUsuario { get; set; }
        public DbSet<TipoEvento> TipoEvento { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
