using System.Text.Json;
using Shared;

namespace SpellDataConverter;

internal static class Pathfinder2
{
    private static List<string> _classes = [
        "Alchemist",
        "Barbarian",
        "Fighter",
        "Investigator",
        "Psychic",
        "Ranger",
        "Thaumaturge",
        "Witch",
        "Gunslinger",
        "Inventor",
        "Bard",
        "Champion",
        "Kineticist",
        "Magus",
        "Rogue",
        "Sorcerer",
        "Wizard",
        "Cleric",
        "Druid",
        "Monk",
        "Oracle",
        "Summoner",
        "Swashbuckler"
    ];
    
    public static void Convert()
    {
        using var fs = File.OpenRead("./PF2Spells.json");
        var spells = JsonSerializer.Deserialize<List<Pathfinder2Spell>>(fs, new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        });
        if (spells is null)
        {
            return;
        }

        var id = 0;
        foreach (var spell in spells)
        {
            spell.Id = id;
            id++;
        }
        
        var workingDirectory = Environment.CurrentDirectory;
        var projectDirectory = Directory.GetParent(workingDirectory)?.Parent?.Parent?.FullName;
        var outputPath = $"{projectDirectory}/output/pathfinder2";
        Directory.CreateDirectory(outputPath);

        using var allSpellsStream = File.OpenWrite($"{outputPath}/AllSpells.json");
        using var allSpellsWriter = new StreamWriter(allSpellsStream);
        allSpellsWriter.Write(JsonSerializer.Serialize(spells));

        var traditions = spells.Select(spell => spell.Traditions).SelectMany(traditions => traditions).ToHashSet();
        traditions.Remove("N/A");
        
        foreach (var tradition in traditions)
        {
            var traditionSpells = spells.Where(spell => spell.Traditions.Contains(tradition));
            Directory.CreateDirectory($"{outputPath}/traditions");
            using var traditionFile = File.OpenWrite($"{outputPath}/traditions/{tradition}.json");
            using var traditionWriter = new StreamWriter(traditionFile);
            traditionWriter.Write(JsonSerializer.Serialize(traditionSpells));
            traditionWriter.Close();
        }

        
        foreach (var characterClass in _classes)
        {
            var traditionSpells = spells.Where(spell => spell.Traits.Contains(characterClass.ToLower())).ToArray();
            if (traditionSpells.Length == 0)
            {
                continue;
            }

            Directory.CreateDirectory($"{outputPath}/classes");
            using var traditionFile = File.OpenWrite($"{outputPath}/classes/{characterClass}.json");
            using var traditionWriter = new StreamWriter(traditionFile);
            traditionWriter.Write(JsonSerializer.Serialize(traditionSpells));
            traditionWriter.Close();
        }
        
        var noTraditionSpells = spells.Where(spell => !spell.Traditions.Any());
        using var noTraditionFile = File.OpenWrite($"{outputPath}/traditions/none.json");
        using var noTraditionWriter = new StreamWriter(noTraditionFile);
        noTraditionWriter.Write(JsonSerializer.Serialize(noTraditionSpells));
        noTraditionWriter.Close();
    }
}