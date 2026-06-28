namespace DavidGroup.Core.Utilities.Samples;

public static class CommonUtilitiesSamples
{
    public static void Run()
    {
        CalculateAgeSample();
        DetermineGeohashPrecisionSample();
        MapSample();
        IsInRangeSample();
    }

    private static void CalculateAgeSample()
    {
        DateTime birthDate = new(1995, 7, 15);
        DateTime today = new(2026, 6, 28);

        int age = birthDate.CalculateAge(today);

        Console.WriteLine($"Age: {age}");
    }

    private static void DetermineGeohashPrecisionSample()
    {
        // Small city area
        int precision = CommonUtilities.DetermineGeohashPrecision(
            topLeftLat: 40.205,
            bottomRightLat: 40.165,
            topLeftLon: 44.480,
            bottomRightLon: 44.540);

        Console.WriteLine($"Recommended geohash precision: {precision}");
    }

    private static void MapSample()
    {
        const decimal percentage = 75m;

        // Map 75 from the range 0–100 to the range 0–1
        decimal normalized = percentage.Map(
            fromSource: 0,
            toSource: 100,
            fromTarget: 0,
            toTarget: 1);

        Console.WriteLine($"Normalized value: {normalized}");
    }

    private static void IsInRangeSample()
    {
        bool isContained = CommonUtilities.IsInRange(
            outerStart: 0,
            outerEnd: 100,
            innerStart: 25,
            innerEnd: 75);

        Console.WriteLine($"Inner range is contained: {isContained}");
    }
}
