using CapitalPlacementTask.API.Data;
using CapitalPlacementTask.API.Data.Repository.Implementation;
using CapitalPlacementTask.API.Data.Repository.Interface;
using CapitalPlacementTask.API.Services.Implementation;
using CapitalPlacementTask.API.Services.Interface;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CosmosDbContext>(options =>
    options.UseCosmos(
        builder.Configuration.GetConnectionString("CosmosDb"),
        databaseName: "CapitalPlacementDb"
    )
);

builder.Services.AddScoped<IProgramRepository, ProgramRepository>();
builder.Services.AddScoped<IProgramService, ProgramService>();
builder.Services.AddScoped<ICandidateRepository, CandidateRepository>();
builder.Services.AddScoped<ICandidateService, CandidateService>();
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddSingleton((serviceProvider) =>
{
    var cosmosDbEndpoint = "https://localhost:8081";
    var cosmosDbKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";

    var cosmosClient = new CosmosClient(cosmosDbEndpoint, cosmosDbKey);
    return cosmosClient;
});

// Create the database and container on application startup
var host = builder.Build();
using (var scope = host.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var cosmosClient = services.GetRequiredService<CosmosClient>();
        var databaseId = "CapitalPlacementDb"; // Same as databaseName used in DbContext options

        var database = await cosmosClient.CreateDatabaseIfNotExistsAsync(databaseId);
        await database.Database.CreateContainerIfNotExistsAsync("Programs", "/Title");
        await database.Database.CreateContainerIfNotExistsAsync("Questions", "/ProgramInfoId");
        await database.Database.CreateContainerIfNotExistsAsync("Employers", "/ProgramInfoId");
        await database.Database.CreateContainerIfNotExistsAsync("Candidates", "/ProgramInfoId");

        Console.WriteLine("Database and containers created successfully.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred while creating the database and containers: {ex.Message}");
    }
}

var app = host;

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

Seeder.PrepPopulation(app);

app.Run();