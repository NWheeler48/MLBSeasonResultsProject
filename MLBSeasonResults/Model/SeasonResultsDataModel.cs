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
                string data = await GetDataFromHttpEndpoint(_endpoint);
                if (data != null && data.Length > 0)
                {
                    List<string> errors = new List<string>();
                    _teamsResults = JsonConvert.DeserializeObject<List<TeamSeasonResults>>(@data,
                    new JsonSerializerSettings
                    {
                        Error = delegate (object sender, ErrorEventArgs args)
                        {
                            errors.Add(args.ErrorContext.Error.Message);
                            args.ErrorContext.Handled = true;
                        }
                    });
                    if (_teamsResults != null && _teamsResults.Count > 0)
                    {
                        _isInitialized = true;
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }

            return false;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Retrieves the data from the given endpoint and returns it as a string.
        /// </summary>
        /// <param name="address">the http address of the given endpoint</param>
        /// <returns>
        /// string: A string containing the data that was retrieved from the given endpoint.
        /// </returns>
        private async Task<string> GetDataFromHttpEndpoint(string address)
        {
            if (address != null)
            {
                using (var httpClient = new HttpClient())
                {
                    try
                    {
                        var uri = new System.Uri(_endpoint);

                        var response = await httpClient.GetAsync(uri);

                        if (response.StatusCode == HttpStatusCode.Ok)
                        {
                            return await response.Content.ReadAsStringAsync();
                        }
                        else
                        {
                            throw new Exception(string.Format("Failed to retrieve data from endpoint with http status code: {0}", response.StatusCode.ToString()));
                        }
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
            }
            else
            {
                throw new ArgumentNullException("The address for the endpoint was unexpectedly null");
            }
        }
        #endregion
    }
}
