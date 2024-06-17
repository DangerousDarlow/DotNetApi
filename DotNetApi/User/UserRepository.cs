using System.Data;
using Dapper;

namespace DotNetApi.User;

public class UserRepository(IDbConnection dbConnection)
{
    public Task<IEnumerable<User>> GetUsers() => dbConnection.QueryAsync<User>("SELECT id, name FROM users");

    public Task<int> AddUser(User user) => dbConnection.ExecuteAsync("INSERT INTO users (id, name) VALUES (@id, @name)", user);

    public Task<int> DeleteUser(UserId id) => dbConnection.ExecuteAsync("DELETE FROM users WHERE id = @value", id);
}