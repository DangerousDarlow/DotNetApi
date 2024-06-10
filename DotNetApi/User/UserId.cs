using System.Data;
using System.Text.Json;
using System.Text.Json.Serialization;
using Dapper;

namespace DotNetApi.User;

[JsonConverter(typeof(UserIdJsonConverter))]
public readonly record struct UserId(Guid Value)
{
    public static implicit operator Guid(UserId userId) => userId.Value;
}

public static class UserIdExtensions
{
    public static UserId ToUserId(this Guid guid) => new(guid);
}

public class UserIdDapperTypeHandler : SqlMapper.ITypeHandler
{
    public void SetValue(IDbDataParameter parameter, object value) => parameter.Value = value.ToString();

    public object? Parse(Type destinationType, object value)
    {
        var str = value.ToString();
        return string.IsNullOrEmpty(str) ? null : new UserId(Guid.Parse(str));
    }

    public static void AddToDapper() => SqlMapper.AddTypeHandler(typeof(UserId), new UserIdDapperTypeHandler());
}

public class UserIdJsonConverter : JsonConverter<UserId>
{
    public override UserId Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => new(reader.GetGuid());

    public override void Write(Utf8JsonWriter writer, UserId value, JsonSerializerOptions options) => writer.WriteStringValue(value.Value);
}