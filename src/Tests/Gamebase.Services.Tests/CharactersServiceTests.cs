namespace Gamebase.Services.Tests
{
    using System.Linq;
    using System.Threading.Tasks;
    using Data;
    using Data.Services;
    using Settings;
    using Xunit;

    public class CharactersServiceTests
    {
        private readonly GamebaseDbContext context;
        private readonly CharacterService charactersService;
        private readonly TestsSettings settings = new TestsSettings();

        public CharactersServiceTests()
        {
            this.context = settings.InitializeDatabase().GetAwaiter().GetResult();
            this.charactersService = new CharacterService(context);
        }

        [Fact]
        public async Task CheckIfSeedCharactersAddsGamesToInMemoryDb()
        {
            await this.settings.SeedCharacters(10);

            var charactersCount = this.context.Characters.Count();

            Assert.Equal(10, charactersCount);
        }
    }
}
