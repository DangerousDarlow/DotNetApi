using System.Net.Sockets;
using DotNetApi.Model.Status;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace DotNetApi.Controllers;

[ApiController]
[Route("[controller]")]
public class StatusController(StatusRepository repository) : ControllerBase
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
            if (e.InnerException is not SocketException)
                throw;

            Response.StatusCode = 408;
            return null;
        }
    }
}