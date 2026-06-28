namespace DavidGroup.Core.Utilities.Tests;

file enum Color { Red, Green, Blue }

file enum Priority { Low, Medium, High }

public static class EnumHelperTests
{
    // -------------------------------------------------------------------------
    // ToEnum With Default Value Tests
    // -------------------------------------------------------------------------

    public class ToEnumWithDefaultTests
    {
        [Fact]
        public void ToEnum_WhenValueMatchesEnumMember_ReturnsParsedValue()
        {
            // Arrange
            const string value = "Green";

            // Act
            Color result = value.ToEnum<Color>(Color.Red);

            // Assert
            Assert.Equal(Color.Green, result);
        }

        [Fact]
        public void ToEnum_WhenValueIsLowercase_ReturnsParsedValueCaseInsensitively()
        {
            // Arrange
            const string value = "blue";

            // Act
            Color result = value.ToEnum<Color>(Color.Red);

            // Assert
            Assert.Equal(Color.Blue, result);
        }

        [Fact]
        public void ToEnum_WhenValueIsUppercase_ReturnsParsedValueCaseInsensitively()
        {
            // Arrange
            string value = "GREEN";

            // Act
            Color result = value.ToEnum<Color>(Color.Red);

            // Assert
            Assert.Equal(Color.Green, result);
        }

        [Fact]
        public void ToEnum_WhenValueIsMixedCase_ReturnsParsedValueCaseInsensitively()
        {
            // Arrange
            string value = "rEd";

            // Act
            Color result = value.ToEnum<Color>(Color.Blue);

            // Assert
            Assert.Equal(Color.Red, result);
        }

        [Fact]
        public void ToEnum_WhenValueIsNull_ReturnsDefaultValue()
        {
            // Arrange
            string? value = null;

            // Act
            Color result = value.ToEnum<Color>(Color.Blue);

            // Assert
            Assert.Equal(Color.Blue, result);
        }

        [Fact]
        public void ToEnum_WhenValueIsEmpty_ReturnsDefaultValue()
        {
            // Arrange
            string value = string.Empty;

            // Act
            Color result = value.ToEnum<Color>(Color.Green);

            // Assert
            Assert.Equal(Color.Green, result);
        }

        [Fact]
        public void ToEnum_WhenValueIsInvalid_ReturnsDefaultValue()
        {
            // Arrange
            const string value = "Purple";

            // Act
            Color result = value.ToEnum<Color>(Color.Red);

            // Assert
            Assert.Equal(Color.Red, result);
        }

        [Fact]
        public void ToEnum_WhenValueIsWhitespace_ReturnsDefaultValue()
        {
            // Arrange
            const string value = "   ";

            // Act
            Color result = value.ToEnum<Color>(Color.Blue);

            // Assert
            Assert.Equal(Color.Blue, result);
        }

        [Fact]
        public void ToEnum_WhenValueIsNumericString_ReturnsParsedValue()
        {
            // Arrange
            const string value = "1"; // Maps to Color.Green (index 1)

            // Act
            Color result = value.ToEnum<Color>(Color.Blue);

            // Assert
            Assert.Equal(Color.Green, result);
        }
    }

    // -------------------------------------------------------------------------
    // ToEnum Without Default Value Tests
    // -------------------------------------------------------------------------
    public class ToEnumWithoutDefaultTests
    {
        [Fact]
        public void ToEnum_WhenValueMatchesEnumMember_ReturnsParsedValue()
        {
            // Arrange
            const string value = "Red";

            // Act
            Color result = value.ToEnum<Color>();

            // Assert
            Assert.Equal(Color.Red, result);
        }

        [Fact]
        public void ToEnum_WhenValueIsLowercase_ReturnsParsedValueCaseInsensitively()
        {
            // Arrange
            const string value = "green";

            // Act
            Color result = value.ToEnum<Color>();

            // Assert
            Assert.Equal(Color.Green, result);
        }

        [Fact]
        public void ToEnum_WhenValueIsUppercase_ReturnsParsedValueCaseInsensitively()
        {
            // Arrange
            const string value = "BLUE";

            // Act
            Color result = value.ToEnum<Color>();

            // Assert
            Assert.Equal(Color.Blue, result);
        }

        [Fact]
        public void ToEnum_WhenValueIsMixedCase_ReturnsParsedValueCaseInsensitively()
        {
            // Arrange
            const string value = "mEdIuM";

            // Act
            Priority result = value.ToEnum<Priority>();

            // Assert
            Assert.Equal(Priority.Medium, result);
        }

        [Fact]
        public void ToEnum_WhenValueIsInvalid_ThrowsArgumentException()
        {
            // Arrange
            const string value = "Purple";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => value.ToEnum<Color>());
        }

        [Fact]
        public void ToEnum_WhenValueIsEmpty_ThrowsArgumentException()
        {
            // Arrange
            string value = string.Empty;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => value.ToEnum<Color>());
        }

        [Fact]
        public void ToEnum_WhenValueIsWhitespaceOnly_ThrowsArgumentException()
        {
            // Arrange
            const string value = "   ";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => value.ToEnum<Color>());
        }

        [Fact]
        public void ToEnum_WhenValueIsNumericString_ReturnsParsedValue()
        {
            // Arrange
            const string value = "2"; // Maps to Color.Blue (index 2)

            // Act
            Color result = value.ToEnum<Color>();

            // Assert
            Assert.Equal(Color.Blue, result);
        }
    }
}
