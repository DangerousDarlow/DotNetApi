using System.Data;
using Dapper;

namespace DotNetApi.Status;

public class StatusRepository(IDbConnection dbConnection)
{
    public Task<DateTime> GetNow() => dbConnection.ExecuteScalarAsync<DateTime>("SELECT now()");
}