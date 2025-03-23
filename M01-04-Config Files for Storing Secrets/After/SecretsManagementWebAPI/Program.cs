using SecretsManagementWebAPI.IEncryption;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddTransient<IEncryption, WindowsDPAPIEncryption>();

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