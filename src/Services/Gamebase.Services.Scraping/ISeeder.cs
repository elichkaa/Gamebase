namespace Gamebase.Scraping
{
    using System.Threading.Tasks;

    public interface ISeeder
    {
        public Task SeedGames();
    }
}
