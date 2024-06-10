using System.Net.Http.Json;
using System.Text.Json;

namespace DotNetApi.IntegrationTests;

public class UserTests : IntegrationTest
{
    [Test]
    public async Task AddGetDeleteUser()
    {
        var userId = Guid.NewGuid();

        var addResponse = await Client.PostAsJsonAsync("/user", new { Id = userId, Name = "Test" });
        Assert.That(addResponse.IsSuccessStatusCode, Is.True);

        var getResponse = await Client.GetAsync("/user");
        Assert.That(getResponse.IsSuccessStatusCode, Is.True);

        var getResponseContent = await getResponse.Content.ReadAsStringAsync();
        var users = JsonSerializer.Deserialize<IEnumerable<User.User>>(getResponseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        var newUser = users?.First(x => x.Id == userId);
        Assert.That(newUser, Is.Not.Null);
        Assert.That(newUser.Name, Is.EqualTo("Test"));

        var deleteResponse = await Client.DeleteAsync($"/user/{userId}");
        Assert.That(deleteResponse.IsSuccessStatusCode, Is.True);

        getResponse = await Client.GetAsync("/user");
        Assert.That(getResponse.IsSuccessStatusCode, Is.True);

        getResponseContent = await getResponse.Content.ReadAsStringAsync();
        users = JsonSerializer.Deserialize<IEnumerable<User.User>>(getResponseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        Assert.That(users?.Any(x => x.Id == userId), Is.False);
    }
}