using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace console
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("BEERONBEARD_ENVIRONMENT")}.json", optional: true)
        .Build();
      
      Console.WriteLine("CustomConfigurationSection values:");
      Console.WriteLine($"\tConfigOne: {configuration["CustomConfigurationSection:ConfigOne"]}");
      Console.WriteLine($"\tConfigTwo: {configuration["CustomConfigurationSection:ConfigTwo"]}");
    }
  }
}
