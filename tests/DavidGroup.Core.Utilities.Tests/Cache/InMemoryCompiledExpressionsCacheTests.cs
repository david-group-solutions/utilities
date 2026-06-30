using System.Linq.Expressions;
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

public class InMemoryCompiledExpressionsCacheTests
{
    // The static cache persists across tests, so we clear it before each test
    // by accessing the private field via reflection to ensure test isolation.
    public InMemoryCompiledExpressionsCacheTests()
    {
        FieldInfo? field = typeof(InMemoryCompiledExpressionsCache)
            .GetField("InMemory", BindingFlags.NonPublic | BindingFlags.Static);

        (field?.GetValue(null) as System.Collections.Concurrent.ConcurrentDictionary<string, Delegate>)?.Clear();
    }

    [Fact]
    public void StoreOrRetrieve_WhenCalledWithExpression_ReturnsCompiledDelegate()
    {
        // Arrange
        Expression<Func<Person, string>> expression = p => p.FirstName;

        // Act
        Func<Person, string> result = InMemoryCompiledExpressionsCache.StoreOrRetrieve(expression);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void StoreOrRetrieve_WhenDelegateInvoked_ReturnsCorrectValue()
    {
        // Arrange
        Expression<Func<Person, string>> expression = p => p.FirstName;
        Person person = new() { FirstName = "John" };

        // Act
        Func<Person, string> compiled = InMemoryCompiledExpressionsCache.StoreOrRetrieve(expression);
        string result = compiled(person);

        // Assert
        Assert.Equal("John", result);
    }

    [Fact]
    public void StoreOrRetrieve_WhenCalledTwiceWithSameExpression_ReturnsSameDelegate()
    {
        // Arrange
        Expression<Func<Person, int>> expression = p => p.Age;

        // Act
        Func<Person, int> first = InMemoryCompiledExpressionsCache.StoreOrRetrieve(expression);
        Func<Person, int> second = InMemoryCompiledExpressionsCache.StoreOrRetrieve(expression);

        // Assert
        Assert.Same(first, second);
    }

    [Fact]
    public void StoreOrRetrieve_WhenCalledWithDifferentExpressions_ReturnsDifferentDelegates()
    {
        // Arrange
        Expression<Func<Person, string>> firstExpression = p => p.FirstName;
        Expression<Func<Person, string>> secondExpression = p => p.LastName;

        // Act
        Func<Person, string> first = InMemoryCompiledExpressionsCache.StoreOrRetrieve(firstExpression);
        Func<Person, string> second = InMemoryCompiledExpressionsCache.StoreOrRetrieve(secondExpression);

        // Assert
        Assert.NotSame(first, second);
    }

    [Fact]
    public void StoreOrRetrieve_WhenCalledWithIntResultType_ReturnsCorrectValue()
    {
        // Arrange
        Expression<Func<Person, int>> expression = p => p.Age;
        Person person = new() { Age = 30 };

        // Act
        Func<Person, int> compiled = InMemoryCompiledExpressionsCache.StoreOrRetrieve(expression);
        int result = compiled(person);

        // Assert
        Assert.Equal(30, result);
    }

    [Fact]
    public void StoreOrRetrieve_WhenCalledWithDecimalResultType_ReturnsCorrectValue()
    {
        // Arrange
        Expression<Func<Order, decimal>> expression = o => o.Total;
        Order order = new() { Total = 99.99m };

        // Act
        Func<Order, decimal> compiled = InMemoryCompiledExpressionsCache.StoreOrRetrieve(expression);
        decimal result = compiled(order);

        // Assert
        Assert.Equal(99.99m, result);
    }

    [Fact]
    public void StoreOrRetrieve_WhenCalledConcurrently_ReturnsConsistentDelegate()
    {
        // Arrange
        Expression<Func<Person, string>> expression = p => p.FirstName;
        Func<Person, string>[] results = new Func<Person, string>[20];

        // Act
        Parallel.For(0, 20, i =>
        {
            results[i] = InMemoryCompiledExpressionsCache.StoreOrRetrieve(expression);
        });

        // Assert
        Assert.All(results, r => Assert.Same(results[0], r));
    }
}
