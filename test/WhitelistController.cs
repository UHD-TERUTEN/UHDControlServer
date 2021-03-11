using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using UHDControlServer;
using UHDControlServer.Models;
using Xunit;
using Xunit.Abstractions;

namespace test
{
    // https://docs.microsoft.com/ko-kr/dotnet/architecture/microservices/multi-container-microservice-net-applications/test-aspnet-core-services-web-apps
    public class WhitelistControllerTest
    {
        public WhitelistControllerTest(ITestOutputHelper output)
        {
            this.output = output;
            server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            client = server.CreateClient();
            caseInsensitive = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
        }

        [Fact]
        public async Task GetPage()
        {
            using (var response = await client.GetAsync($"{baseUrl}/whitelist?page=1"))
            {
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                var responseString = await response.Content.ReadAsStringAsync();
                var deserialized = JsonSerializer.Deserialize<List<Whitelist>>(responseString, caseInsensitive);

                Assert.NotNull(deserialized);
            }

            using (var response = await client.GetAsync($"{baseUrl}/whitelist?page=3"))
            {
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                var responseString = await response.Content.ReadAsStringAsync();
                var deserialized = JsonSerializer.Deserialize<List<Whitelist>>(responseString, caseInsensitive);

                Assert.NotNull(deserialized);
            }

            using (var response = await client.GetAsync($"{baseUrl}/whitelist?page=0"))
            {
                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            }

            using (var response = await client.GetAsync($"{baseUrl}/whitelist?page=-1"))
            {
                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            }

            using (var response = await client.GetAsync($"{baseUrl}/whitelist?page=echo hello"))
            {
                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            }
        }

        [Fact]
        public async Task GetOneByVersion()
        {
            using (var response = await client.GetAsync($"{baseUrl}/whitelist?version=1.0.1"))
            {
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                var responseString = await response.Content.ReadAsStringAsync();
                var deserialized = JsonSerializer.Deserialize<Whitelist>(responseString, caseInsensitive);

                Assert.Equal("1.0.1", deserialized.Version);
            }

            using (var response = await client.GetAsync($"{baseUrl}/whitelist?version=0.0.0"))
            {
                Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            }

            using (var response = await client.GetAsync($"{baseUrl}/whitelist?version=echo hello"))
            {
                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            }
        }

        [Fact]
        public async Task GetOne()
        {
            using (var response = await client.GetAsync($"{baseUrl}/whitelist/1"))
            {
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                var responseString = await response.Content.ReadAsStringAsync();
                var deserialized = JsonSerializer.Deserialize<Whitelist>(responseString, caseInsensitive);

                Assert.Equal(1, deserialized.Id);
            }

            using (var response = await client.GetAsync($"{baseUrl}/whitelist/3"))
            {
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                var responseString = await response.Content.ReadAsStringAsync();
                var deserialized = JsonSerializer.Deserialize<Whitelist>(responseString, caseInsensitive);

                Assert.Equal(3, deserialized.Id);
            }

            using (var response = await client.GetAsync($"{baseUrl}/whitelist/0"))
            {
                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            }

            using (var response = await client.GetAsync($"{baseUrl}/whitelist/-1"))
            {
                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            }
        }

        [Fact]
        public async Task GetLatest()
        {
            int latestVersion = 0;

            using (var response = await client.GetAsync($"{baseUrl}/whitelist?page=1"))
            {
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                var responseString = await response.Content.ReadAsStringAsync();
                var deserialized = JsonSerializer.Deserialize<List<Whitelist>>(responseString, caseInsensitive);

                Assert.NotNull(deserialized);

                latestVersion = deserialized[deserialized.Count - 1].Id;
            }

            using (var response = await client.GetAsync($"{baseUrl}/whitelist/latest"))
            {
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                var responseString = await response.Content.ReadAsStringAsync();
                var deserialized = JsonSerializer.Deserialize<Whitelist>(responseString, caseInsensitive);

                Assert.Equal(latestVersion, deserialized.Id);
            }
        }

        [Fact]
        public async Task Distribute()
        {
            using (var response = await client.GetAsync($"{baseUrl}/whitelist/distribute?version=1.0.1"))
            {
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }

            using (var response = await client.GetAsync($"{baseUrl}/whitelist/distribute?version=0.0.0"))
            {
                Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            }
        }

        private readonly ITestOutputHelper output;

        private readonly TestServer server;

        private readonly HttpClient client;

        private readonly string baseUrl = "http://localhost:50598/api";

        private readonly JsonSerializerOptions caseInsensitive;
    }
}
