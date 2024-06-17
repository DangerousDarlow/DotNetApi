using System.Data;
using Dapper;
using Npgsql;

namespace DotNetApi.User;

public class UserRepository(IDbConnection dbConnection)
{
    public Task<IEnumerable<User>> GetUsers() => dbConnection.QueryAsync<User>("SELECT id, name FROM users");

    public async Task<int> AddUser(User user)
    {
        try
        {
            return await dbConnection.ExecuteAsync("INSERT INTO users (id, name) VALUES (@id, @name)", user);
        }
        catch (NpgsqlException e)
        {
            if (e.SqlState == PostgresErrorCodes.UniqueViolation)
                throw new ConflictException("User already exists", e);

            throw;
        }
    }

    public Task<int> DeleteUser(UserId id) => dbConnection.ExecuteAsync("DELETE FROM users WHERE id = @value", id);
}

public class ConflictException(string message, Exception e) : Exception(message, e);