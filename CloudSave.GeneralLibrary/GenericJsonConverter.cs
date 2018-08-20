using System;

using Newtonsoft.Json;

namespace CloudSave.GeneralLibrary
{
    public static class GenericJsonConverter
    {
        public static JsonConverter Create<TSource>() => new GenericJsonConverter<TSource>();
    }

public class GenericJsonConverter<TSource> : JsonConverter
{
    #region Overrides of JsonConverter

    /// <inheritdoc />
    public override bool CanConvert(Type objectType)
    {
        var type = typeof(TSource);

        return type != objectType && objectType.IsAssignableFrom(type);
    }

    /// <inheritdoc />
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        serializer.Serialize(writer, value, typeof(TSource));
    }

    /// <inheritdoc />
    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        return serializer.Deserialize<TSource>(reader);
    }

    #endregion
}
}
