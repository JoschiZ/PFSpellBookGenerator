using System.Collections.Immutable;
using System.Net.Http.Json;
using Shared;

namespace SpellBookGenerator.Core.Spells;

public class Pathfinder1SpellService
{
    private readonly HttpClient _httpClient;
    private readonly LoadingService _loadingService;

    private readonly Dictionary<CharacterClass.Pathfinder1, IEnumerable<Pathfinder1Spell>> _spellCache = [];
    private (HashSet<CharacterClass.Pathfinder1>, IEnumerable<Pathfinder1Spell>)? _mergedSpellListCache;
    
    public Pathfinder1SpellService(LoadingService loadingService, HttpClient httpClient)
    {
        _loadingService = loadingService;
        _httpClient = httpClient;
    }


    public async Task<IEnumerable<Pathfinder1Spell>> GetSpellsAsync(IEnumerable<CharacterClass.Pathfinder1> lists, IEnumerable<SourceFile> sourceFiles, CancellationToken ctx = default)
    {
        var spells = await GetSpellsAsync(lists, ctx);
        var booksToLoad = sourceFiles.Select(s => s.InternalName).ToHashSet();
        return spells.Where(s => booksToLoad.Contains(s.Source));
    }
    
    public async Task<IEnumerable<Pathfinder1Spell>> GetSpellsAsync(IEnumerable<CharacterClass.Pathfinder1> lists, CancellationToken ctx = default)
    {
        var classesToLoad = lists.ToImmutableArray();

        // Only load the complete data set, if all data is requested
        if (classesToLoad.Contains(CharacterClass.Pathfinder1.AllSpells))
        {
            classesToLoad = [CharacterClass.Pathfinder1.AllSpells];
        }
        
        // See if the last request requested the same data and directly return the cache
        if (_mergedSpellListCache.HasValue)
        {
            var (k, v) = _mergedSpellListCache.Value;
            if (classesToLoad.Length == k.Count && classesToLoad.All(k.Contains))
            {
                return v;
            }
        }
        
        List<IEnumerable<Pathfinder1Spell>> allSpellLists = [];
        List<Task<(CharacterClass.Pathfinder1 characterClass, Task<IEnumerable<Pathfinder1Spell>?> fetchSpells)>> fetchSpellsTasks = [];
        
        foreach (var characterClass in classesToLoad)
        {
            var spells = _spellCache.GetValueOrDefault(characterClass);
            
            if (spells is not null)
            {
                allSpellLists.Add(spells);
                continue;
            }
            
            var fetchSpells = _httpClient.GetFromJsonAsync<IEnumerable<Pathfinder1Spell>>($"data/pathfinder1/{characterClass.ToString()}.json", cancellationToken: ctx);
            fetchSpellsTasks.Add(Task.FromResult((characterClass, fetchSpells)));
        }

        var listsToLoad = fetchSpellsTasks.Count;
        var doLoadingStep = await _loadingService.StartSteppedLoading(listsToLoad, $"Fetching spell data 0 / {listsToLoad}");
        
        while (fetchSpellsTasks.Count != 0)
        {
            var finishedTask = await Task.WhenAny(fetchSpellsTasks);
            fetchSpellsTasks.Remove(finishedTask);
            var (characterClass, spellsTask) = await finishedTask;
            var spells = await spellsTask;
            await doLoadingStep($"Fetching spell data {listsToLoad - fetchSpellsTasks.Count} / {listsToLoad}", $"Finished: {characterClass.ToString()}");
            _spellCache.Add(characterClass, spells ?? throw new Exception($"Loading of {characterClass} failed"));
            allSpellLists.Add(spells);
        }
        
        var allSpells = allSpellLists
            .SelectMany(s => s)
            .GroupBy(s => s.Id)
            .ToDictionary(group => group.Key, spells => spells.First())
            .Values;

        // Cache the request, because it is likely that the next one will request the same data again
        _mergedSpellListCache = (classesToLoad.ToHashSet(), allSpells);
        await _loadingService.FinishLoading();
        return allSpells;
    }
}