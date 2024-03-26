using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;
using NetEscapades.EnumGenerators;

namespace Shared;

[EnumExtensions]
[JsonConverter(typeof(PatronThemeConverter))]
public enum PatronTheme
{
    [Display(Name = "Baba Yaga")]
    BabaYaga,
    
    [Display(Name = "Faith's Flamekeeper")]
    FaithsFlamekeeper,
    
    [Display(Name = "Mosquito Witch")]
    MosquitoWitch,
    
    Pacts,
    
    [Display(Name = "Silence In Snow")]
    SilenceInSnow,
    
    [Display(Name = "Spinner of Fates")]
    SpinnerOfThreads,
    
    [Display(Name = "Starless Shadow")]
    StarlessShadow,
    
    [Display(Name = "The Inscribed One")]
    TheInscribedOne,
    
    [Display(Name = "The Resentment")]
    TheResentment,
    
    [Display(Name = "Wilding Steward")]
    WildingSteward
}

public static partial class PatronThemeExtensions
{
    public static Tradition GetTradition(this PatronTheme theme)
    {
        return theme switch
        {
            PatronTheme.BabaYaga => Tradition.Occult,
            PatronTheme.FaithsFlamekeeper => Tradition.Divine,
            PatronTheme.MosquitoWitch => Tradition.Primal,
            PatronTheme.Pacts => Tradition.Occult,
            PatronTheme.SilenceInSnow => Tradition.Primal,
            PatronTheme.SpinnerOfThreads => Tradition.Occult,
            PatronTheme.StarlessShadow => Tradition.Occult,
            PatronTheme.TheInscribedOne => Tradition.Arcane,
            PatronTheme.TheResentment => Tradition.Occult,
            PatronTheme.WildingSteward => Tradition.Primal,
            _ => throw new ArgumentOutOfRangeException(nameof(theme), theme, null)
        };
    }
}

internal sealed class PatronThemeConverter : JsonConverter<PatronTheme>
{
    public override PatronTheme Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString() ?? "";
        if (PatronThemeExtensions.TryParse(value, out var patron, true, true))
        {
            return patron;
        }

        throw new InvalidDataException();
    }

    public override void Write(Utf8JsonWriter writer, PatronTheme value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToStringFast());
    }
}