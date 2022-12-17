using Microsoft.EntityFrameworkCore;

namespace AnnonceAPI.Models
{
    public class AnnonceContext : DbContext
    {
        public AnnonceContext(DbContextOptions<AnnonceContext> options)
            : base(options)
        {
        }

        public DbSet<Annonce> Annonces { get; set; }
    }
}
