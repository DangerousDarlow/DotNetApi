namespace DotNetApi.Model;

public record DatabaseConnectionConfiguration(
    string Host,
    int Port,
    string Username,
    string Password,
    string Database
);