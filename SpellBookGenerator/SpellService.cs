using System.Net.Http.Json;
using Shared;

namespace SpellBookGenerator;

public class SpellService
{
    private HashSet<Spell> _loadedSpells = [];
    private readonly HttpClient _httpClient;

    public IEnumerable<Classes> DataToLoad { get; set; } = [];
    private readonly HashSet<Classes> _loadedDataSets = [];

    public SpellService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<HashSet<Spell>> GetSpells()
    {
        if (_loadedDataSets.Contains(Classes.AllSpells))
        {
            return _loadedSpells;
        }

        if (DataToLoad.Contains(Classes.AllSpells))
        {
            _loadedSpells = await _httpClient.GetFromJsonAsync<HashSet<Spell>>($"data/allSpells.json") ??
                            throw new FileNotFoundException($"Spell Data for allSpells not found");
            _loadedDataSets.Add(Classes.AllSpells);
            return _loadedSpells;
        }
        
        foreach (var classToLoad in DataToLoad)
        {
            if (_loadedDataSets.Contains(classToLoad))
            {
                continue;
            }
            
            var spells = await _httpClient.GetFromJsonAsync<HashSet<Spell>>($"data/{classToLoad.ToString()}.json") ??
                         throw new FileNotFoundException($"Spell Data for {classToLoad} not found");
            _loadedSpells.UnionWith(spells);
            
            _loadedDataSets.Add(classToLoad);
        }

        return _loadedSpells;
    }
}