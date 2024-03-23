// Utility to convert the D20 spell data to a json file for better perf later on

using System.Collections.Immutable;
using System.Globalization;
using System.Reflection;
using System.Text.Json;
using CsvHelper;
using Shared;
using SpellDataConverter;

using var fs = File.OpenRead("./input.csv");
using var sr = new StreamReader(fs);
using var csvReader = new CsvReader(sr, CultureInfo.InvariantCulture);
csvReader.Context.RegisterClassMap<SpellMap>();
var records = csvReader.GetRecords<Spell>().ToArray();

// This will get the current WORKING directory (i.e. \bin\Debug)
var workingDirectory = Environment.CurrentDirectory;
// or: Directory.GetCurrentDirectory() gives the same result
// This will get the current PROJECT directory
var projectDirectory = Directory.GetParent(workingDirectory)?.Parent?.Parent?.FullName;

var classLists = typeof(SpellGrades).GetProperties(BindingFlags.Public | BindingFlags.Instance);
foreach (var classList in classLists)
{
    using var writeFs = File.OpenWrite($@"{projectDirectory}\output\{classList.Name}.json");
    using var sw = new StreamWriter(writeFs);
    var json = JsonSerializer.Serialize(records.Where(spell => classList.GetValue(spell.SpellGrades) != null));
    sw.Write(json);
}

using var allSpellsFs = File.OpenWrite($@"{projectDirectory}\output\AllSpells.json");
using var allSpellsSw = new StreamWriter(allSpellsFs);
var allSpellsJson = JsonSerializer.Serialize(records);
allSpellsSw.Write(allSpellsJson);

var sources = records.Select(spell => spell.Source).ToHashSet().Select(s => new SourceFile(s, s, 0)).OrderBy(s => s.SortPriority).ThenBy(s => s.Name).ToImmutableArray();
using var sourceFile = File.OpenWrite($"{projectDirectory}/output/sources.json");
using var sourceWriter = new StreamWriter(sourceFile);
var sourceJson = JsonSerializer.Serialize(sources, new JsonSerializerOptions(){WriteIndented = true});
sourceWriter.Write(sourceJson);
