using GFT_Podcasts.Models;
using Microsoft.EntityFrameworkCore;

namespace GFT_Podcasts.Database {
    public class ApplicationDbContext : DbContext {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Categoria> Categorias { get; set; }

        public DbSet<Podcast> Podcasts { get; set; }

        public DbSet<Episodio> Episodios { get; set; }
        
    }
}