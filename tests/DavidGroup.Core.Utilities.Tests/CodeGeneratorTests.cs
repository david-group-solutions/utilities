using System.Text.RegularExpressions;

namespace DavidGroup.Core.Utilities.Tests;

public class CodeGeneratorTests
{
    [Fact]
    public void GenerateCode_WhenCalledWithDefaultLength_ReturnsEightCharacterString()
    {
        // Arrange
        const int expectedLength = 8;

        // Act
        string result = CodeGenerator.GenerateCode();

        // Assert
        Assert.Equal(expectedLength, result.Length);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(8)]
    [InlineData(16)]
    [InlineData(64)]
    [InlineData(128)]
    public void GenerateCode_WhenCalledWithSpecificLength_ReturnsStringOfThatLength(int length)
    {
        // Arrange & Act
        string result = CodeGenerator.GenerateCode(length);

        // Assert
        Assert.Equal(length, result.Length);
    }

    [Fact]
    public void GenerateCode_WhenCalled_ReturnsOnlyBase62Characters()
    {
        // Arrange
        Regex base62Pattern = new("^[0-9A-Za-z]+$");

        // Act
        string result = CodeGenerator.GenerateCode(256);

        // Assert
        Assert.Matches(base62Pattern, result);
    }

    [Fact]
    public void GenerateCode_WhenCalledTwice_ReturnsDifferentValues()
    {
        // Arrange & Act
        string first = CodeGenerator.GenerateCode();
        string second = CodeGenerator.GenerateCode();

        // Assert
        Assert.NotEqual(first, second);
    }

    [Fact]
    public void GenerateCode_WhenCalledMultipleTimes_AllResultsHaveCorrectLength()
    {
        // Arrange
        const int length = 12;
        const int iterations = 100;

        // Act
        IEnumerable<string> results = Enumerable.Range(0, iterations)
            .Select(_ => CodeGenerator.GenerateCode(length));

        // Assert
        Assert.All(results, code => Assert.Equal(length, code.Length));
    }

    [Fact]
    public void GenerateCode_WhenCalledMultipleTimes_AllResultsContainOnlyBase62Characters()
    {
        // Arrange
        const int iterations = 100;
        Regex base62Pattern = new("^[0-9A-Za-z]+$");

        // Act
        IEnumerable<string> results = Enumerable.Range(0, iterations)
            .Select(_ => CodeGenerator.GenerateCode());

        // Assert
        Assert.All(results, code => Assert.Matches(base62Pattern, code));
    }

    [Fact]
    public void GenerateCode_WhenCalledWithLargeLength_ReturnsStringOfCorrectLength()
    {
        // Arrange
        const int largeLength = 10_000;

        // Act
        string result = CodeGenerator.GenerateCode(largeLength);

        // Assert
        Assert.Equal(largeLength, result.Length);
    }
}
