namespace DavidGroup.Core.Utilities.Tests;

public static class CommonUtilitiesTests
{
    // -------------------------------------------------------------------------
    // CalculateAge Tests
    // -------------------------------------------------------------------------
    public class CalculateAgeTests
    {
        [Fact]
        public void CalculateAge_WhenBirthdayAlreadyOccurredThisYear_ReturnsCorrectAge()
        {
            // Arrange
            DateTime birthDate = new(1990, 3, 15);
            DateTime currentDate = new(2025, 6, 1);

            // Act
            int age = birthDate.CalculateAge(currentDate);

            // Assert
            Assert.Equal(35, age);
        }

        [Fact]
        public void CalculateAge_WhenBirthdayHasNotYetOccurredThisYear_ReturnsOneYearLess()
        {
            // Arrange
            DateTime birthDate = new(1990, 12, 31);
            DateTime currentDate = new(2025, 6, 1);

            // Act
            int age = birthDate.CalculateAge(currentDate);

            // Assert
            Assert.Equal(34, age);
        }

        [Fact]
        public void CalculateAge_WhenTodayIsExactlyTheBirthday_ReturnsPreviousAge()
        {
            // Arrange
            DateTime birthDate = new(1990, 6, 1);
            DateTime currentDate = new(2025, 6, 1);

            // Act
            int age = birthDate.CalculateAge(currentDate);

            // Assert
            Assert.Equal(34, age);
        }

        [Fact]
        public void CalculateAge_WhenCurrentDateIsDefault_UsesUtcNow()
        {
            // Arrange
            DateTime birthDate = DateTime.UtcNow.AddYears(-30);

            // Act
            int age = birthDate.CalculateAge();

            // Assert
            Assert.Equal(30, age);
        }

        [Fact]
        public void CalculateAge_WhenBornOnLeapDay_DoesNotThrowAndReturnsCorrectAge()
        {
            // Arrange
            DateTime birthDate = new(2000, 2, 29);
            DateTime currentDate = new(2025, 3, 1);

            // Act
            int age = birthDate.CalculateAge(currentDate);

            // Assert
            Assert.Equal(25, age);
        }

        [Fact]
        public void CalculateAge_WhenAgeIsZero_ReturnsZero()
        {
            // Arrange
            DateTime birthDate = new(2025, 6, 1);
            DateTime currentDate = new(2025, 6, 1);

            // Act
            int age = birthDate.CalculateAge(currentDate);

            // Assert
            Assert.Equal(0, age);
        }

        [Fact]
        public void CalculateAge_WhenCurrentDateIsOneDayBeforeBirthday_ReturnsOneYearLess()
        {
            // Arrange
            DateTime birthDate = new(1990, 6, 2);
            DateTime currentDate = new(2025, 6, 1);

            // Act
            int age = birthDate.CalculateAge(currentDate);

            // Assert
            Assert.Equal(34, age);
        }

        [Fact]
        public void CalculateAge_WhenCurrentDateIsOneDayAfterBirthday_ReturnsCorrectAge()
        {
            // Arrange
            DateTime birthDate = new(1990, 5, 31);
            DateTime currentDate = new(2025, 6, 1);

            // Act
            int age = birthDate.CalculateAge(currentDate);

            // Assert
            Assert.Equal(35, age);
        }
    }

    // -------------------------------------------------------------------------
    // DetermineGeohashPrecision Tests
    // -------------------------------------------------------------------------
    public class DetermineGeohashPrecisionTests
    {
        [Fact]
        public void DetermineGeohashPrecision_WhenLatDiffExceedsTen_ReturnsCountryLevel()
        {
            // Arrange
            const double topLeftLat = 60.0;
            const double bottomRightLat = 30.0; // latDiff = 30
            const double topLeftLon = 10.0;
            const double bottomRightLon = 15.0; // lonDiff = 5

            // Act
            int precision = CommonUtilities.DetermineGeohashPrecision(
                topLeftLat, bottomRightLat, topLeftLon, bottomRightLon);

            // Assert
            Assert.Equal(3, precision);
        }

        [Fact]
        public void DetermineGeohashPrecision_WhenLonDiffExceedsTen_ReturnsCountryLevel()
        {
            // Arrange
            const double topLeftLat = 52.0;
            const double bottomRightLat = 50.0; // latDiff = 2
            const double topLeftLon = 10.0;
            const double bottomRightLon = 25.0; // lonDiff = 15

            // Act
            int precision = CommonUtilities.DetermineGeohashPrecision(
                topLeftLat, bottomRightLat, topLeftLon, bottomRightLon);

            // Assert
            Assert.Equal(3, precision);
        }

        [Fact]
        public void DetermineGeohashPrecision_WhenDiffBetweenFiveAndTen_ReturnsRegionalLevel()
        {
            // Arrange
            const double topLeftLat = 55.0;
            const double bottomRightLat = 48.0; // latDiff = 7
            const double topLeftLon = 10.0;
            const double bottomRightLon = 13.0; // lonDiff = 3

            // Act
            int precision = CommonUtilities.DetermineGeohashPrecision(
                topLeftLat, bottomRightLat, topLeftLon, bottomRightLon);

            // Assert
            Assert.Equal(4, precision);
        }

        [Fact]
        public void DetermineGeohashPrecision_WhenDiffBetweenOneAndFive_ReturnsCityLevel()
        {
            // Arrange
            const double topLeftLat = 52.0;
            const double bottomRightLat = 50.5; // latDiff = 1.5
            const double topLeftLon = 13.0;
            const double bottomRightLon = 14.0; // lonDiff = 1

            // Act
            int precision = CommonUtilities.DetermineGeohashPrecision(
                topLeftLat, bottomRightLat, topLeftLon, bottomRightLon);

            // Assert
            Assert.Equal(5, precision);
        }

        [Fact]
        public void DetermineGeohashPrecision_WhenDiffBetweenPointOneAndOne_ReturnsNeighborhoodLevel()
        {
            // Arrange
            const double topLeftLat = 52.55;
            const double bottomRightLat = 52.25; // latDiff = 0.3
            const double topLeftLon = 13.35;
            const double bottomRightLon = 13.65; // lonDiff = 0.3

            // Act
            int precision = CommonUtilities.DetermineGeohashPrecision(
                topLeftLat, bottomRightLat, topLeftLon, bottomRightLon);

            // Assert
            Assert.Equal(6, precision);
        }

        [Fact]
        public void DetermineGeohashPrecision_WhenDiffLessThanPointOne_ReturnsStreetLevel()
        {
            // Arrange
            const double topLeftLat = 52.521;
            const double bottomRightLat = 52.515; // latDiff = 0.006
            const double topLeftLon = 13.404;
            const double bottomRightLon = 13.412; // lonDiff = 0.008

            // Act
            int precision = CommonUtilities.DetermineGeohashPrecision(
                topLeftLat, bottomRightLat, topLeftLon, bottomRightLon);

            // Assert
            Assert.Equal(7, precision);
        }

        [Fact]
        public void DetermineGeohashPrecision_WhenCoordsAreReversed_UsesAbsoluteDifference()
        {
            // Arrange — bottom-right lat is greater than top-left (reversed), diff still 30
            const double topLeftLat = 30.0;
            const double bottomRightLat = 60.0;
            const double topLeftLon = 15.0;
            const double bottomRightLon = 10.0;

            // Act
            int precision = CommonUtilities.DetermineGeohashPrecision(
                topLeftLat, bottomRightLat, topLeftLon, bottomRightLon);

            // Assert
            Assert.Equal(3, precision);
        }
    }

    // -------------------------------------------------------------------------
    // Map Tests
    // -------------------------------------------------------------------------
    public class MapTests
    {
        [Fact]
        public void Map_WhenValueIsAtSourceMinimum_ReturnsTargetMinimum()
        {
            // Arrange
            const decimal value = 0m;
            const decimal fromSource = 0m;
            const decimal toSource = 100m;
            const decimal fromTarget = 0m;
            const decimal toTarget = 1m;

            // Act
            decimal result = value.Map(fromSource, toSource, fromTarget, toTarget);

            // Assert
            Assert.Equal(0m, result);
        }

        [Fact]
        public void Map_WhenValueIsAtSourceMaximum_ReturnsTargetMaximum()
        {
            // Arrange
            const decimal value = 100m;
            const decimal fromSource = 0m;
            const decimal toSource = 100m;
            const decimal fromTarget = 0m;
            const decimal toTarget = 1m;

            // Act
            decimal result = value.Map(fromSource, toSource, fromTarget, toTarget);

            // Assert
            Assert.Equal(1m, result);
        }

        [Fact]
        public void Map_WhenValueIsAtSourceMidpoint_ReturnsTargetMidpoint()
        {
            // Arrange
            const decimal value = 50m;
            const decimal fromSource = 0m;
            const decimal toSource = 100m;
            const decimal fromTarget = 0m;
            const decimal toTarget = 1m;

            // Act
            decimal result = value.Map(fromSource, toSource, fromTarget, toTarget);

            // Assert
            Assert.Equal(0.5m, result);
        }

        [Fact]
        public void Map_WhenSourceAndTargetRangesDiffer_MapsProportionally()
        {
            // Arrange
            const decimal value = 5m;
            const decimal fromSource = 0m;
            const decimal toSource = 10m;
            const decimal fromTarget = 100m;
            const decimal toTarget = 200m;

            // Act
            decimal result = value.Map(fromSource, toSource, fromTarget, toTarget);

            // Assert
            Assert.Equal(150m, result);
        }

        [Fact]
        public void Map_WhenValueIsNegative_MapsCorrectly()
        {
            // Arrange
            const decimal value = -5m;
            const decimal fromSource = -10m;
            const decimal toSource = 10m;
            const decimal fromTarget = 0m;
            const decimal toTarget = 100m;

            // Act
            decimal result = value.Map(fromSource, toSource, fromTarget, toTarget);

            // Assert
            Assert.Equal(25m, result);
        }

        [Fact]
        public void Map_WhenTargetRangeIsInverted_MapsCorrectly()
        {
            // Arrange
            const decimal value = 25m;
            const decimal fromSource = 0m;
            const decimal toSource = 100m;
            const decimal fromTarget = 100m;
            const decimal toTarget = 0m;

            // Act
            decimal result = value.Map(fromSource, toSource, fromTarget, toTarget);

            // Assert
            Assert.Equal(75m, result);
        }

        [Fact]
        public void Map_WhenValueExceedsSourceRange_ExtrapolatesCorrectly()
        {
            // Arrange
            const decimal value = 150m;
            const decimal fromSource = 0m;
            const decimal toSource = 100m;
            const decimal fromTarget = 0m;
            const decimal toTarget = 10m;

            // Act
            decimal result = value.Map(fromSource, toSource, fromTarget, toTarget);

            // Assert
            Assert.Equal(15m, result);
        }
    }

    // -------------------------------------------------------------------------
    // IsInRange Tests
    // -------------------------------------------------------------------------
    public class IsInRangeTests
    {
        [Fact]
        public void IsInRange_WhenInnerRangeIsCompletelyInsideOuter_ReturnsTrue()
        {
            // Arrange
            const int outerStart = 1;
            const int outerEnd = 10;
            const int innerStart = 3;
            const int innerEnd = 7;

            // Act
            bool result = CommonUtilities.IsInRange(outerStart, outerEnd, innerStart, innerEnd);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsInRange_WhenInnerRangeMatchesOuterExactly_InclusiveReturnsTrue()
        {
            // Arrange
            const int outerStart = 1;
            const int outerEnd = 10;
            const int innerStart = 1;
            const int innerEnd = 10;

            // Act
            bool result = CommonUtilities.IsInRange(outerStart, outerEnd, innerStart, innerEnd, isInclusive: true);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsInRange_WhenInnerRangeMatchesOuterExactly_ExclusiveReturnsFalse()
        {
            // Arrange
            const int outerStart = 1;
            const int outerEnd = 10;
            const int innerStart = 1;
            const int innerEnd = 10;

            // Act
            bool result = CommonUtilities.IsInRange(outerStart, outerEnd, innerStart, innerEnd, isInclusive: false);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsInRange_WhenInnerStartIsBeforeOuterStart_ReturnsFalse()
        {
            // Arrange
            const int outerStart = 5;
            const int outerEnd = 10;
            const int innerStart = 3;
            const int innerEnd = 8;

            // Act
            bool result = CommonUtilities.IsInRange(outerStart, outerEnd, innerStart, innerEnd);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsInRange_WhenInnerEndIsAfterOuterEnd_ReturnsFalse()
        {
            // Arrange
            const int outerStart = 1;
            const int outerEnd = 8;
            const int innerStart = 3;
            const int innerEnd = 10;

            // Act
            bool result = CommonUtilities.IsInRange(outerStart, outerEnd, innerStart, innerEnd);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsInRange_WhenInnerStartEqualsOuterStart_InclusiveReturnsTrue()
        {
            // Arrange
            const int outerStart = 1;
            const int outerEnd = 10;
            const int innerStart = 1;
            const int innerEnd = 5;

            // Act
            bool result = CommonUtilities.IsInRange(outerStart, outerEnd, innerStart, innerEnd, isInclusive: true);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsInRange_WhenInnerStartEqualsOuterStart_ExclusiveReturnsFalse()
        {
            // Arrange
            const int outerStart = 1;
            const int outerEnd = 10;
            const int innerStart = 1;
            const int innerEnd = 5;

            // Act
            bool result = CommonUtilities.IsInRange(outerStart, outerEnd, innerStart, innerEnd, isInclusive: false);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsInRange_WhenInnerEndEqualsOuterEnd_InclusiveReturnsTrue()
        {
            // Arrange
            const int outerStart = 1;
            const int outerEnd = 10;
            const int innerStart = 5;
            const int innerEnd = 10;

            // Act
            bool result = CommonUtilities.IsInRange(outerStart, outerEnd, innerStart, innerEnd, isInclusive: true);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsInRange_WhenInnerEndEqualsOuterEnd_ExclusiveReturnsFalse()
        {
            // Arrange
            const int outerStart = 1;
            const int outerEnd = 10;
            const int innerStart = 5;
            const int innerEnd = 10;

            // Act
            bool result = CommonUtilities.IsInRange(outerStart, outerEnd, innerStart, innerEnd, isInclusive: false);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsInRange_WhenUsedWithDateTimeType_WorksCorrectly()
        {
            // Arrange
            DateTime outerStart = new(2024, 1, 1);
            DateTime outerEnd = new(2024, 12, 31);
            DateTime innerStart = new(2024, 3, 1);
            DateTime innerEnd = new(2024, 9, 30);

            // Act
            bool result = CommonUtilities.IsInRange(outerStart, outerEnd, innerStart, innerEnd);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsInRange_DefaultIsInclusive_ReturnsTrue()
        {
            // Arrange — default isInclusive = true, boundaries touching outer
            const int outerStart = 0;
            const int outerEnd = 100;
            const int innerStart = 0;
            const int innerEnd = 100;

            // Act
            bool result = CommonUtilities.IsInRange(outerStart, outerEnd, innerStart, innerEnd);

            // Assert
            Assert.True(result);
        }
    }
}
