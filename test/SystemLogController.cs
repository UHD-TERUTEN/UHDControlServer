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
    public class SystemLogControllerTest
    {
        public SystemLogControllerTest(ITestOutputHelper output)
        {
            this.output = output;
            server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            client = server.CreateClient();
            caseInsensitive = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
        }

        [Fact]
        public async Task GetPage()
        {
            using (var response = await client.GetAsync($"{baseUrl}/system-log?page=1"))
            {
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                var responseString = await response.Content.ReadAsStringAsync();
                var deserialized = JsonSerializer.Deserialize<List<SystemLog>>(responseString, caseInsensitive);

                Assert.NotNull(deserialized);
            }

            using (var response = await client.GetAsync($"{baseUrl}/system-log?page=3"))
            {
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                var responseString = await response.Content.ReadAsStringAsync();
                var deserialized = JsonSerializer.Deserialize<List<SystemLog>>(responseString, caseInsensitive);

                Assert.NotNull(deserialized);
            }

            using (var response = await client.GetAsync($"{baseUrl}/system-log?page=0"))
            {
                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            }

            using (var response = await client.GetAsync($"{baseUrl}/system-log?page=-1"))
            {
                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            }

            using (var response = await client.GetAsync($"{baseUrl}/system-log?page=CREATE TABLE t1(a, b PRIMARY KEY);"))
            {
                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            }
        }

        [Fact]
        public async Task GetOne()
        {
            using (var response = await client.GetAsync($"{baseUrl}/system-log/1"))
            {
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                var responseString = await response.Content.ReadAsStringAsync();
                var deserialized = JsonSerializer.Deserialize<SystemLog>(responseString, caseInsensitive);

                Assert.Equal(1, deserialized.Id);
            }

            using (var response = await client.GetAsync($"{baseUrl}/system-log/3"))
            {
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                var responseString = await response.Content.ReadAsStringAsync();
                var deserialized = JsonSerializer.Deserialize<SystemLog>(responseString, caseInsensitive);

                Assert.Equal(3, deserialized.Id);
            }

            using (var response = await client.GetAsync($"{baseUrl}/system-log/0"))
            {
                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            }

            using (var response = await client.GetAsync($"{baseUrl}/system-log/-1"))
            {
                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            }
        }

        [Fact]
        public async Task Download()
        {
            using (var response = await client.GetAsync($"{baseUrl}/system-log/1_2021-02-10.zip"))
            {
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        private readonly ITestOutputHelper output;

        private readonly TestServer server;

        private readonly HttpClient client;

        private readonly string baseUrl = "http://localhost:50598/api";

        private readonly JsonSerializerOptions caseInsensitive;
    }
}
