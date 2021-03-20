namespace Gamebase.Services.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Data;
    using Data.Services;
    using Gamebase.Models;
    using Gamebase.Web.InputModels.Search;
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
            this.settings.SeedCharacters(15).GetAwaiter().GetResult();
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        //we have seeded 15 characters => 2 pages => 3 is an invalid parameter
        [InlineData(3)]
        public void InvalidPageNumberOnAllPageShouldReturnNull(int page)
        {
            
            var characters = this.charactersService.GetAll(page);
            Assert.Null(characters);
        }

        [Fact]
        public void CheckIfSeedCharactersAddsGamesToInMemoryDb()
        {

            var charactersCount = this.context.Characters.Count();

            Assert.Equal(15, charactersCount);
        }
        [Fact]
        public void GetAllRetunsAllCharacters()
        {
            var charactersOnFirstPage=this.charactersService.GetAll(1);
            Assert.Equal(9, charactersOnFirstPage.Count);
            var charactersOnSecondPage = this.charactersService.GetAll(2);
            var counter = 1;
            foreach(var character in charactersOnFirstPage) {
                Assert.Equal($"Character{counter}", charactersOnFirstPage.ToList()[counter-1].Name);
                counter++;
            }
            foreach (var character in charactersOnSecondPage)
            {
                Assert.Equal($"Character{counter}", charactersOnSecondPage.ToList()[counter - 10].Name);
                counter++;
            }

        }
        [Fact]
        public void GetCharacterByNameReturnsCorrectInformation()
        {
            this.context.Games.Add(new Game
            {
                Id = 1,
                Name = "Garfield Kart"
            });
            this.context.Games.Add(new Game
            {
                Id = 2,
                Name = "Garfield Kart: Redemption"
            });
            this.context.SaveChanges();
            this.context.Characters.Add(new Character
            {
                Id = 16,
                Name = "Garfield",
                Games = new List<GamesCharacters>()
                {
                    new GamesCharacters()
                    {
                        GameId = 1,
                        CharacterId = 16
                    }, new GamesCharacters()
                    {
                        GameId = 2,
                        CharacterId = 16
                    }
                }
            });
            this.context.Characters.Add(new Character
            {
                Id = 17,
                Name = "The dog from Garfield",
                Games = new List<GamesCharacters>()
                {
                    new GamesCharacters()
                    {
                        GameId = 1,
                        CharacterId = 17
                    }, new GamesCharacters()
                    {
                        GameId = 2,
                        CharacterId = 17
                    }
                }
            });
            this.context.SaveChanges();
            var characterInput = new SearchCharacterInputModel
            {
                Name = "Garfield"
            };
            var characters = this.charactersService.GetCharacterByName(characterInput);
            Assert.Equal("Garfield", characters.ToList()[0].Name);
            Assert.Equal("Garfield Kart", characters.ToList()[0].Games.ToList()[0].Name);
            Assert.Equal("Garfield Kart: Redemption", characters.ToList()[0].Games.ToList()[1].Name);
            Assert.Equal("The dog from Garfield", characters.ToList()[1].Name);
            Assert.Equal("Garfield Kart", characters.ToList()[1].Games.ToList()[0].Name);
            Assert.Equal("Garfield Kart: Redemption", characters.ToList()[1].Games.ToList()[1].Name);
        }
    }
}
