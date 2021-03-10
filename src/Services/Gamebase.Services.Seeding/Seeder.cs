﻿namespace Gamebase.Services.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
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
    using Scraping.Dtos;


    public class Seeder : ISeeder
    {
        private readonly RestClient client;
        private readonly ConfigSettings configuration;
        private readonly GamebaseDbContext context;

        public Seeder()
        {
        }

        public Seeder(ConfigSettings configuration, GamebaseDbContext context)
        {
            this.context = context;
            this.configuration = configuration;
            client = new RestClient("https://api.igdb.com/v4/");
            client.UseNewtonsoftJson();
        }

        public async Task SeedGames(int maxId = 10)
        {
            for (int i = 1; i <= maxId; i++)
            {
                RestRequest request = CreateRequest("games", $"fields *; where id = {i};");
                IRestResponse<List<GameDto>> response = await client.ExecuteAsync<List<GameDto>>(request);
                GameDto gameDto = response.Data[0];

                if (gameDto == null || context.Games.Any(x => x.Id == gameDto.Id)) continue;
                var game = new Game
                {
                    Id = gameDto.Id,
                    //AgeRatings = await GetAgeRatings(gameDto.AgeRatings),
                    AggregatedRating = (gameDto.AggregatedRating != 0 && gameDto.AggregatedRating != null)
                        ? gameDto.AggregatedRating
                        : (double?)null,
                    TotalRating = gameDto.TotalRating,
                    //AlternativeNames = await GetEntities<AlternativeName>(gameDto.AlternativeNames, "alternative_names"),
                    //Artworks = await GetEntities<Artwork>(gameDto.Artworks, "artworks"),
                    Bundles = GetEntitiesOfSameTypeAsString(gameDto.Bundles),
                    Category = gameDto.Category,
                    CollectionId = (gameDto.CollectionId != 0 && gameDto.CollectionId != null) ? gameDto.CollectionId : (int?)null,
                    Collection = await GetEntity<Collection>(gameDto.CollectionId, "collections"),
                    CoverId = (gameDto.CoverId != 0 && gameDto.CoverId != null) ? gameDto.CoverId : (int?)null,
                    Cover = await GetEntity<Cover>(gameDto.CoverId, "covers"),
                    CreatedAt = gameDto.CreatedAt,
                    Dlcs = GetEntitiesOfSameTypeAsString(gameDto.DLCs),
                    Expansions = GetEntitiesOfSameTypeAsString(gameDto.Expansions),
                    //ExpandedGames = GetEntitiesOfSameTypeAsString(gameDto.ExpandedGames),
                    FirstReleaseDate = gameDto.FirstReleaseDate,
                    //FranchiseId = gameDto.FranchiseId != 0 ? gameDto.FranchiseId : (int?)null,
                    //MainFranchise = (gameDto.FranchiseId != 0 && gameDto.FranchiseId != null)
                    //    ? await GetEntity<Franchise>(gameDto.FranchiseId, "franchises")
                    //    : await GetEntity<Franchise>(gameDto.Franchises?.FirstOrDefault() ?? 0, "franchises"),
                    GameEngines = await GetManyToMany<GamesGameEngines, GameEngine>(gameDto.GameEngines, "game_engines",
                        nameof(GameEngine), gameDto.Id),
                    GameModes = await GetManyToMany<GamesGameModes, GameMode>(gameDto.GameModes, "game_modes",
                        nameof(GameMode), gameDto.Id),
                    Genres = await GetManyToMany<GamesGenres, Genre>(gameDto.Genres, "genres", nameof(Genre), gameDto.Id),
                    Developers = await GetManyToMany<GamesDevelopers, Developer>(await this.GetActualCompanyIds(gameDto.InvolvedCompanies),
                        "companies", nameof(Developer), gameDto.Id),
                    Keywords = await GetManyToMany<GamesKeywords, Keyword>(gameDto.Keywords, "keywords", nameof(Keyword),
                        gameDto.Id),
                    //MultiplayerModes = await GetEntities<MultiplayerMode>(gameDto.MultiplayerModes, "multiplayer_modes"),
                    Name = gameDto.Name,
                    ParentGameId = (gameDto.ParentGame != 0 && gameDto.ParentGame != null) ? gameDto.ParentGame : (int?)null,
                    //PlayerPerspectives = await GetManyToMany<GamesPlayerPerspectives, PlayerPerspective>(
                    //    gameDto.PlayerPerspectives, "player_perspectives", nameof(PlayerPerspective), gameDto.Id),
                    Platforms = await GetManyToMany<GamesPlatforms, Platform>(gameDto.Platforms, "platforms",
                        nameof(Platform), gameDto.Id),
                    Screenshots = await GetEntities<Screenshot>(gameDto.Screenshots, "screenshots"),
                    Status = gameDto.Status,
                    Storyline = gameDto.Storyline,
                    Summary = gameDto.Summary,
                    SimilarGames = GetEntitiesOfSameTypeAsString(gameDto.SimilarGames),
                    //Themes = await GetManyToMany<GamesThemes, Theme>(gameDto.Themes, "themes", nameof(Theme), gameDto.Id),
                    Url = gameDto.Url,
                    VersionParent = gameDto.VersionParent != 0 ? gameDto.VersionParent : null,
                    VersionTitle = gameDto.VersionTitle,
                    //Videos = await GetEntities<Video>(gameDto.Videos, "game_videos"),
                    Websites = await GetEntities<Website>(gameDto.Websites, "websites")
                };
                Console.WriteLine("success");
                await context.Games.AddAsync(game);
                await context.SaveChangesAsync();
            }
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

            return response.Data?[0];
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
            ICollection<int> dtoIds, string subdomain, string entityName, int gameId)
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
                typeof(TManyToManyClass).GetProperty("GameId")?.SetValue(mtmEntity, gameId);
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