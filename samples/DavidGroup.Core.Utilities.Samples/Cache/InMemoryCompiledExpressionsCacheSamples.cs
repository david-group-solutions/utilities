using System.Linq.Expressions;

using DavidGroup.Core.Utilities.Cache;

namespace DavidGroup.Core.Utilities.Samples.Cache;

public static class InMemoryCompiledExpressionsCacheSamples
{
    public static void Run()
    {
        StoreOrRetrieveSample();
    }

    private static void StoreOrRetrieveSample()
    {
        Expression<Func<Person, string>> expression = p => p.FullName;

        // Compiles and caches the expression on first use.
        Func<Person, string> getFullName =
            InMemoryCompiledExpressionsCache.StoreOrRetrieve(expression);

        Person person = new() { FullName = "John Doe" };

        string fullName = getFullName(person);

        Console.WriteLine($"Full name: {fullName}");

        // Retrieves the previously compiled delegate from the cache.
        Func<Person, string> cachedDelegate =
            InMemoryCompiledExpressionsCache.StoreOrRetrieve(expression);

        Console.WriteLine($"Delegate reused: {ReferenceEquals(getFullName, cachedDelegate)}");
    }

    private sealed class Person
    {
        public required string FullName { get; init; }
    }
}
