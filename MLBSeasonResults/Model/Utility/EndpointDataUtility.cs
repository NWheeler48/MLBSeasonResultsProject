using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.Web.Http;



namespace MLBSeasonResults.Model.Utility
{
    /// <summary>
    /// Class: EndpointDataUtility
    /// Purpose: Retrieve data from a specified endpoint and make it avaliable to other classes.
    /// </summary>
    public static class EndpointDataUtility
    {
        /// <summary>
        /// Retrieve data from the specified endpoint.
        /// </summary>
        /// <param name="address">The full address of the endpoint which contains the data</param>
        /// <returns></returns>
        public static async Task<string> GetDataBuffer(string address)
        {

            var uri = new System.Uri(address);

            // Attempt to get the data from the provided enpoint.
            using (var httpClient = new HttpClient())
            {
                try
                {
                    var response = await httpClient.GetAsync(uri);

                    // Check that the response was correctly carried out.
                    if (response.StatusCode == HttpStatusCode.Ok)
                    {
                        // If the call completed sucessfully return the buffer.
                        return await response.Content.ReadAsStringAsync();
                    }
                }
                catch (Exception e)
                {
                    return "";
                }

                return "";
            }
        }
    }
}
