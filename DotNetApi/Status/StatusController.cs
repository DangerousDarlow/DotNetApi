using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Npgsql;

namespace DotNetApi.Status;

[ApiController]
[Route("[controller]")]
public class StatusController(
    StatusRepository repository,
    IOptions<DatabaseConnectionConfiguration> configurationOption,
    ILogger<StatusController> logger) : ControllerBase
{
    [HttpGet]
    public void GetStatus() => Response.StatusCode = 200;

    [HttpGet("database")]
    public async Task<DatabaseStatusResponse?> GetDatabaseStatus()
    {
        try
        {
            Response.StatusCode = 200;
            var applicationTime = DateTime.UtcNow;
            var databaseTime = await repository.GetNow();
            var difference = databaseTime - applicationTime;
            return new DatabaseStatusResponse(applicationTime, databaseTime, difference);
        }
        catch (NpgsqlException e)
        {
            var configuration = configurationOption.Value;
            if (configuration is null)
                throw new NullReferenceException("Null database connection configuration", e);

            logger.LogError(
                "Failed to obtain database status: Host {DatabaseHost}, Port {DatabasePort}, Username {DatabaseUsername}, Database {DatabaseName}, Error {ErrorMessage}",
                configuration.Host, configuration.Port, configuration.Username, configuration.Database, e.Message);

            throw;
        }
    }
}