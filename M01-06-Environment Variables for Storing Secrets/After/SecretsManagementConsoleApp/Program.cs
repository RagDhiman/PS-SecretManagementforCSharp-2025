
using Microsoft.Extensions.Configuration;
using SecretsManagementConsoleApp.IEncryption;

var configuration = new ConfigurationBuilder()
    .AddEnvironmentVariables(prefix: "MyApp1__")
    .Build();

ListConfigurationProviders(configuration);

IEncryption encryption = new WindowsDPAPIEncryption();

string? fileSharePath = encryption.Decrypt(configuration["FileSharePath"]);
string? mainDatabaseConString = encryption.Decrypt(configuration["ConnectionStrings:MainDatabase"]);
string? cacheDatabaseConString = encryption.Decrypt(configuration["ConnectionStrings:CacheDatabase"]);

Console.WriteLine($"fileSharePath: {fileSharePath}");
Console.WriteLine($"MainDatabase: {mainDatabaseConString}");
Console.WriteLine($"CacheDatabase: {cacheDatabaseConString}");



Console.ReadKey();




void ListConfigurationProviders(IConfigurationRoot configuration)
{
    string providers = string.Empty;
    foreach (var provider in configuration.Providers.ToList())
    {
        providers += provider.ToString() + "\n";
    }
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine(providers);
    Console.ResetColor();
}