using SecretsManagementConsoleApp.IEncryption;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

if (builder.Environment.IsProduction())
{
    builder.Services.AddTransient<IEncryption, NOEncryption>();
    builder.Configuration.AddAzureKeyVault(
        new Uri("https://<your-keyvault-name>.vault.azure.net/"),
        new DefaultAzureCredential());
}

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddTransient<IEncryption, WindowsDPAPIEncryption>();
    builder.Configuration.AddXmlFile("test.xml");
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