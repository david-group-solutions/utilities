using System.Linq.Expressions;

using DavidGroup.Core.Utilities.Expressions;

namespace DavidGroup.Core.Utilities.Tests.Expressions;

file class Person
{
    public string FirstName { get; set; } = string.Empty;
    public int Age { get; set; }
    public Address Address { get; set; } = new();
}

file class Address
{
    public string City { get; set; } = string.Empty;
    public Country Country { get; set; } = new();
}

file class Country
{
    public string Name { get; set; } = string.Empty;
}

public static class ExpressionsHelperTests
{
    // -------------------------------------------------------------------------
    // GetPropertyPath Tests
    // -------------------------------------------------------------------------

    public class GetPropertyPathTests
    {
        [Fact]
        public void GetPropertyPath_WhenExpressionIsSimpleStringProperty_ReturnsPropertyName()
        {
            // Arrange
            Expression<Func<Person, object>> expression = p => p.FirstName;

            // Act
            string result = ExpressionsHelper.GetPropertyPath(expression);

            // Assert
            Assert.Equal("FirstName", result);
        }

        [Fact]
        public void GetPropertyPath_WhenExpressionIsSimpleValueTypeProperty_ReturnsPropertyName()
        {
            // Arrange
            // Value types (int) are boxed via a Convert UnaryExpression — this exercises the unwrap path
            Expression<Func<Person, object>> expression = p => p.Age;

            // Act
            string result = ExpressionsHelper.GetPropertyPath(expression);

            // Assert
            Assert.Equal("Age", result);
        }

        [Fact]
        public void GetPropertyPath_WhenExpressionIsTwoLevelsDeep_ReturnsDotSeparatedPath()
        {
            // Arrange
            Expression<Func<Person, object>> expression = p => p.Address.Country.Name;

            // Act
            string result = ExpressionsHelper.GetPropertyPath(expression);

            // Assert
            Assert.Equal("Address.Country.Name", result);
        }

        [Fact]
        public void GetPropertyPath_WhenResultContainsDots_HasCorrectSegmentCount()
        {
            // Arrange
            Expression<Func<Person, object>> expression = p => p.Address.Country.Name;

            // Act
            string result = ExpressionsHelper.GetPropertyPath(expression);
            string[] segments = result.Split('.');

            // Assert
            Assert.Equal(3, segments.Length);
        }

        [Fact]
        public void GetPropertyPath_WhenExpressionIsNotMemberAccess_ThrowsInvalidOperationException()
        {
            // Arrange
            Expression<Func<Person, object>> expression = p => p.FirstName.Length.ToString();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => ExpressionsHelper.GetPropertyPath(expression));
        }

        [Fact]
        public void GetPropertyPath_WhenExpressionIsConstant_ThrowsInvalidOperationException()
        {
            // Arrange
            Expression<Func<Person, object>> expression = _ => "constant";

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => ExpressionsHelper.GetPropertyPath(expression));
        }

        [Fact]
        public void GetPropertyPath_WhenPathHasMultipleSegments_DoesNotStartOrEndWithDot()
        {
            // Arrange
            Expression<Func<Person, object>> expression = p => p.Address.City;

            // Act
            string result = ExpressionsHelper.GetPropertyPath(expression);

            // Assert
            Assert.False(result.StartsWith('.'));
            Assert.False(result.EndsWith('.'));
        }
    }
}
