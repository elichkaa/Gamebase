using Gamebase.Data;
using Gamebase.Data.Services;
using Gamebase.Models;
using Gamebase.Services.Tests.Settings;
using Gamebase.Web.InputModels.Search;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Gamebase.Services.Tests
{
    public class DeveloperServiceTests
    {
        private readonly GamebaseDbContext context;
        private readonly DeveloperService developerService;
        private readonly TestsSettings settings = new TestsSettings();

        public DeveloperServiceTests()
        {
            this.context = settings.InitializeDatabase().GetAwaiter().GetResult();
            this.developerService = new DeveloperService(context);
        }

        [Fact]
        public void GetSingleDeveloperReturnsCorrectInformation()
        {
            this.context.Games.Add(new Game
            {
                Id = 1,
                Name = "Garfield Racing"
            });
            this.context.SaveChanges();
            this.context.Games.Add(new Game
            {
                Id = 2,
                Name = "Garfield Mambo No5"
            });
            this.context.SaveChanges();
            this.context.Developers.Add(new Developer
            {
                Id = 1,
                Name = "Garfield Inc",
                Url = "www.superrealenSajt.com",
                Description = "amazing company creator of amazing games",
                ParentCompanyId = 2,
                PublishedIds = new List<int> {1,2},
                Games = new List<GamesDevelopers>()
                {
                    new GamesDevelopers()
                    {
                        GameId = 1,
                        DeveloperId = 1
                    }, new GamesDevelopers()
                    {
                        GameId = 2,
                        DeveloperId = 1
                    }
                }
            }
            );
            context.SaveChanges();
            var developer = this.developerService.GetSingleDeveloper("Garfield Inc");
            
            Assert.Equal("Garfield Inc", developer.Name);
            Assert.Equal("Garfield Racing", developer.Games.ToList()[0].Name);
            Assert.Equal("www.superrealenSajt.com", developer.Url);
            Assert.Equal("amazing company creator of amazing games",developer.Description);
            Assert.Equal(2, developer.ParentCompanyId);
            Assert.Equal("Garfield Mambo No5", developer.Games.ToList()[1].Name);
            Assert.Equal("1, 2", developer.PublishedGames);
        }
        [Fact]
        public void GetDeveloperByNameReturnsCorrectInformation()
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
            this.context.Developers.Add(new Developer
            {
                Id=1,
                Name="Garfield Inc",
                Games = new List<GamesDevelopers>()
                {
                    new GamesDevelopers()
                    {
                        GameId = 1,
                        DeveloperId = 1
                    }, new GamesDevelopers()
                    {
                        GameId = 2,
                        DeveloperId = 1
                    }
                }
            });
            this.context.Developers.Add(new Developer
            {
                Id=2,
                Name="Garfield Industries",
                Games = new List<GamesDevelopers>()
                {
                    new GamesDevelopers()
                    {
                        GameId = 1,
                        DeveloperId = 2
                    }, new GamesDevelopers()
                    {
                        GameId = 2,
                        DeveloperId = 2
                    }
                }
            });
            this.context.SaveChanges();
            var developerInput = new SearchDeveloperInputModel
            {
                Name = "Garfield"
            };
            var developers = this.developerService.GetDeveloperByName(developerInput);
            Assert.Equal("Garfield Inc",developers.ToList()[0].Name);
            Assert.Equal("Garfield Kart", developers.ToList()[0].GamesByDeveloper.ToList()[0].Name);
            Assert.Equal("Garfield Kart: Redemption", developers.ToList()[0].GamesByDeveloper.ToList()[1].Name);
            Assert.Equal("Garfield Industries", developers.ToList()[1].Name);
            Assert.Equal("Garfield Kart", developers.ToList()[1].GamesByDeveloper.ToList()[0].Name);
            Assert.Equal("Garfield Kart: Redemption", developers.ToList()[1].GamesByDeveloper.ToList()[1].Name);

        }

        [Fact]
        public void GetSingleDeveloperReturnsNullIfNotFound()
        {
            //Arrange
            string devName = "EA";

            //Act + Assert
            Assert.Null(this.developerService.GetSingleDeveloper(devName));
        }
    }
}
