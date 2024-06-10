using Microsoft.AspNetCore.Mvc;

namespace DotNetApi.User;

[ApiController]
[Route("[controller]")]
public class UserController(UserRepository repository)
{
    [HttpGet]
    public Task<IEnumerable<User>> GetUsers() => repository.GetUsers();

    [HttpPost]
    public Task AddUser(User user) => repository.AddUser(user);

    [HttpDelete("{id:guid}")]
    public Task DeleteUser(Guid id) => repository.DeleteUser(id.ToUserId());
}