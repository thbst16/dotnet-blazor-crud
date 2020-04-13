using BlazorCrud.Shared.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorCrud.Tests.API
{
    [TestClass]
    public class APITest
    {
        private TestContext testContextInstance;
        static HttpClient client = new HttpClient();
        // private const string ENV_URL = "https://localhost:44377/api";
        private const string ENV_URL = "https://becksblazor.azurewebsites.net/api";

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
        public async Task SearchPatientsReturnsExpectedCountAndResults()
        {
            List<Patient> patients = null;
            var requestUri = ENV_URL + "/patient?name=br&page=1";
            HttpResponseMessage response = await client.GetAsync(requestUri);
            var responseData = response.Content.ReadAsStringAsync();
            patients = JObject.Parse(responseData.Result).SelectToken("results").ToObject<List<Patient>>();
            Assert.AreEqual(9, patients.Count);
            Assert.AreEqual("Broderick Shields", patients[0].Name);
            Assert.AreEqual("Female", patients[0].Gender);
            Assert.AreEqual("Mertz - Hessel", patients[0].PrimaryCareProvider);
            Assert.AreEqual("Michigan", patients[0].State);
        }

        [TestMethod()]
        public async Task SearchOrganizationsReturnsExpectedCountAndResults()
        {
            List<Organization> organizations = null;
            var requestUri = ENV_URL + "/organization?name=wa&page=1";
            HttpResponseMessage response = await client.GetAsync(requestUri);
            var responseData = response.Content.ReadAsStringAsync();
            organizations = JObject.Parse(responseData.Result).SelectToken("results").ToObject<List<Organization>>();
            Assert.AreEqual(1, organizations.Count);
            Assert.AreEqual("Swaniawski, Collier and Hauck", organizations[0].Name);
            Assert.AreEqual("Government", organizations[0].Type);
        }

        [TestMethod()]
        public async Task SearchClaimsReturnsExpectedCountAndResults()
        {
            List<Claim> claims = null;
            var requestUri = ENV_URL + "/Claim?name=ist&page=1";
            HttpResponseMessage response = await client.GetAsync(requestUri);
            var responseData = response.Content.ReadAsStringAsync();
            claims = JObject.Parse(responseData.Result).SelectToken("results").ToObject<List<Claim>>();
            Assert.AreEqual(10, claims.Count);
            Assert.AreEqual("Donnelly, Bosco and Schumm", claims[0].Organization);
            Assert.AreEqual("Libby Rath", claims[1].Patient);
        }

        [TestMethod()]
        public async Task GetClaimReturnsExpectedClaimDetails()
        {
            Claim claim = null;
            var requestUri = ENV_URL + "/Claim/1";
            HttpResponseMessage response = await client.GetAsync(requestUri);
            var responseData = response.Content.ReadAsStringAsync();
            claim = JsonConvert.DeserializeObject<Claim>(responseData.Result);
            Assert.AreEqual("Royce Paucek", claim.Patient);
            Assert.AreEqual("Bayer LLC", claim.Organization);
            Assert.AreEqual("Active", claim.Status);
            Assert.AreEqual("Oral", claim.Type);
        }

        [TestCleanup()]
        public void CleanupTest() { }
    }
}
