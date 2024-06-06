namespace DotNetApi.Model;

public class DatabaseConnectionConfiguration
{
    public DatabaseConnectionConfiguration()
    {
    }

    public DatabaseConnectionConfiguration(string host, int port, string username, string password, string database)
    {
        Host = host;
        Port = port;
        Username = username;
        Password = password;
        Database = database;
    }

    public string Host { get; init; } = "localhost";
    public int Port { get; init; } = 5432;
    public string Username { get; init; } = "postgres";
    public string Password { get; init; } = "password";
    public string Database { get; init; } = "dotnetapi";
}