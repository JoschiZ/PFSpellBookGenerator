using System.Net.Http.Json;
using Shared;

namespace SpellBookGenerator;

public class SpellService
{
    //private HashSet<Spell> _loadedSpells = [];
    private readonly HttpClient _httpClient;
    private readonly LoadingService _loadingService;

    public IEnumerable<CharacterClass> DataToLoad { get; set; } = [];
    private readonly HashSet<CharacterClass> _loadedDataSets = [];
    private readonly Dictionary<int, Spell> _loadedSpells = []; 
    

    public SpellService(HttpClient httpClient, LoadingService loadingService)
    {
        _httpClient = httpClient;
        _loadingService = loadingService;
    }

    public async Task<IEnumerable<Spell>> GetSpells(CancellationToken cts = default)
    {
        if (_loadedDataSets.Contains(CharacterClass.AllSpells))
        {
            return _loadedSpells.Values.AsEnumerable();
        }

        if (DataToLoad.Contains(CharacterClass.AllSpells))
        {
            var newSpellsTask = _httpClient.GetFromJsonAsync<IEnumerable<Spell>>($"data/allSpells.json", cancellationToken: cts);
            var newSpells = await _loadingService.ShowAsync("Loading All Spells", () => newSpellsTask) ??
                            throw new FileNotFoundException($"Spell Data for allSpells not found");
            foreach (var newSpell in newSpells)
            {
                _loadedSpells.TryAdd(newSpell.Id, newSpell);
            }

            _loadedDataSets.Add(CharacterClass.AllSpells);
            return _loadedSpells.Values.AsEnumerable();
        }
        
        foreach (var classToLoad in DataToLoad)
        {
            if (_loadedDataSets.Contains(classToLoad))
            {
                continue;
            }
            
            Console.Write($"Loading {classToLoad}");

            var newSpellsTask = _httpClient.GetFromJsonAsync<IEnumerable<Spell>>($"data/{classToLoad.ToString()}.json", cancellationToken: cts);
            
            var newSpells = await _loadingService.ShowAsync($"Loading {classToLoad}", () => newSpellsTask) ??
                            throw new FileNotFoundException($"Spell Data for {classToLoad} not found");
            
            foreach (var newSpell in newSpells)
            {
                _loadedSpells.TryAdd(newSpell.Id, newSpell);
            }
            
            _loadedDataSets.Add(classToLoad);
        }

        return _loadedSpells.Values.AsEnumerable();
    }
}