using System.Net.Http.Json;
using Shared;

namespace SpellBookGenerator.Core.Sources;

internal sealed class SourceService
{
    private readonly HttpClient _httpClient;
    
    private IEnumerable<SourceFile>? _sourceFilesCache;

    public SourceService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<SourceFile>> GetSourceFilesAsync()
    {
        if (_sourceFilesCache is not null)
        {
            return _sourceFilesCache;
        }

        var sources = await _httpClient.GetFromJsonAsync<IEnumerable<SourceFile>>("data/sources.json");
        _sourceFilesCache = sources?.OrderByDescending(s => s.SortPriority).ThenBy(s => s.Name);
        return _sourceFilesCache ?? throw new Exception("Source Files Missing");
    }
}