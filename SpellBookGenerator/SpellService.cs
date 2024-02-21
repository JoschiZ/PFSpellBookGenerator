using System.Net.Http.Json;
using Shared;

namespace SpellBookGenerator;

public class SpellService
{
    //private HashSet<Spell> _loadedSpells = [];
    private readonly HttpClient _httpClient;

    public IEnumerable<Classes> DataToLoad { get; set; } = [];
    private readonly HashSet<Classes> _loadedDataSets = [];
    private readonly Dictionary<int, Spell> _loadedSpells = []; 

    public SpellService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<Spell>> GetSpells()
    {
        if (_loadedDataSets.Contains(Classes.AllSpells))
        {
            return _loadedSpells.Values.AsEnumerable();
        }

        if (DataToLoad.Contains(Classes.AllSpells))
        {
            var newSpells = await _httpClient.GetFromJsonAsync<IEnumerable<Spell>>($"data/allSpells.json") ??
                            throw new FileNotFoundException($"Spell Data for allSpells not found");
            foreach (var newSpell in newSpells)
            {
                _loadedSpells.TryAdd(newSpell.Id, newSpell);
            }

            return _loadedSpells.Values.AsEnumerable();
        }
        
        foreach (var classToLoad in DataToLoad)
        {
            if (_loadedDataSets.Contains(classToLoad))
            {
                continue;
            }
            
            var newSpells = await _httpClient.GetFromJsonAsync<IEnumerable<Spell>>($"data/{classToLoad.ToString()}.json") ??
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