using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using BlazorCrud.Shared.Models;
using Newtonsoft.Json;
using System.Threading.Tasks;

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
