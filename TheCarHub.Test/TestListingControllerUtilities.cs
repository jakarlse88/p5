using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using TheCarHub.Utilities;

namespace TheCarHub.Test
{
    public class TestListingControllerUtilities
    {
        [Fact]
        public void TestPopulateCarYearSelect()
        {
            // Arrange

            // Act
            var result = ListingControllerUtilities.PopulateCarYearSelect();

            // Assert
            Assert.NotNull(result);
            
            var enumerable = result.ToList();
            Assert.NotEmpty(enumerable);
            Assert.IsAssignableFrom<IEnumerable<ListingControllerUtilities.YearSelectItem>>(result);
            Assert.Equal(1990, enumerable.First().Value);
            Assert.Equal(DateTime.Today.Year + 1, enumerable.Last().Value);
        }

        [Fact]
        public void TestPopulateStatusSelect()
        {
            // Arrange

            // Act
            var result = ListingControllerUtilities.PopulateStatusSelect();

            // Assert
            Assert.NotNull(result);

            var enumerable = result.ToList();
            Assert.NotEmpty(enumerable);
            Assert.IsAssignableFrom<IEnumerable<ListingControllerUtilities.StatusSelectItem>>(result);
            Assert.Equal("Available", enumerable.First().Text);
            Assert.Equal("Sold", enumerable.Last().Text);
        }

    }
}