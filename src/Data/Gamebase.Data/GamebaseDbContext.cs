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
        public DbSet<GameEngine> GameEngines { get; set; }
        public DbSet<GameGenre> GameGenres { get; set; }
        public DbSet<Platform> Platforms { get; set; }
        public DbSet<GamePlatform> GamePlatforms { get; set; }
        public DbSet<DLC> DLCs { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<GameCharacter> GameCharacters { get; set; }
        public DbSet<CharacterImage> CharacterImages { get; set; }
        public DbSet<DLCCharacter> DLCCharacters { get; set; }
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

            modelBuilder.Entity<GameGenre>()
        .HasKey(bc => new { bc.GameId, bc.GenreId });
            modelBuilder.Entity<GamePlatform>()
        .HasKey(bc => new { bc.GameId, bc.PlatformId });
            modelBuilder.Entity<GameCharacter>()
        .HasKey(bc => new { bc.GameId, bc.CharacterId });
            modelBuilder.Entity<DLCCharacter>()
        .HasKey(bc => new { bc.DLCId, bc.CharacterId });

        }
    }
}
