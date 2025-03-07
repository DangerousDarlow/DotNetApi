using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace DotNetApi.IntegrationTests;

public class UserTests : IntegrationTest
{
    [Test]
    public async Task Add_then_get_then_delete_user()
    {
        var userId = Guid.NewGuid();

        var addResponse = await Client.PostAsJsonAsync("/user", new { Id = userId, Name = "Test" });
        Assert.That(addResponse.IsSuccessStatusCode, Is.True);

        var getResponse = await Client.GetAsync("/user");
        Assert.That(getResponse.IsSuccessStatusCode, Is.True);

        var getResponseContent = await getResponse.Content.ReadAsStringAsync();
        var users = JsonSerializer.Deserialize<IEnumerable<User.User>>(getResponseContent, JsonSerializerOptions.Web);

        var newUser = users?.First(x => x.Id == userId);
        Assert.That(newUser, Is.Not.Null);
        Assert.That(newUser.Name, Is.EqualTo("Test"));

        var deleteResponse = await Client.DeleteAsync($"/user/{userId}");
        Assert.That(deleteResponse.IsSuccessStatusCode, Is.True);

        getResponse = await Client.GetAsync("/user");
        Assert.That(getResponse.IsSuccessStatusCode, Is.True);

        getResponseContent = await getResponse.Content.ReadAsStringAsync();
        users = JsonSerializer.Deserialize<IEnumerable<User.User>>(getResponseContent, JsonSerializerOptions.Web);
        Assert.That(users?.Any(x => x.Id == userId), Is.False);
    }

    [Test]
    public async Task Add_same_user_twice_returns_conflict()
    {
        var userId = Guid.NewGuid();

        var addResponse = await Client.PostAsJsonAsync("/user", new { Id = userId, Name = "Test" });
        Assert.That(addResponse.IsSuccessStatusCode, Is.True);

        addResponse = await Client.PostAsJsonAsync("/user", new { Id = userId, Name = "Test" });
        Assert.That(addResponse.StatusCode, Is.EqualTo(HttpStatusCode.Conflict));

        var deleteResponse = await Client.DeleteAsync($"/user/{userId}");
        Assert.That(deleteResponse.IsSuccessStatusCode, Is.True);
    }
}