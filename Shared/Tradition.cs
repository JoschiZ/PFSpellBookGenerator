using System.Text.Json;
using System.Text.Json.Serialization;
using NetEscapades.EnumGenerators;

namespace Shared;

[EnumExtensions]
[JsonConverter(typeof(TraditionConverter))]
public enum Tradition
{
    NoTradition = 0,
    Arcane,
    Divine,
    Occult,
    Primal,
    All
}

internal sealed class TraditionConverter: JsonConverter<Tradition>
{
    public override Tradition Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString() ?? "";
        if (TraditionExtensions.TryParse(value, out var characterClass, true, true))
        {
            return characterClass;
        }

        return Tradition.NoTradition;
    }

    public override void Write(Utf8JsonWriter writer, Tradition value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToStringFast());
    }
}