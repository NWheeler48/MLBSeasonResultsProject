using MLBSeasonResults.Model.Utility;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MLBSeasonResults.Model
{
    /// <summary>
    /// Class: SeasonResultsDataModel
    /// Purpose: To get the data from a static address.
    /// </summary>
    public class SeasonResultsDataModel
    {
        #region Fields
        // A list of team results that is deserialized from the json gathered from the server.
        private List<TeamSeasonResults> _teamsResults = null;

        // The address of the server containing the data.
        private const string _address = "https://api.mobileqa.mlbinfra.com/api/interview/v1/records";
        #endregion

        #region Constructors
        public SeasonResultsDataModel()
        {
            _teamsResults = new List<TeamSeasonResults>();
        }
        #endregion

        #region Public Methods
        public List<TeamSeasonResults> GetTeamsSeasonResultsList()
        {
            return _teamsResults;
        }

        /// <summary>
        /// Initializes the Data retrieved from the specified address and stores it in a list.
        /// </summary>
        /// <returns>
        /// True: If the initialization completed successfully.
        /// False: If the initialization failed.
        /// </returns>
        public async Task<bool> Initialize()
        {
            try
            {
                string data = await HttpDataUtility.GetDataFromHttpServer(_address);
                if (data != null && data.Length > 0)
                {
                    _teamsResults = JsonDeserializationUtility.DeserializeJsonToSeasonResults(data);
                    if (_teamsResults != null && _teamsResults.Count > 0)
                    {
                        return true;
                    }
                }
            }
            catch
            {
                // Since there is no real way to handle not getting the data return false.
                return false;
            }

            // If there is no exception but the team results comes back null or empty return false.
            return false;
        }
        #endregion
    }
}
