namespace DavidGroup.Core.Utilities.Samples;

public static class EnumHelperSamples
{
    public static void Run()
    {
        ToEnumWithDefaultSample();
        ToEnumSample();
    }

    private enum OrderStatus
    {
        Pending,
        Processing,
        Completed,
        Cancelled
    }

    private static void ToEnumWithDefaultSample()
    {
        OrderStatus status = "completed".ToEnum(OrderStatus.Pending);
        Console.WriteLine($"Parsed status: {status}");

        OrderStatus fallbackStatus = "Unknown".ToEnum(OrderStatus.Pending);
        Console.WriteLine($"Fallback status: {fallbackStatus}");
    }

    private static void ToEnumSample()
    {
        OrderStatus status = "Processing".ToEnum<OrderStatus>();
        Console.WriteLine($"Parsed status: {status}");
    }
}
