namespace DavidGroup.Core.Utilities.Tests;

public class StringHelperTests
{
    [Fact]
    public void FirstCharToLower_WhenFirstCharIsUppercase_ReturnsStringWithFirstCharLowercased()
    {
        // Arrange
        const string input = "Hello";

        // Act
        string result = StringHelper.FirstCharToLower(input);

        // Assert
        Assert.Equal("hello", result);
    }

    [Fact]
    public void FirstCharToLower_WhenFirstCharIsAlreadyLowercase_ReturnsOriginalString()
    {
        // Arrange
        const string input = "hello";

        // Act
        string result = StringHelper.FirstCharToLower(input);

        // Assert
        Assert.Equal("hello", result);
    }

    [Fact]
    public void FirstCharToLower_WhenStringIsNull_ReturnsNull()
    {
        // Arrange
        string? input = null;

        // Act
        string result = StringHelper.FirstCharToLower(input!);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void FirstCharToLower_WhenStringIsEmpty_ReturnsEmptyString()
    {
        // Arrange
        string input = string.Empty;

        // Act
        string result = StringHelper.FirstCharToLower(input);

        // Assert
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void FirstCharToLower_WhenStringIsSingleUppercaseChar_ReturnsLowercasedChar()
    {
        // Arrange
        const string input = "A";

        // Act
        string result = StringHelper.FirstCharToLower(input);

        // Assert
        Assert.Equal("a", result);
    }

    [Fact]
    public void FirstCharToLower_WhenStringIsSingleLowercaseChar_ReturnsOriginalString()
    {
        // Arrange
        const string input = "a";

        // Act
        string result = StringHelper.FirstCharToLower(input);

        // Assert
        Assert.Equal("a", result);
    }

    [Fact]
    public void FirstCharToLower_WhenRemainingCharsAreUppercase_LeavesThemUnchanged()
    {
        // Arrange
        const string input = "HELLO";

        // Act
        string result = StringHelper.FirstCharToLower(input);

        // Assert
        Assert.Equal("hELLO", result);
    }

    [Fact]
    public void FirstCharToLower_WhenStringStartsWithDigit_ReturnsOriginalString()
    {
        // Arrange
        const string input = "1Hello";

        // Act
        string result = StringHelper.FirstCharToLower(input);

        // Assert
        Assert.Equal("1Hello", result);
    }

    [Fact]
    public void FirstCharToLower_WhenStringStartsWithSpecialChar_ReturnsOriginalString()
    {
        // Arrange
        const string input = "_Hello";

        // Act
        string result = StringHelper.FirstCharToLower(input);

        // Assert
        Assert.Equal("_Hello", result);
    }

    [Fact]
    public void FirstCharToLower_WhenStringIsPascalCase_ReturnsCamelCase()
    {
        // Arrange
        const string input = "MyPropertyName";

        // Act
        string result = StringHelper.FirstCharToLower(input);

        // Assert
        Assert.Equal("myPropertyName", result);
    }
}
