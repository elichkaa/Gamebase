namespace Gamebase.Scraper
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;
    using RestSharp;

    public class Seeder : ISeeder
    {
        private readonly IConfiguration configuration;

        public Seeder(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task SeedGames()
        {
            var client = new RestClient("https://api.igdb.com/v4/");
            var gameNames = new List<string>();
            for (int i = 1; i <= 100; i++)
            {
                var request = this.CreateRequest("games", $"fields *; where id = {i};");
                var response = await client.ExecuteAsync<List<Game>>(request);
                Console.WriteLine(response.Data[0].Name);
            }
        }

        public class Game
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        private RestRequest CreateRequest(string subdomain, string requestBody = "fields *;")
        {
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;

            var request = new RestRequest(subdomain, Method.POST);
            request.AddHeader(configuration.GetSection("ConfigSettings:Client-ID").Key,
                configuration.GetSection("ConfigSettings:Client-ID").Value);
            request.AddHeader(configuration.GetSection("ConfigSettings:Authorization").Key,
                configuration.GetSection("ConfigSettings:Authorization").Value);
            request.AddParameter("application/json", requestBody, ParameterType.RequestBody);
            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
            request.AddHeader("content-type", "application/json");

            return request;
        }
    }
}
