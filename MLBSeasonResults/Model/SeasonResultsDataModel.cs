using MLBSeasonResults.Model.Utility;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using System;
using Newtonsoft.Json;
using Windows.Web.Http;
using Newtonsoft.Json.Serialization;

namespace MLBSeasonResults.Model
{
    public class SeasonResultsDataModel
    {
        #region Fields
        // A list of team results that is deserialized from the json gathered from the endpoint.
        private List<TeamSeasonResults> _teamsResults = null;

        // For now keep the endpoint in the application. I may create a build variable for that later.
        private const string _endpoint = "https://api.mobileqa.mlbinfra.com/api/interview/v1/records";
        #endregion

        #region Properties
        // A get only flag to determine that this model has been initialized.
        private bool _isInitialized = false;
        public bool IsInitialized
        {
            get { return _isInitialized; }
        }
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
        /// Initializes the Data retrieved from the endpoint and stores it in a list.
        /// </summary>
        /// <returns>
        /// True: If the initialization completed successfully.
        /// False: If the initialization failed.
        /// </returns>
        public async Task<bool> Initialize()
        {
            try
            {
                string data = await EndpointDataUtility.GetDataFromHttpEndpoint(_endpoint);
                if (data != null && data.Length > 0)
                {
                    _teamsResults = JsonDeserializationUtility.DeserializeJsonToSeasonResults(data);
                    if (_teamsResults != null && _teamsResults.Count > 0)
                    {
                        _isInitialized = true;
                        return true;
                    }
                }
            }
            catch
            {
                // Since there is no real way to handle not getting the data return false.
                return false;
            }

            return false;
        }
        #endregion
    }
}
