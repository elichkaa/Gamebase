﻿namespace Gamebase.Playground
{
    using Microsoft.Extensions.Configuration;
    using Gamebase.Data;
    using Gamebase.Scraping;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Scraper;

    class Program
    {
        static void Main(string[] args)
        {
            IConfiguration settings = new ConfigurationBuilder()
                .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            var services = new ServiceCollection();

            services.AddDbContext<GamebaseDbContext>(options => options.UseSqlServer(settings.GetConnectionString("DefaultConnection")));

            var serviceProvider = services.BuildServiceProvider();
            var appDbContext = serviceProvider.GetService<GamebaseDbContext>();

            var apiConfigSettings = new ConfigSettings
            {
                ClientId = settings.GetSection("ConfigSettings:Client-ID").Value,
                Authorization = settings.GetSection("ConfigSettings:Authorization").Value,
            };
        }
    }
}
