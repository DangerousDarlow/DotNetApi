using System.Data;
using Dapper;

namespace DotNetApi.User;

public class UserRepository(IDbConnection dbConnection)
{
    public Task<IEnumerable<User>> GetUsers() => dbConnection.QueryAsync<User>("SELECT Id, Name FROM users");

    public Task<int> AddUser(User user) => dbConnection.ExecuteAsync("INSERT INTO users (Id, Name) VALUES (@Id, @Name)", new { Id = user.Id.Value, user.Name });

    public Task<int> DeleteUser(UserId id) => dbConnection.ExecuteAsync("DELETE FROM users WHERE Id = @Id", new { Id = id.Value });
}