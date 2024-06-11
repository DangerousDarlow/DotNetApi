using System.Data;
using DotNetApi;
using DotNetApi.Status;
using DotNetApi.User;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", false, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true)
    .AddEnvironmentVariables();

builder.Services.Configure<DatabaseConnectionConfiguration>(builder.Configuration.GetSection("DatabaseConnection"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<StatusRepository>();
builder.Services.AddSingleton<UserRepository>();

builder.Services.AddSingleton<IDbConnection>(serviceProvider =>
{
    var configuration = serviceProvider.GetRequiredService<IConfiguration>();

    var databaseConnectionConfiguration = configuration.GetSection("DatabaseConnection").Get<DatabaseConnectionConfiguration>() ??
                                          throw new InvalidOperationException("Failed to obtain database connection configuration");

    var connectionStringBuilder = new NpgsqlConnectionStringBuilder
    {
        Host = databaseConnectionConfiguration.Host,
        Port = databaseConnectionConfiguration.Port,
        Username = databaseConnectionConfiguration.Username,
        Password = databaseConnectionConfiguration.Password,
        Database = databaseConnectionConfiguration.Database
    };

    var connection = new NpgsqlConnection(connectionStringBuilder.ConnectionString);
    return connection;
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();