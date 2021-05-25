using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace RobotSenderSample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("appsettings.json");

            var configuration = configurationBuilder.Build();

            var scenario = await File.ReadAllTextAsync("scenario.txt");
            await using var commandClient = new RobotHubClient(configuration.GetValue<string>("RobotAddress"));
            await commandClient.StartAsync();
            await commandClient.Add(scenario);
            Console.ReadLine();
        }
    }
}