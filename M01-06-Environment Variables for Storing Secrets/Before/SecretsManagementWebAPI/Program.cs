using Appy.Configuration.WinRegistry;
using SecretsManagementWebAPI.IEncryption;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddTransient<IEncryption, WindowsDPAPIEncryption>();
}

if (builder.Environment.IsProduction())
{
    //Select whats appropriate and secure for your production envrionment!
    builder.Services.AddTransient<IEncryption, ANCDataProtectionEncryption>();
    builder.Configuration.AddRegistrySection(() => Microsoft.Win32.Registry.CurrentUser, "Software\\MyCompany\\MyApp1");
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

ListConfigurationProviders(app);

app.Run();

void ListConfigurationProviders(WebApplication app)
{
    var configurationRoot = (IConfigurationRoot)app.Services.GetRequiredService<IConfiguration>();
    string providers = string.Empty;
    foreach (var provider in configurationRoot.Providers.ToList())
    {
        providers += provider.ToString() + "\n";
    }
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine(providers);
    Console.ResetColor();
}