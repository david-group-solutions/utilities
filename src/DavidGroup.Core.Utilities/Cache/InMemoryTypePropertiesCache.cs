using System.Collections.Concurrent;

namespace DavidGroup.Core.Utilities.Cache;

/// <summary>
/// Provides in memory caching of compiled expressions to improve performance in repeated operations.
/// </summary>
public static class InMemoryTypePropertiesCache
{
    private static readonly ConcurrentDictionary<Type, HashSet<string>> InMemory = new();

    /// <summary>
    /// Retrieves a set of property names for a given type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type to retrieve property names for.</typeparam>
    /// <returns>A <see cref="HashSet{String}"/> containing all property names of <typeparamref name="T"/>, ignoring case.</returns>
    /// <remarks>
    /// Property names are cached per type to avoid repeated reflection and improve performance.
    /// </remarks>
    public static HashSet<string> StoreOrRetrieve<T>()
    {
        return InMemory.GetOrAdd(
            typeof(T),
            t => t.GetProperties().Select(p => p.Name).ToHashSet(StringComparer.OrdinalIgnoreCase));
    }
}
