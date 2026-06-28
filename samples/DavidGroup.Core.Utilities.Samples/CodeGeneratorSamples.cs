namespace DavidGroup.Core.Utilities.Samples;

public static class CodeGeneratorSamples
{
    public static void Run()
    {
        GenerateCodeSample();
    }

    private static void GenerateCodeSample()
    {
        // Generate the default 8-character code.
        string defaultCode = CodeGenerator.GenerateCode();

        // Generate a custom 16-character code.
        string customCode = CodeGenerator.GenerateCode(16);

        Console.WriteLine($"Default code: {defaultCode}");
        Console.WriteLine($"Custom code:  {customCode}");
    }
}
