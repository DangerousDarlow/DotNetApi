using Microsoft.AspNetCore.Mvc;

namespace DotNetApi.User;

[ApiController]
[Route("[controller]")]
public class UserController(UserRepository repository) : ControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<User>> GetUsers() => await repository.GetUsers();

    [HttpPost]
    public async Task<IActionResult> AddUser(User user)
    {
        try
        {
            await repository.AddUser(user);
            return Ok();
        }
        catch (ConflictException)
        {
            return Conflict(new { error = "UserAlreadyExists", message = "The user you are trying to create already exists" });
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task DeleteUser(Guid id) => await repository.DeleteUser(id.ToUserId());
}