namespace Gamebase.Data
{
    using Gamebase.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class GamebaseDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
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
        public DbSet<Cover> Covers { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Screenshot> Screenshots { get; set; }
        public DbSet<GameEngine> GameEngines { get; set; }
        public DbSet<GamesDevelopers> GamesDevelopers { get; set; }
        public DbSet<GamesGameEngines> GamesEngines { get; set; }
        public DbSet<GamesGameModes> GamesModes { get; set; }
        public DbSet<GamesKeywords> GamesKeywords { get; set; }
        public DbSet<GamesGenres> GameGenres { get; set; }
        public DbSet<Platform> Platforms { get; set; }
        public DbSet<GamesPlatforms> GamePlatforms { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<GamesCharacters> GameCharacters { get; set; }
        public DbSet<CharacterImage> CharacterImages { get; set; }
        public DbSet<Collection> Collections { get; set; }
        public DbSet<GameMode> GameModes { get; set; }
        public DbSet<Keyword> Keywords { get; set; }
        public DbSet<Website> Websites { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Game>()
                .HasOne(a => a.Cover)
               .WithOne(b => b.Game)
               .HasForeignKey<Cover>(b => b.GameId);

            modelBuilder.Entity<Game>()
                .HasIndex(x => x.Id);

            modelBuilder.Entity<Character>()
                .HasOne(a => a.Image)
                .WithOne(b => b.Character)
                .HasForeignKey<CharacterImage>(b => b.CharacterId);

            modelBuilder.Entity<GamesDevelopers>()
                .HasOne(c => c.Developer)
                .WithMany(g => g.Games)
                .HasForeignKey(g => g.DeveloperId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<GamesGenres>()
                .HasKey(bc => new { bc.GameId, bc.GenreId });

            modelBuilder.Entity<GamesPlatforms>()
                .HasKey(bc => new { bc.GameId, bc.PlatformId });

            modelBuilder.Entity<GamesCharacters>()
                .HasKey(bc => new { bc.GameId, bc.CharacterId });

            modelBuilder.Entity<GamesDevelopers>()
                .HasKey(x => new { x.GameId, x.DeveloperId });

            modelBuilder.Entity<GamesGameEngines>()
                .HasKey(x => new { x.GameEngineId, x.GameId });

            modelBuilder.Entity<GamesGameModes>()
                .HasKey(x => new { x.GameModeId, x.GameId });

            modelBuilder.Entity<GamesKeywords>()
                .HasKey(x => new { x.GameId, x.KeywordId });


        }
    }
}
