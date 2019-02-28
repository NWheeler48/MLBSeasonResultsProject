
using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MLBSeasonResults.Model.Utility;
using Windows.Storage.Streams;

namespace MLBSeasonResultsTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task Get_Good_Endpoint()
        {
            // Good endpoint name.
            var buffer = await EndpointDataUtility.GetDataBuffer("https://api.mobileqa.mlbinfra.com/api/interview/v1/records");
            Assert.AreNotEqual(buffer.Length, 0);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException),
            "A null enpoint was inappropriately provided.")]
        public async Task Get_Null_Endpoint()
        {
            // Null endpoint this should fail.
            var buffer = await EndpointDataUtility.GetDataBuffer(null);
        }
    }
}
