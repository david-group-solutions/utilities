using DavidGroup.Core.Utilities.Cache;

namespace DavidGroup.Core.Utilities.Samples.Cache;

public static class InMemoryTypePropertiesCacheSamples
{
    public static void Run()
    {
        StoreOrRetrieveSample();
    }

    private static void StoreOrRetrieveSample()
    {
        // Retrieves and caches the property names of Person.
        HashSet<string> propertyNames =
            InMemoryTypePropertiesCache.StoreOrRetrieve<Person>();

        Console.WriteLine("Properties:");
        foreach (string propertyName in propertyNames)
            Console.WriteLine($"- {propertyName}");

        bool hasFirstName = propertyNames.Contains("FirstName");
        Console.WriteLine($"Contains 'FirstName': {hasFirstName}");

        // Returns the cached HashSet on subsequent calls.
        HashSet<string> cachedPropertyNames =
            InMemoryTypePropertiesCache.StoreOrRetrieve<Person>();

        Console.WriteLine($"Cache reused: {ReferenceEquals(propertyNames, cachedPropertyNames)}");
    }

    private sealed class Person
    {
        public required string FirstName { get; init; }

        public required string LastName { get; init; }

        public int Age { get; init; }
    }
}
