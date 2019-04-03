using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using SpacePlanetsClientLib.ClientServices;
using SpacePlanetsDAL.Repositories;
using SpacePlanetsDAL.Services;
using SpLib.DataTransfer.ServerToClient;
using SpLib.Objects;
using System;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {

            //var client = new MongoClient("mongodb://localhost:27017/SpacePlanets");
            //IServiceCollection services = new ServiceCollection();
            //services.AddScoped<IMongoClient>(c => client);
            //services.AddScoped<IGameService, GameService>();
            //var provider = services.BuildServiceProvider();
            //IGameService gameService = provider.GetRequiredService<IGameService>();

            IServiceCollection services = new ServiceCollection();
            services.AddScoped<IFlurlClient>(f => new FlurlClient("https://localhost:5001/"));
            var provider = services.BuildServiceProvider();
            IFlurlClient client = provider.GetRequiredService<IFlurlClient>();

            bool quit = false;
            while (quit == false)
            {
                Console.WriteLine("Enter username:");
                string username = Console.ReadLine();
                Console.WriteLine("Enter password:");
                string password = Console.ReadLine();
                bool tokenOk = client.GetAccessToken(username, password, out AccessToken token, out ErrorFromServer error);
                if (tokenOk)
                {
                    Console.WriteLine("welcome " + username);
                }
                else
                {
                    Console.WriteLine("error from server: " + error.Message + ", ID# " + error.ErrorId);
                }

                Console.WriteLine("Type q then enter to quit, otherwise keep trying to log in.");
                var line = Console.ReadLine();
                if (line.ToLower() == "q")
                {
                    quit = true;
                }
            }
        }
    }
}
