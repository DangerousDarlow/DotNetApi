namespace DotNetApi.IntegrationTests;

public abstract class IntegrationTest
{
    protected HttpClient Client { get; private set; }

    protected Uri TargetUrl { get; } = new(TestContext.Parameters.Get("TargetUrl", "http://localhost:8080"));

    [OneTimeSetUp]
    public void CreateClient()
    {
        Client = new HttpClient { BaseAddress = TargetUrl };
        TestContext.WriteLine($"Created HttpClient with base Url {TargetUrl}");
    }

    [OneTimeTearDown]
    public void DisposeClient() => Client.Dispose();
}