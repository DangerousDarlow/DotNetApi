using System.Data;
using Dapper;

namespace DotNetApi.User;

public class UserRepository(IDbConnection dbConnection)
{
    public Task<IEnumerable<User>> GetUsers() => dbConnection.QueryAsync<User>("SELECT Id, Name FROM users");
}