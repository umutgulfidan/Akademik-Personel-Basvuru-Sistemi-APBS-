using Core.CrossCuttingConcerns.Caching;
using Microsoft.Extensions.Caching.Memory;
using System.Text.RegularExpressions;

public class MemoryCacheManager : ICacheManager
{
    private readonly IMemoryCache _memoryCache;
    private readonly List<string> _cacheKeys;  // Eklenen anahtarları saklamak için

    public MemoryCacheManager(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
        _cacheKeys = new List<string>();
    }

    public void Add(string key, object value, int duration)
    {
        _memoryCache.Set(key, value, TimeSpan.FromMinutes(duration));
        if (!_cacheKeys.Contains(key))
        {
            _cacheKeys.Add(key);
        }
    }

    public T Get<T>(string key) => _memoryCache.Get<T>(key);

    public object Get(string key) => _memoryCache.Get(key);

    public bool IsAdd(string key) => _memoryCache.TryGetValue(key, out _);

    public void Remove(string key)
    {
        _memoryCache.Remove(key);
        _cacheKeys.Remove(key);
    }

    public void RemoveByPattern(string pattern)
    {
        var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
        var keysToRemove = _cacheKeys.Where(key => regex.IsMatch(key)).ToList();
        foreach (var key in keysToRemove)
        {
            _memoryCache.Remove(key);
            _cacheKeys.Remove(key);
        }
    }
}
