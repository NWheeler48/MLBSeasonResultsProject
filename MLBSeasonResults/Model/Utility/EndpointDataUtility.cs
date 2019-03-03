using System;
using System.Threading.Tasks;
using Windows.Web.Http;



namespace MLBSeasonResults.Model.Utility
{
    /// <summary>
    /// Class: HttpDataUtility
    /// Purpose: Retrieve data from a specified address and make it avaliable to other classes.
    /// </summary>
    public static class HttpDataUtility
    {
        /// <summary>
        /// Retrieve data from the specified address.
        /// </summary>
        /// <param name="address">The full address of the server which contains the data</param>
        /// <returns>
        /// Task<string> A string containing the data from the provided address.
        /// </returns>
        public static async Task<string> GetDataFromHttpServer(string address)
        {
            // Attempt to get the data from the provided address.
            using (var httpClient = new HttpClient())
            {
                try
                {
                    var uri = new System.Uri(address);

                    var response = await httpClient.GetAsync(uri);

                    // Check that the response was correctly carried out.
                    if (response.StatusCode == HttpStatusCode.Ok)
                    {
                        // If the call completed sucessfully return the buffer.
                        return await response.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        throw new Exception(string.Format("Http request failed with status code: {0}", response.StatusCode.ToString()));
                    }
                }
                catch (Exception e)
                {
                    // Since this app has no real way to recover from a exception while getting the data from an server
                    // it just throws an exception and lets the caller handle it.
                    throw e;
                }
            }
        }
    }
}
