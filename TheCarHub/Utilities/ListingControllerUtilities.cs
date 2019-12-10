using System;
using System.Collections.Generic;

namespace TheCarHub.Utilities
{
    public class ListingControllerUtilities
    {
        /// <summary>
        /// Utility class for car year SelectList creation.
        /// </summary>
        public class YearSelectItem
        {
            public int Value { get; set; }
            public string Text { get; set; }
        }

        /// <summary>
        /// Utility class for Status SelectList creation.
        /// </summary>
        public class StatusSelectItem
        {
            public int Value { get; set; }
            public string Text { get; set; }
        }

        /// <summary>
        /// Utility method that populates a list with YearSelectItems
        /// representing years ranging from 1990 to current year + 1.
        /// </summary>
        /// <returns>A List of YearSelectItem containing entries from 1990 to current year +1, inclusive.</returns>
        public static IEnumerable<YearSelectItem> PopulateCarYearSelect()
        {
            var years = new List<YearSelectItem>();

            for (int i = 1990; i <= (DateTime.Today.Year + 1); i++)
            {
                years.Add(new YearSelectItem
                {
                    Value = i,
                    Text = i.ToString()
                });
            }

            return years;
        }

        /// <summary>
        /// Utility method that populates a list with StatusSelectItems representing
        /// all status options present in database.
        /// </summary>
        /// <returns>A List of YearSelectItem containing entries of all current status options.</returns>
        public static IEnumerable<StatusSelectItem> PopulateStatusSelect()
        {

            var statusSelectItems = new List<StatusSelectItem>
            {
                new StatusSelectItem { Value = 1, Text = "Available" },
                new StatusSelectItem { Value = 2, Text = "Sold" }
                
            };

            return statusSelectItems;
        }
    }
}