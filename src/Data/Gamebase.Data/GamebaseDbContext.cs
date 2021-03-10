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
        public DbSet<Franchise> Franchises { get; set; }
        public DbSet<Cover> Covers { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Screenshot> Screenshots { get; set; }
        public DbSet<GameEngine> GameEngines { get; set; }
        public DbSet<GamesGenres> GameGenres { get; set; }
        public DbSet<Platform> Platforms { get; set; }
        public DbSet<GamesPlatforms> GamePlatforms { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<GameCharacter> GameCharacters { get; set; }
        public DbSet<CharacterImage> CharacterImages { get; set; }
        public DbSet<AgeRating> AgeRatings { get; set; }
        public DbSet<ContentDescription> ContentDescriptions { get; set; }
        public DbSet<AlternativeName> AlternativeNames { get; set; }
        public DbSet<Artwork> Artworks { get; set; }
        public DbSet<Collection> Collections { get; set; }
        public DbSet<GameMode> GameModes { get; set; }
        public DbSet<Keyword> Keywords { get; set; }
        public DbSet<PlayerPerspective> PlayerPerspectives { get; set; }
        public DbSet<Theme> Themes { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Website> Websites { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
                modelBuilder.Entity<Game>()
                    .HasOne(a => a.Cover)
                   .WithOne(b => b.Game)                    
                   .HasForeignKey<Cover>(b => b.GameId);

            //modelBuilder.Entity<Developer>()
            //    .HasOne(a => a.DeveloperLogo)
            //   .WithOne(b => b.Developer)
            //   .HasForeignKey<DeveloperLogo>(b => b.DeveloperId);

            modelBuilder.Entity<GamesDevelopers>()
                .HasOne(c => c.Developer)
                .WithMany(g => g.Games)
                .HasForeignKey(g => g.DeveloperId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<GamesGenres>()
                .HasKey(bc => new { bc.GameId, bc.GenreId });

            modelBuilder.Entity<GamesPlatforms>()
                .HasKey(bc => new { bc.GameId, bc.PlatformId });

            modelBuilder.Entity<GameCharacter>()
                .HasKey(bc => new { bc.GameId, bc.CharacterId });

            modelBuilder.Entity<GamesDevelopers>()
                .HasKey(x => new { x.GameId, x.DeveloperId });

            modelBuilder.Entity<GamesGameEngines>()
                .HasKey(x => new { x.GameEngineId, x.GameId });

            modelBuilder.Entity<GamesGameModes>()
                .HasKey(x => new { x.GameModeId, x.GameId });

            modelBuilder.Entity<GamesKeywords>()
                .HasKey(x => new { x.GameId, x.KeywordId });

            modelBuilder.Entity<GamesPlayerPerspectives>()
                .HasKey(x => new { x.GameId, x.PlayerPerspectiveId });

            modelBuilder.Entity<GamesThemes>()
                .HasKey(x => new { x.GameId, x.ThemeId });

        }
    }
}
