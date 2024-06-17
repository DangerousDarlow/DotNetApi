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
    public void SetValue(IDbDataParameter parameter, object value) => parameter.Value = ((UserId) value).Value;

    public object Parse(Type destinationType, object value) => new UserId((Guid) value);

    public static void AddToDapper() => SqlMapper.AddTypeHandler(typeof(UserId), new UserIdDapperTypeHandler());
}

public class UserIdJsonConverter : JsonConverter<UserId>
{
    public override UserId Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => new(reader.GetGuid());

    public override void Write(Utf8JsonWriter writer, UserId value, JsonSerializerOptions options) => writer.WriteStringValue(value.Value);
}