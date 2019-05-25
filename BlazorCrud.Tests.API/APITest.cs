using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using BlazorCrud.Shared.Models;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace BlazorCrud.Tests.API
{
    [TestClass]
    public class APITest
    {
        private TestContext testContextInstance;
        static HttpClient client = new HttpClient();

        public APITest() { }

        // Gets or sets the test context, which provides information about the current test run
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        [TestInitialize()]
        public void SetupTest() { }

        [TestMethod()]
        public async Task SearchMemberReturnsExpectedCount()
        {
            List<Patient> patients = null;
            var requestUri = "https://becksapi.azurewebsites.net/api/patient?name=br&page=1";
            HttpResponseMessage response = await client.GetAsync(requestUri);
            var responseData = response.Content.ReadAsStringAsync();
            patients = JObject.Parse(responseData.Result).SelectToken("results").ToObject<List<Patient>>();
            Assert.AreEqual(8, patients.Count);
            Assert.AreEqual("Bradly Legros", patients[0].Name);
            Assert.AreEqual("Male", patients[0].Gender);
            Assert.AreEqual("Sporer - Schiller", patients[0].PrimaryCareProvider);
            Assert.AreEqual("IL", patients[0].State);
        }

        [TestMethod()]
        public async Task GetClaimReturnsExpectedClaimDetails()
        {
            Claim claim = null;
            var requestUri = "https://becksapi.azurewebsites.net/api/Claim/1";
            HttpResponseMessage response = await client.GetAsync(requestUri);
            var responseData = response.Content.ReadAsStringAsync();
            claim = JsonConvert.DeserializeObject<Claim>(responseData.Result);
            Assert.AreEqual("Lucio Watsica", claim.Patient);
            Assert.AreEqual("McKenzie - Hettinger", claim.Organization);
            Assert.AreEqual("Draft", claim.Status);
            Assert.AreEqual("Vision", claim.Type);
        }

        [TestCleanup()]
        public void CleanupTest() { }
    }
}
