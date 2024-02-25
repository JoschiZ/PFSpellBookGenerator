using System.IO.Compression;
using System.Net.Http.Json;
using Shared;

namespace SpellBookGenerator;

public class SpellService
{
    private readonly HttpClient _httpClient;
    private readonly LoadingService _loadingService;

    private readonly Dictionary<CharacterClass, IEnumerable<Spell>> _spellCache = [];
    
    public SpellService(LoadingService loadingService, HttpClient httpClient)
    {
        _loadingService = loadingService;
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<Spell>> GetSpellsAsync(IEnumerable<CharacterClass> lists, CancellationToken ctx = default)
    {
        List<IEnumerable<Spell>> allSpellLists = [];
        List<Task<(CharacterClass characterClass, Task<IEnumerable<Spell>?> fetchSpells)>> fetchSpellsTasks = [];
        
        foreach (var characterClass in lists)
        {
            var spells = _spellCache.GetValueOrDefault(characterClass);
            
            if (spells is not null)
            {
                allSpellLists.Add(spells);
                continue;
            }
            
            var fetchSpells = _httpClient.GetFromJsonAsync<IEnumerable<Spell>>($"data/{characterClass.ToString()}.json", cancellationToken: ctx);
            fetchSpellsTasks.Add(Task.FromResult((characterClass, fetchSpells)));
        }

        var listsToLoad = fetchSpellsTasks.Count;
        var doLoadingStep = await _loadingService.StartSteppedLoading(listsToLoad, $"Fetching spell data 0 / {listsToLoad}");
        
        while (fetchSpellsTasks.Count != 0)
        {
            var finishedTask = await Task.WhenAny(fetchSpellsTasks);
            fetchSpellsTasks.Remove(finishedTask);
            await doLoadingStep($"Fetching spell data {listsToLoad - fetchSpellsTasks.Count} / {listsToLoad}");

            var (characterClass, spellsTask) = await finishedTask;
            var spells = await spellsTask;
            _spellCache.Add(characterClass, spells ?? throw new Exception($"Loading of {characterClass} failed"));
            allSpellLists.Add(spells);
        }

        await _loadingService.FinishLoading();
        var allSpells = allSpellLists.SelectMany(s => s).ToDictionary(s => s.Id, s=> s).Values;
        return allSpells;
    }
}