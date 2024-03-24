using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Json;
using Shared;

namespace SpellBookGenerator.Core.Spells;

public class Pathfinder2SpellService
{
    private readonly HttpClient _httpClient;
    private readonly LoadingService _loadingService;

    private readonly Dictionary<CharacterClass.Pathfinder2, IEnumerable<Pathfinder2Spell>> _classSpellCache = [];
    private readonly Dictionary<Tradition, IEnumerable<Pathfinder2Spell>> _traditionSpellCache = [];
    
    private (ImmutableHashSet<CharacterClass.Pathfinder2> requestedClasses, ImmutableHashSet<Tradition> requestedTraditions, IEnumerable<Pathfinder2Spell> fullSpellResponse)? _mergedSpellListCache;

    public Pathfinder2SpellService(LoadingService loadingService, HttpClient httpClient)
    {
        _loadingService = loadingService;
        _httpClient = httpClient;
    }

    /// <summary>
    /// Checks if the last requested set of data is equal to the currently requested set of data. 
    /// </summary>
    /// <param name="classes"></param>
    /// <param name="traditions"></param>
    /// <param name="returnCachedSpells"></param>
    /// <returns></returns>
    private bool TryGetMergedCache(ImmutableHashSet<CharacterClass.Pathfinder2> classes, ImmutableHashSet<Tradition> traditions, [NotNullWhen(returnValue: true)] out IEnumerable<Pathfinder2Spell>? returnCachedSpells)
    {
        returnCachedSpells = null;
        
        if (_mergedSpellListCache is null)
        {
            return false;
        }

        var (cachedClasses, cachedTraditions, cachedSpells) = _mergedSpellListCache.Value;
        if (classes.SetEquals(cachedClasses) && traditions.SetEquals(cachedTraditions))
        {
            returnCachedSpells = cachedSpells;
            return true;
        }

        return false;
    }
    
    public async Task<IEnumerable<Pathfinder2Spell>> GetSpellsAsync(IEnumerable<CharacterClass.Pathfinder2> lists, IEnumerable<Tradition> traditions, CancellationToken ctx = default)
    {
        var classesToLoad = lists.ToImmutableHashSet();
        var traditionsToLoad = traditions.ToImmutableHashSet();

        // Only load the complete data set, if all data is requested
        if (classesToLoad.Contains(CharacterClass.Pathfinder2.AllSpells))
        {
            classesToLoad = [CharacterClass.Pathfinder2.AllSpells];
        }

        if (traditionsToLoad.Contains(Tradition.All))
        {
            traditionsToLoad = [Tradition.All];
            
            //Loading all tradition is equal to loading all spells, we can just ignore the classes going forward
            classesToLoad = [];
        }

        if (TryGetMergedCache(classesToLoad, traditionsToLoad, out var cachedSpells))
        {
            return cachedSpells;
        }
        
        List<IEnumerable<Pathfinder2Spell>> allSpellLists = [];
        
        
        List<Task<(CharacterClass.Pathfinder2 characterClass, Task<IEnumerable<Pathfinder2Spell>?> fetchSpells)>> fetchClassesTasks = [];
        foreach (var characterClass in classesToLoad)
        {
            if (_classSpellCache.TryGetValue(characterClass, out var spells))
            {
                allSpellLists.Add(spells);
                continue;
            }

            var requestUrl = $"data/pathfinder2/classes/{characterClass.ToString()}.json";
            var fetchSpellsTask = _httpClient.GetFromJsonAsync<IEnumerable<Pathfinder2Spell>>(requestUrl, cancellationToken: ctx);
            fetchClassesTasks.Add(Task.FromResult((characterClass, fetchSpells: fetchSpellsTask)));
        }


        List<Task<(Tradition tradition, Task<IEnumerable<Pathfinder2Spell>?> fetchSpells)>> fetchTraditionsTasks = [];
        foreach (var tradition in traditionsToLoad)
        {
            if ( _traditionSpellCache.TryGetValue(tradition, out var spells))
            {
                allSpellLists.Add(spells);
                continue;
            }

            var requestUrl = $"data/pathfinder2/traditions/{tradition.ToString()}.json";
            if (tradition == Tradition.All)
            {
                requestUrl = $"data/pathfinder2/AllSpells.json";
            }
            var fetchSpellsTask = _httpClient.GetFromJsonAsync<IEnumerable<Pathfinder2Spell>>(requestUrl, cancellationToken: ctx);
            fetchTraditionsTasks.Add(Task.FromResult((tradition, fetchSpells: fetchSpellsTask)));
        }

        var listsToLoad = fetchClassesTasks.Count + traditionsToLoad.Count;
        var doLoadingStep = await _loadingService.StartSteppedLoading(listsToLoad, $"Fetching spell data 0 / {listsToLoad}");
        
        while (fetchClassesTasks.Count != 0)
        {
            var finishedTask = await Task.WhenAny(fetchClassesTasks);
            fetchClassesTasks.Remove(finishedTask);
            var (characterClass, spellsTask) = await finishedTask;
            var spells = await spellsTask;
            await doLoadingStep($"Fetching spell data {listsToLoad - fetchClassesTasks.Count} / {listsToLoad}", $"Finished: {characterClass.ToString()}");
            _classSpellCache.Add(characterClass, spells ?? throw new Exception($"Loading of {characterClass} failed"));
            allSpellLists.Add(spells);
        }
        
        while (fetchTraditionsTasks.Count != 0)
        {
            var finishedTask = await Task.WhenAny(fetchTraditionsTasks);
            fetchTraditionsTasks.Remove(finishedTask);
            var (characterClass, spellsTask) = await finishedTask;
            var spells = await spellsTask;
            await doLoadingStep($"Fetching spell data {listsToLoad - fetchClassesTasks.Count} / {listsToLoad}", $"Finished: {characterClass.ToString()}");
            _traditionSpellCache.Add(characterClass, spells ?? throw new Exception($"Loading of {characterClass} failed"));
            allSpellLists.Add(spells);
        }
        
        var allSpells = allSpellLists
            .SelectMany(s => s)
            .GroupBy(s => s.Id)
            .ToDictionary(group => group.Key, spells => spells.First())
            .Values;

        // Cache the request, because it is likely that the next one will request the same data again
        _mergedSpellListCache = (classesToLoad, traditionsToLoad, allSpells);
        await _loadingService.FinishLoading();
        return allSpells;
    }
}