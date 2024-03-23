using System.Collections.Immutable;
using System.Net.Http.Json;
using Shared;

namespace SpellBookGenerator.Core.Spells;

public class SpellService<TSpell> where TSpell: SpellBase
{
    private readonly HttpClient _httpClient;
    private readonly LoadingService _loadingService;

    private readonly Dictionary<CharacterClass, IEnumerable<TSpell>> _spellCache = [];
    private (HashSet<CharacterClass>, IEnumerable<TSpell>)? _mergedSpellListCache;
    
    public SpellService(LoadingService loadingService, HttpClient httpClient)
    {
        _loadingService = loadingService;
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<TSpell>> GetSpellsAsync(IEnumerable<CharacterClass> lists, IEnumerable<SourceFile> sourceFiles, CancellationToken ctx = default)
    {
        var classesToLoad = lists.ToImmutableArray();
        var sourceBooksToLoad = sourceFiles.Select(s => s.InternalName).ToImmutableHashSet();

        // Only load the complete data set, if all data is requested
        if (classesToLoad.Contains(CharacterClass.AllSpells))
        {
            classesToLoad = [CharacterClass.AllSpells];
        }
        
        // See if the last request requested the same data and directly return the cache
        if (_mergedSpellListCache.HasValue)
        {
            var (k, v) = _mergedSpellListCache.Value;
            if (classesToLoad.Length == k.Count && classesToLoad.All(k.Contains))
            {
                return v.Where(s => sourceBooksToLoad.Contains(s.Source));
            }
        }
        
        List<IEnumerable<TSpell>> allSpellLists = [];
        List<Task<(CharacterClass characterClass, Task<IEnumerable<TSpell>?> fetchSpells)>> fetchSpellsTasks = [];
        
        foreach (var characterClass in classesToLoad)
        {
            var spells = _spellCache.GetValueOrDefault(characterClass);
            
            if (spells is not null)
            {
                allSpellLists.Add(spells);
                continue;
            }
            
            var fetchSpells = _httpClient.GetFromJsonAsync<IEnumerable<TSpell>>($"data/{characterClass.ToString()}.json", cancellationToken: ctx);
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
        return allSpells.Where(s => sourceBooksToLoad.Contains(s.Source));
    }
}