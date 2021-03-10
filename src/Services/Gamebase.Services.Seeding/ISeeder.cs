namespace Gamebase.Services.Seeding
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ISeeder
    {
        public Task SeedGames(ICollection<int> ids);
    }
}
