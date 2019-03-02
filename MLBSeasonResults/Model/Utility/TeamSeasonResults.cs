using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLBSeasonResults.Model.Utility
{
    /// <summary>
    /// A data class that holds the values from the endpoint.
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
            // For now do nothing.
        }
        #endregion
    }
}
