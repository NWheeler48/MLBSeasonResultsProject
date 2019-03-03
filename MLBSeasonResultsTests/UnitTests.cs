using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MLBSeasonResults.Model.Utility;

namespace MLBSeasonResultsTests
{
    [TestClass]
    public class HttpDataUtility_Tests
    {
        [TestMethod]
        public async Task Get_Good_Address()
        {
            // Good address name.
            var buffer = await HttpDataUtility.GetDataFromHttpServer("https://api.mobileqa.mlbinfra.com/api/interview/v1/records");
            Assert.AreNotEqual(buffer.Length, 0);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task Get_Null_Address()
        {
            // Null address this should fail.
            var buffer = await HttpDataUtility.GetDataFromHttpServer(null);
        }

        [TestMethod]
        [ExpectedException(typeof (InvalidOperationException))]
        public async Task Utility_Throws_InvalidOperationException()
        {
            var buffer = await HttpDataUtility.GetDataFromHttpServer("htpps://fake");
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

            // This is not a great test I am only doing it this way so I do not have to overload the Equals and GetHash method for TeamSeasonResults.
            Assert.AreEqual(expected.Count, results.Count);
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
