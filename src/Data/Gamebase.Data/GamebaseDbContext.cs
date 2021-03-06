namespace Gamebase.Data
{
    using Gamebase.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class GamebaseDbContext : IdentityDbContext
    {
        public GamebaseDbContext(DbContextOptions<GamebaseDbContext> options)
            : base(options)
        {
        }
        public GamebaseDbContext()
        {

        }
        //all dbsets here
        public DbSet<Game> Games { get; set; }
        public DbSet<Developer> Developers { get; set; }
        public DbSet<Franchise> Franchises { get; set; }
        public DbSet<Cover> Covers { get; set; }
        public DbSet<DeveloperLogo> DeveloperLogos { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Screenshot> Screenshots { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
                modelBuilder.Entity<Game>()
                    .HasOne(a => a.Cover)
                   .WithOne(b => b.Game)                    
                   .HasForeignKey<Cover>(b => b.GameId);

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Developer>()
                .HasOne(a => a.DeveloperLogo)
               .WithOne(b => b.Developer)
               .HasForeignKey<DeveloperLogo>(b => b.DeveloperId);

        }
    }
}
