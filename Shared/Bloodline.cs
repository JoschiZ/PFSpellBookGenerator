using System.Text.Json;
using System.Text.Json.Serialization;
using NetEscapades.EnumGenerators;

namespace Shared;

[EnumExtensions]
[JsonConverter(typeof(BloodlineConverter))]
public enum Bloodline
{
    Aberrant,
    Angelic,
    Demonic,
    Diabolic,
    Draconic,
    Elemental,
    Fey,
    Genie,
    Hag,
    Harrow,
    Imperial,
    Nymph,
    Phoenix,
    Psychopomp,
    Shadow,
    Undead,
    Wyrmblessed,
}

internal sealed class BloodlineConverter : JsonConverter<Bloodline>
{
    public override Bloodline Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString() ?? "";

        if (BloodlineExtensions.TryParse(value, out var bloodline, true, true))
        {
            return bloodline;
        }

        throw new InvalidDataException($"{value} Could not be parsed to a bloodline");
    }

    public override void Write(Utf8JsonWriter writer, Bloodline value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToStringFast());
    }
}


public static partial class BloodlineExtensions
{
    public static Tradition GetTradition(this Bloodline bloodline)
    {
        return bloodline switch
        {
            Bloodline.Aberrant => Tradition.Occult,
            Bloodline.Angelic => Tradition.Divine,
            Bloodline.Demonic => Tradition.Divine,
            Bloodline.Diabolic => Tradition.Divine,
            Bloodline.Draconic => Tradition.Arcane,
            Bloodline.Elemental => Tradition.Primal,
            Bloodline.Fey => Tradition.Primal,
            Bloodline.Genie => Tradition.Arcane,
            Bloodline.Hag => Tradition.Occult,
            Bloodline.Harrow => Tradition.Occult,
            Bloodline.Imperial => Tradition.Arcane,
            Bloodline.Nymph => Tradition.Primal,
            Bloodline.Phoenix => Tradition.Primal,
            Bloodline.Psychopomp => Tradition.Divine,
            Bloodline.Shadow => Tradition.Occult,
            Bloodline.Undead => Tradition.Divine,
            Bloodline.Wyrmblessed => Tradition.Divine,
            _ => throw new ArgumentOutOfRangeException(nameof(bloodline), bloodline, null)
        };
    }
}