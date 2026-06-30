namespace DavidGroup.Core.Utilities.Samples;

public static class StringHelperSamples
{
    public static void Run()
    {
        FirstCharToLowerSample();
    }

    private static void FirstCharToLowerSample()
    {
        const string propertyName = "FirstName";
        string camelCaseName = StringHelper.FirstCharToLower(propertyName);

        Console.WriteLine($"Original: {propertyName}");
        Console.WriteLine($"Camel case: {camelCaseName}");
    }
}
