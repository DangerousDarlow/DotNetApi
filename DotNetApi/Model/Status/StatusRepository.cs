using System.Data;
using Dapper;

namespace DotNetApi.Model.Status;

public class StatusRepository(IDbConnection dbConnection)
{
    public Task<DateTime> GetNow() => dbConnection.ExecuteScalarAsync<DateTime>("SELECT now()");
}