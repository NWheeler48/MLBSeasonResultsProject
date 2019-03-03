using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLBSeasonResults.Model.Utility
{
    public static class JsonDeserializationUtility
    {

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
                    string errorMsg = "JsonConvert.DeserializeObject threw the follwing exceptions";
                    errorMsg += string.Join("\n", errors.ToArray());
                    throw new Exception(errorMsg);
                }
            }
            else
            {
                throw new ArgumentNullException("The parameter data as unexpectedly null.");
            }
            
        }

    }
}
