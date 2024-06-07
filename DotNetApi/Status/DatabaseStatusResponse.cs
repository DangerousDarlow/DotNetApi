namespace DotNetApi.Status;

public record DatabaseStatusResponse(
    DateTime applicationTime,
    DateTime databaseTime,
    TimeSpan difference
);