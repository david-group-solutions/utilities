using DavidGroup.Core.Utilities.Expressions;

namespace DavidGroup.Core.Utilities.Samples.Expressions;

public static class ExpressionsHelperSamples
{
    public static void Run()
    {
        GetPropertyPathSample();
    }

    private static void GetPropertyPathSample()
    {
        string namePath = ExpressionsHelper.GetPropertyPath<Person>(p => p.Name);
        string cityPath = ExpressionsHelper.GetPropertyPath<Person>(p => p.Address.City);

        Console.WriteLine($"Name path: {namePath}");
        Console.WriteLine($"City path: {cityPath}");
    }

    private sealed class Person
    {
        public required string Name { get; init; }

        public required Address Address { get; init; }
    }

    private sealed class Address
    {
        public required string City { get; init; }
    }
}
