using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MLBSeasonResults.Model.Utility;

namespace MLBSeasonResultsTests
{
    [TestClass]
    public class EndpointDataUtility_Tests
    {
        [TestMethod]
        public async Task Get_Good_Endpoint()
        {
            // Good endpoint name.
            var buffer = await EndpointDataUtility.GetDataFromHttpEndpoint("https://api.mobileqa.mlbinfra.com/api/interview/v1/records");
            Assert.AreNotEqual(buffer.Length, 0);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task Get_Null_Endpoint()
        {
            // Null endpoint this should fail.
            var buffer = await EndpointDataUtility.GetDataFromHttpEndpoint(null);
        }

        [TestMethod]
        [ExpectedException(typeof (InvalidOperationException))]
        public async Task Utility_Throws_InvalidOperationException()
        {
            var buffer = await EndpointDataUtility.GetDataFromHttpEndpoint("htpps://fake");
        }

    }

    [TestClass]
    public class JsonDeserializationUtility_Tests
    {
        [TestMethod]
        public void Deserialize_Good_Json()
        {

            List<TeamSeasonResults> expected = new List<TeamSeasonResults>()
            {
                new TeamSeasonResults
                {
                    team = "KCR",
                    wins = "58",
                    losses = "104",
                    league = "AL",
                    division = "Central"
                },
                new TeamSeasonResults
                {
                    team = "PIT",
                    wins = "75",
                    losses = "111",
                    league = "NL",
                    division = "East"
                }
            };
            string json = "[{\"team\": \"KCR\",\"wins\": 58,\"losses\": 104,\"league\": \"AL\",\"division\": \"Central\"}, {\"team\": \"PIT\",\"wins\": 75,\"losses\": 111,\"league\": \"NL\",\"division\": \"East\"}]";
            List<TeamSeasonResults> results = JsonDeserializationUtility.DeserializeJsonToSeasonResults(json);

            Assert.AreSame(expected[0], results[0]);            
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Deserialize_Null()
        {
            List<TeamSeasonResults> results = JsonDeserializationUtility.DeserializeJsonToSeasonResults(null);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Deserialize_Bad_Json()
        {
            List<TeamSeasonResults> results = JsonDeserializationUtility.DeserializeJsonToSeasonResults("[This, :, is] }{ Bad Json...");
        }
    }
}
