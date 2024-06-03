namespace DotNetApi.Model.Status;

public record DatabaseStatusResponse(
    DateTime applicationTime,
    DateTime databaseTime,
    TimeSpan difference
);