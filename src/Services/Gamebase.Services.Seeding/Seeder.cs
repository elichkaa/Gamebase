namespace Gamebase.Services.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using Data;
    using Dtos;
    using Gamebase.Scraping;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using RestSharp;
    using RestSharp.Serializers.NewtonsoftJson;


    public class Seeder : ISeeder
    {
        private readonly RestClient client;
        private readonly ConfigSettings configuration;
        private readonly GamebaseDbContext context;

        public Seeder(ConfigSettings configuration, GamebaseDbContext context)
        {
            this.context = context;
            this.configuration = configuration;
            client = new RestClient("https://api.igdb.com/v4/");
            client.UseNewtonsoftJson();
        }

        public async Task SeedGames(ICollection<int> gameIds)
        {
            RestRequest request = CreateRequest("games", $"fields *;where id=({string.Join(",", gameIds)}); limit 500;");
            IRestResponse<List<GameDto>> response = await client.ExecuteAsync<List<GameDto>>(request);
            foreach (var gameDto in response.Data)
            {
                if (gameDto == null || context.Games.Any(x => x.Id == gameDto.Id)) continue;
                var game = new Game
                {
                    Id = gameDto.Id,
                    TotalRating = gameDto.TotalRating,
                    Bundles = GetEntitiesOfSameTypeAsString(gameDto.Bundles),
                    Category = gameDto.Category,
                    CollectionId = (gameDto.CollectionId != 0 && gameDto.CollectionId != null) ? gameDto.CollectionId : (int?)null,
                    Collection = await GetEntity<Collection>(gameDto.CollectionId, "collections"),
                    CoverId = (gameDto.CoverId != 0 && gameDto.CoverId != null) ? gameDto.CoverId : (int?)null,
                    Cover = await GetEntity<Cover>(gameDto.CoverId, "covers"),
                    Dlcs = GetEntitiesOfSameTypeAsString(gameDto.DLCs),
                    Expansions = GetEntitiesOfSameTypeAsString(gameDto.Expansions),
                    FirstReleaseDate = ConvertFromUnixToUtc(gameDto.FirstReleaseDate),
                    GameEngines = await GetManyToMany<GamesGameEngines, GameEngine>(gameDto.GameEngines, "game_engines",
                        nameof(GameEngine), gameDto.Id, "GameId"),
                    GameModes = await GetManyToMany<GamesGameModes, GameMode>(gameDto.GameModes, "game_modes",
                        nameof(GameMode), gameDto.Id, "GameId"),
                    Genres = await GetManyToMany<GamesGenres, Genre>(gameDto.Genres, "genres", nameof(Genre), gameDto.Id, "GameId"),
                    Developers = await GetManyToMany<GamesDevelopers, Developer>(await this.GetActualCompanyIds(gameDto.InvolvedCompanies),
                        "companies", nameof(Developer), gameDto.Id, "GameId"),
                    Keywords = await GetManyToMany<GamesKeywords, Keyword>(gameDto.Keywords, "keywords", nameof(Keyword),
                        gameDto.Id, "GameId"),
                    Name = gameDto.Name,
                    Platforms = await GetManyToMany<GamesPlatforms, Platform>(gameDto.Platforms, "platforms",
                        nameof(Platform), gameDto.Id, "GameId"),
                    Screenshots = await GetEntities<Screenshot>(gameDto.Screenshots, "screenshots"),
                    Status = gameDto.Status,
                    Storyline = gameDto.Storyline,
                    Summary = gameDto.Summary,
                    SimilarGames = GetEntitiesOfSameTypeAsString(gameDto.SimilarGames),
                    Url = gameDto.Url,
                    Websites = await GetEntities<Website>(gameDto.Websites, "websites")
                };
                Console.WriteLine("Game success");
                await context.Games.AddAsync(game);
                await context.SaveChangesAsync();
            }
        }

        public async Task SeedCharacters(ICollection<int> ids)
        {
            foreach (var id in ids)
            {
                RestRequest request = CreateRequest("characters", $"fields *;where id={id}; limit 500;");
                IRestResponse<List<Character>> response = await client.ExecuteAsync<List<Character>>(request);
                if (response.Data == null || response.Data.Count == 0)
                {
                    continue;
                }
                var character = response.Data[0];
                if (character == null || context.Characters.Any(x => x.Id == character.Id)) continue;

                await this.SeedGames(character.GameIds);
                character.Games = new List<GamesCharacters>();
                foreach (var gameId in character.GameIds)
                {
                    character.Games.Add(new GamesCharacters
                    {
                        GameId = gameId,
                        Game = await this.context.Games.FirstOrDefaultAsync(x => x.Id == gameId),
                        CharacterId = character.Id,
                    });
                }

                character.Image = await GetEntity<CharacterImage>(character.ImageId, "character_mug_shots");
                if (character.Image != null)
                {
                    character.Image.CharacterId = character.Id;
                }

                Console.WriteLine("Character success");
                await context.Characters.AddAsync(character);
                await context.SaveChangesAsync();
            }
        }

        public DateTime ConvertFromUnixToUtc(long unixDate)
        {
            System.DateTime dtDateTime = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
            dtDateTime = dtDateTime.AddSeconds(unixDate);
            return dtDateTime;
        }

        private string GetEntitiesOfSameTypeAsString(ICollection<int> dtoIds)
        {
            if (dtoIds != null)
                return string.Join(", ", dtoIds);
            return null;
        }

        private async Task<ICollection<int>> GetActualCompanyIds(ICollection<int> dtoIds)
        {
            if (dtoIds == null || dtoIds.Count == 0) return null;

            RestRequest request =
                CreateRequest("involved_companies", $"fields *;where id=({string.Join(",", dtoIds)}); limit 500;");
            IRestResponse<ICollection<InvolvedCompany>> response = await client.ExecuteAsync<ICollection<InvolvedCompany>>(request);

            var actualIds = new List<int>();
            if (response.IsSuccessful && response.Data != null)
            {
                foreach (var id in response.Data.Select(x => x.CompanyId))
                {
                    actualIds.Add(id);
                }
            }

            return actualIds;
        }

        private async Task<T> GetEntity<T>(int? id, string subdomain) where T : MainEntity, new()
        {
            if (id == 0 || id == null) return null;
            RestRequest request = CreateRequest(subdomain, $"fields *;where id={id};");
            IRestResponse<List<T>> response = await client.ExecuteAsync<List<T>>(request);

            bool connectionAlreadyExists = await context.Set<T>().FirstOrDefaultAsync(x => x.Id == id) != null;
            if (connectionAlreadyExists)
            {
                return null;
            }

            if (response.Data.Count != 0 && response.Data != null)
            {
                return response.Data[0];
            }

            return null;

        }

        private async Task<ICollection<T>> GetEntities<T>(ICollection<int> dtoIds, string subdomain) where T : new()
        {
            if (dtoIds == null || dtoIds.Count == 0) return null;

            RestRequest request =
                CreateRequest(subdomain, $"fields *;where id=({string.Join(",", dtoIds)}); limit 500;");
            IRestResponse<ICollection<T>> response = await client.ExecuteAsync<ICollection<T>>(request);

            if (response.IsSuccessful && response.Data != null) return response.Data;

            return null;
        }

        private async Task<ICollection<TManyToManyClass>> GetManyToMany<TManyToManyClass, TEntity>(
            ICollection<int> dtoIds, string subdomain, string entityName, int gameId, string propertyName)
            where TManyToManyClass : new() where TEntity : BaseEntity
        {
            if (dtoIds == null || dtoIds.Count == 0) return null;

            RestRequest request =
                CreateRequest(subdomain, $"fields *;where id=({string.Join(",", dtoIds)}); limit 500;");
            IRestResponse<ICollection<TEntity>> response = await client.ExecuteAsync<ICollection<TEntity>>(request);

            var entities = new List<TManyToManyClass>();
            foreach (TEntity entity in response.Data)
            {
                var entityId = (int)typeof(TEntity).GetProperty("Id")?.GetValue(entity);
                var mtmEntity = new TManyToManyClass();
                typeof(TManyToManyClass).GetProperty(entityName + "Id")?.SetValue(mtmEntity, entityId);
                typeof(TManyToManyClass).GetProperty(propertyName)?.SetValue(mtmEntity, gameId);
                bool connectionAlreadyExists =
                    await context.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == entityId) != null;
                if (!connectionAlreadyExists)
                    typeof(TManyToManyClass).GetProperty(entityName)?.SetValue(mtmEntity, entity);
                entities.Add(mtmEntity);
            }

            return entities;
        }

        private RestRequest CreateRequest(string subdomain, string requestBody = "fields *;")
        {
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;

            var request = new RestRequest(subdomain, Method.POST);
            request.AddHeader("Client-ID", configuration.ClientId);
            request.AddHeader("Authorization", configuration.Authorization);
            request.AddParameter("application/json", requestBody, ParameterType.RequestBody);
            request.AddHeader("content-type", "application/json");

            return request;
        }
    }
}
