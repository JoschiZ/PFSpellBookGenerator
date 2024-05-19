using System.Text.Json;
using System.Text.Json.Serialization;
using Shared;

namespace SpellDataConverter;

internal sealed class AONPf2Spell
{
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("spell_type")]
        public string SpellType { get; set; }

        [JsonPropertyName("rank")]
        public string Rank { get; set; }

        [JsonPropertyName("heighten")]
        public string Heighten { get; set; }

        [JsonPropertyName("tradition")]
        public string Tradition { get; set; }

        [JsonPropertyName("school")]
        public string School { get; set; }

        [JsonPropertyName("trait")]
        public string Trait { get; set; }

        [JsonPropertyName("actions")]
        public string Actions { get; set; }

        [JsonPropertyName("component")]
        public string Component { get; set; }

        [JsonPropertyName("trigger")]
        public string Trigger { get; set; }

        [JsonPropertyName("target")]
        public string Target { get; set; }

        [JsonPropertyName("range")]
        public string Range { get; set; }

        [JsonPropertyName("area")]
        public string Area { get; set; }

        [JsonPropertyName("duration")]
        public string Duration { get; set; }

        [JsonPropertyName("defense")]
        public string Defense { get; set; }

        [JsonPropertyName("rarity")]
        public string Rarity { get; set; }

        [JsonPropertyName("pfs")]
        public string Pfs { get; set; }

        [JsonPropertyName("bloodline")]
        public string Bloodline { get; set; }

        [JsonPropertyName("deity")]
        public string Deity { get; set; }

        [JsonPropertyName("deity_category")]
        public string DeityCategory { get; set; }

        [JsonPropertyName("domain")]
        public string Domain { get; set; }

        [JsonPropertyName("element")]
        public string Element { get; set; }

        [JsonPropertyName("feat")]
        public string Feat { get; set; }

        [JsonPropertyName("heighten_level")]
        public string HeightenLevel { get; set; }

        [JsonPropertyName("mystery")]
        public string Mystery { get; set; }

        [JsonPropertyName("level")]
        public string Level { get; set; }

        [JsonPropertyName("patron_theme")]
        public string PatronTheme { get; set; }

        [JsonPropertyName("primary_check")]
        public string PrimaryCheck { get; set; }

        [JsonPropertyName("saving_throw")]
        public string SavingThrow { get; set; }

        [JsonPropertyName("source")]
        public string Source { get; set; }

        [JsonPropertyName("source_category")]
        public string SourceCategory { get; set; }

        [JsonPropertyName("source_group")]
        public string SourceGroup { get; set; }

        [JsonPropertyName("spoilers")]
        public string Spoilers { get; set; }

        [JsonPropertyName("summary")]
        public string Summary { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
}