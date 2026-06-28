using System.Reflection;

using DavidGroup.Core.Utilities.Cache;

namespace DavidGroup.Core.Utilities.Tests.Cache;

file class Person
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int Age { get; set; }
}

file class Order
{
    public Guid Id { get; set; }
    public decimal Total { get; set; }
}

file class Empty { }

public class InMemoryTypePropertiesCacheTests
{
    // The static cache persists across tests, so we clear it before each test
    // by accessing the private field via reflection to ensure test isolation.
    public InMemoryTypePropertiesCacheTests()
    {
        FieldInfo? field = typeof(InMemoryTypePropertiesCache)
            .GetField("InMemory", BindingFlags.NonPublic | BindingFlags.Static);

        (field?.GetValue(null) as System.Collections.Concurrent.ConcurrentDictionary<Type, HashSet<string>>)?.Clear();
    }

    [Fact]
    public void StoreOrRetrieve_WhenCalledForType_ReturnsNonNullHashSet()
    {
        // Arrange & Act
        HashSet<string> result = InMemoryTypePropertiesCache.StoreOrRetrieve<Person>();

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void StoreOrRetrieve_WhenCalledForType_ReturnsAllPropertyNames()
    {
        // Arrange & Act
        HashSet<string> result = InMemoryTypePropertiesCache.StoreOrRetrieve<Person>();

        // Assert
        Assert.Contains("FirstName", result);
        Assert.Contains("LastName", result);
        Assert.Contains("Age", result);
    }

    [Fact]
    public void StoreOrRetrieve_WhenCalledForType_ReturnsExactPropertyCount()
    {
        // Arrange & Act
        HashSet<string> result = InMemoryTypePropertiesCache.StoreOrRetrieve<Person>();

        // Assert
        Assert.Equal(3, result.Count);
    }

    [Fact]
    public void StoreOrRetrieve_WhenCalledTwiceForSameType_ReturnsSameHashSetInstance()
    {
        // Arrange & Act
        HashSet<string> first = InMemoryTypePropertiesCache.StoreOrRetrieve<Person>();
        HashSet<string> second = InMemoryTypePropertiesCache.StoreOrRetrieve<Person>();

        // Assert
        Assert.Same(first, second);
    }

    [Fact]
    public void StoreOrRetrieve_WhenCalledForDifferentTypes_ReturnsDifferentHashSets()
    {
        // Arrange & Act
        HashSet<string> personProperties = InMemoryTypePropertiesCache.StoreOrRetrieve<Person>();
        HashSet<string> orderProperties = InMemoryTypePropertiesCache.StoreOrRetrieve<Order>();

        // Assert
        Assert.NotSame(personProperties, orderProperties);
    }

    [Fact]
    public void StoreOrRetrieve_WhenCalledForDifferentTypes_ReturnsCorrectPropertiesPerType()
    {
        // Arrange & Act
        HashSet<string> personProperties = InMemoryTypePropertiesCache.StoreOrRetrieve<Person>();
        HashSet<string> orderProperties = InMemoryTypePropertiesCache.StoreOrRetrieve<Order>();

        // Assert
        Assert.Contains("FirstName", personProperties);
        Assert.DoesNotContain("FirstName", orderProperties);
        Assert.Contains("Total", orderProperties);
        Assert.DoesNotContain("Total", personProperties);
    }

    [Fact]
    public void StoreOrRetrieve_WhenTypeHasNoProperties_ReturnsEmptyHashSet()
    {
        // Arrange & Act
        HashSet<string> result = InMemoryTypePropertiesCache.StoreOrRetrieve<Empty>();

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void StoreOrRetrieve_WhenCalledConcurrently_ReturnsConsistentHashSet()
    {
        // Arrange
        HashSet<string>[] results = new HashSet<string>[20];

        // Act
        Parallel.For(0, 20, i =>
        {
            results[i] = InMemoryTypePropertiesCache.StoreOrRetrieve<Person>();
        });

        // Assert
        Assert.All(results, r => Assert.Same(results[0], r));
    }
}
