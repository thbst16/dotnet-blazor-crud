using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Blazorcrud.Server.Authorization;
using Blazorcrud.Shared.Models;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Blazorcrud.Server.IntegrationTests
{
    public static class Utilities
    {
        public static async Task<string> GetJwtAsync(HttpClient client)
        {
            var login = new AuthenticateRequest
            {
                Username = "admin",
                Password = "admin"
            };
            var response = await client.PostAsJsonAsync("/api/user/authenticate", login);
            response.EnsureSuccessStatusCode();
            var authResponse = await response.Content.ReadFromJsonAsync<AuthenticateResponse>();
            return authResponse.Token;
        }
    }
}
