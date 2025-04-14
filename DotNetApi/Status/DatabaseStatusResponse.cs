namespace DotNetApi.Status;

public record DatabaseStatusResponse(
    DateTime ApplicationTime,
    DateTime DatabaseTime,
    TimeSpan Difference
);