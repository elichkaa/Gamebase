namespace Gamebase.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class GamebaseDbContext : IdentityDbContext
    {
        public GamebaseDbContext(DbContextOptions<GamebaseDbContext> options)
            : base(options)
        {
        }

    }
}
