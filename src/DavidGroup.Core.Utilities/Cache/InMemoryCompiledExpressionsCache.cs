using System.Collections.Concurrent;
using System.Linq.Expressions;

namespace DavidGroup.Core.Utilities.Cache;

/// <summary>
/// Provides in memory caching of compiled expressions to improve performance in repeated operations.
/// </summary>
public static class InMemoryCompiledExpressionsCache
{
    private static readonly ConcurrentDictionary<string, Delegate> InMemory = new();

    /// <summary>
    /// Compiles an expression of type <see cref="Expression"/> into a delegate
    /// and caches it for subsequent use.
    /// </summary>
    /// <typeparam name="T">The type of the source object.</typeparam>
    /// <typeparam name="TResult">The type of the result object.</typeparam>
    /// <param name="expression">The expression to compile and cache.</param>
    /// <returns>A compiled delegate.</returns>
    /// <remarks>
    /// The expression is cached using <see cref="Expression.ToString"/> as the key.
    /// This ensures that repeated calls with the same expression instance do not incur
    /// the cost of recompiling.
    /// </remarks>
    public static Func<T, TResult> StoreOrRetrieve<T, TResult>(
        Expression<Func<T, TResult>> expression)
    {
        return (Func<T, TResult>)InMemory.GetOrAdd(
            expression.ToString(),
            _ => expression.Compile());
    }
}
