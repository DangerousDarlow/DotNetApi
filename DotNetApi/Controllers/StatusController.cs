using DotNetApi.Model.Status;
using Microsoft.AspNetCore.Mvc;

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
        Response.StatusCode = 200;
        var applicationTime = DateTime.UtcNow;
        var databaseTime = await repository.GetNow();
        var difference = databaseTime - applicationTime;
        return new DatabaseStatusResponse(applicationTime, databaseTime, difference);
    }
}