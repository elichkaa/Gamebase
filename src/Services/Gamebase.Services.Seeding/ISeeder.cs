namespace Gamebase.Services.Seeding
{
    using System.Threading.Tasks;

    public interface ISeeder
    {
        public Task SeedGames(int maxId = 100);
    }
}
