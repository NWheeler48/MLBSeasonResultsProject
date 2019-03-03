using System.Collections.Generic;

namespace MLBSeasonResults.Helpers
{
    /// <summary>
    /// Class: GroupedByDivisionList
    /// Purpose: Creates a list with a Key associated with it to allow a collection view source to know the name of the group key to display it.
    /// </summary>
    public class GroupedByDivisionList : List<object>
    {
        public object Division { get; set; }
    }
}
