using System.Collections.Immutable;
using System.Globalization;
using System.Reflection;
using System.Text.Json;
using CsvHelper;
using Shared;

namespace SpellDataConverter;

internal static class Pathfinder1
{

    public static void ConvertSpells()
    {
        // Utility to convert the D20 spell data to a json file for better perf later on
        using var fs = File.OpenRead("./input.csv");
        using var sr = new StreamReader(fs);
        using var csvReader = new CsvReader(sr, CultureInfo.InvariantCulture);
        csvReader.Context.RegisterClassMap<SpellMap>();
        var records = csvReader.GetRecords<Pathfinder1Spell>().ToArray();

// This will get the current WORKING directory (i.e. \bin\Debug)
        var workingDirectory = Environment.CurrentDirectory;
// or: Directory.GetCurrentDirectory() gives the same result
// This will get the current PROJECT directory
        var projectDirectory = Directory.GetParent(workingDirectory)?.Parent?.Parent?.FullName;
        var outputPath = $"{projectDirectory}/output/pathfinder1";
        Directory.CreateDirectory(outputPath);

        var classLists = typeof(Levels).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        foreach (var classList in classLists)
        {
            using var writeFs = File.OpenWrite($"{outputPath}/{classList.Name}.json");
            using var sw = new StreamWriter(writeFs);
            var json = JsonSerializer.Serialize(records.Where(spell => classList.GetValue(spell.Levels) != null));
            sw.Write(json);
        }

        using var allSpellsFs = File.OpenWrite($"{outputPath}/AllSpells.json");
        using var allSpellsSw = new StreamWriter(allSpellsFs);
        var allSpellsJson = JsonSerializer.Serialize(records);
        allSpellsSw.Write(allSpellsJson);

        var sources = records.Select(spell => spell.Source).ToHashSet().Select(s => new SourceFile(s, s, 0)).OrderBy(s => s.SortPriority).ThenBy(s => s.Name).ToImmutableArray();
        using var sourceFile = File.OpenWrite($"{outputPath}/sources.json");
        using var sourceWriter = new StreamWriter(sourceFile);
        var sourceJson = JsonSerializer.Serialize(sources, new JsonSerializerOptions(){WriteIndented = true});
        sourceWriter.Write(sourceJson);
    }
}