using Microsoft.AspNetCore.Mvc;

namespace DotNetApi.User;

[ApiController]
[Route("[controller]")]
public class UserController(UserRepository repository)
{
    [HttpGet]
    public Task<IEnumerable<User>> GetUsers() => repository.GetUsers();
}