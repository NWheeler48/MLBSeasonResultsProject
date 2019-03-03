using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;

namespace MLBSeasonResults.Model.Utility
{
    /// <summary>
    /// Class: JsonDeserializationUtility
    /// Purpose: Deserializes the json data from a given string, it assumes that the data is in a array and that it is properly formatted.
    /// </summary>
    public static class JsonDeserializationUtility
    {

        /// <summary>
        /// Deserializes the json data and throws an exception to be handled if anything goes wrong.
        /// This method is designed to handle data that can be mapped to the TeamSeaonResults class if it doesn't map it will just return a list of empty
        /// Team season results
        /// </summary>
        /// <param name="data">json data to be deserialized</param>
        /// <returns>
        /// List<TeamSeasonResults> A list of the a teams wins and loses along with there league and division.
        /// </returns>
        public static List<TeamSeasonResults> DeserializeJsonToSeasonResults(string data)
        {
            if (data != null)
            {
                List<string> errors = new List<string>();
                List<TeamSeasonResults> results = JsonConvert.DeserializeObject<List<TeamSeasonResults>>(data,
                        new JsonSerializerSettings
                        {
                            Error = delegate (object sender, ErrorEventArgs args)
                            {
                                errors.Add(args.ErrorContext.Error.Message);
                                args.ErrorContext.Handled = true;
                            }
                        });

                if (errors.Count == 0)
                {
                    return results;
                }
                else
                {
                    // If there was a problem parsing the json throw a exception with the json exception values
                    // to be handled.
                    string errorMsg = "JsonConvert.DeserializeObject threw the follwing exceptions";
                    errorMsg += string.Join("\n", errors.ToArray());
                    throw new Exception(errorMsg);
                }
            }
            else
            {
                // If the data is null throw an exception that will be handled in the caller.
                throw new ArgumentNullException("The parameter data as unexpectedly null.");
            }
            
        }

    }
}
