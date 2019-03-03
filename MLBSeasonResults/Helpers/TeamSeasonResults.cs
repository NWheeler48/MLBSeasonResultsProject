
namespace MLBSeasonResults.Model.Utility
{
    /// <summary>
    /// A data class that holds the values from the json or whatever data source is used.
    /// </summary>
    public class TeamSeasonResults
    {
        #region Properties
        public string team { get; set; }
        public string wins { get; set; }
        public string losses { get; set; }
        public string league { get; set; }
        public string division { get; set; }
        #endregion

        #region Constructors
        public TeamSeasonResults()
        {
            // Give everything an initial value so non are null.
            team = "";
            wins = "";
            losses = "";
            league = "";
            division = "";
        }
        #endregion
    }
}
