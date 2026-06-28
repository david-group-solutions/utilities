using DavidGroup.Core.Utilities.Samples;
using DavidGroup.Core.Utilities.Samples.Cache;
using DavidGroup.Core.Utilities.Samples.Expressions;

Console.WriteLine("======================================");
Console.WriteLine("=              General               =");
Console.WriteLine("======================================");
Console.WriteLine();

Console.WriteLine("=== CommonUtilities ===");
CommonUtilitiesSamples.Run();
Console.WriteLine();

Console.WriteLine("=== CodeGenerator ===");
CodeGeneratorSamples.Run();
Console.WriteLine();

Console.WriteLine("=== EnumHelper ===");
EnumHelperSamples.Run();
Console.WriteLine();

Console.WriteLine("=== StringHelper ===");
StringHelperSamples.Run();
Console.WriteLine();

Console.WriteLine("======================================");
Console.WriteLine("=               Cache                =");
Console.WriteLine("======================================");
Console.WriteLine();

Console.WriteLine("=== InMemoryCompiledExpressionsCache ===");
InMemoryCompiledExpressionsCacheSamples.Run();
Console.WriteLine();

Console.WriteLine("=== InMemoryTypePropertiesCache ===");
InMemoryTypePropertiesCacheSamples.Run();
Console.WriteLine();

Console.WriteLine("======================================");
Console.WriteLine("=            Expressions             =");
Console.WriteLine("======================================");
Console.WriteLine();

Console.WriteLine("=== ExpressionsHelper ===");
ExpressionsHelperSamples.Run();
Console.WriteLine();

Console.WriteLine("=== ReplaceParameterVisitor ===");
ReplaceParameterVisitorSamples.Run();
Console.WriteLine();
