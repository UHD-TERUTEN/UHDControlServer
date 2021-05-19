using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using UHDControlServer;
using UHDControlServer.Models;
using Xunit;
using Xunit.Abstractions;

namespace test
{
    public class FileAccessRejectLogControllerTest
    {
        public FileAccessRejectLogControllerTest(ITestOutputHelper output)
        {
            this.output = output;
            server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            client = server.CreateClient();
            caseInsensitive = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
        }

        [Fact]
        public async Task GetPage()
        {
            using (var response = await client.GetAsync($"{baseUrl}/file-access-reject-log?page=1"))
            {
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                var responseString = await response.Content.ReadAsStringAsync();
                var deserialized = JsonSerializer.Deserialize<List<FileAccessRejectLog>>(responseString, caseInsensitive);

                Assert.NotNull(deserialized);
            }

            using (var response = await client.GetAsync($"{baseUrl}/file-access-reject-log?page=3"))
            {
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                var responseString = await response.Content.ReadAsStringAsync();
                var deserialized = JsonSerializer.Deserialize<List<FileAccessRejectLog>>(responseString, caseInsensitive);

                Assert.NotNull(deserialized);
            }

            using (var response = await client.GetAsync($"{baseUrl}/file-access-reject-log?page=0"))
            {
                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            }

            using (var response = await client.GetAsync($"{baseUrl}/file-access-reject-log?page=-1"))
            {
                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            }

            using (var response = await client.GetAsync($"{baseUrl}/file-access-reject-log?page=CREATE TABLE t1(a, b PRIMARY KEY)"))
            {
                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            }
        }

        [Fact]
        public async Task GetOne()
        {
            using (var response = await client.GetAsync($"{baseUrl}/file-access-reject-log/1"))
            {
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                var responseString = await response.Content.ReadAsStringAsync();
                var deserialized = JsonSerializer.Deserialize<FileAccessRejectLog>(responseString, caseInsensitive);

                Assert.Equal(1, deserialized.Id);
            }

            using (var response = await client.GetAsync($"{baseUrl}/file-access-reject-log/3"))
            {
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                var responseString = await response.Content.ReadAsStringAsync();
                var deserialized = JsonSerializer.Deserialize<FileAccessRejectLog>(responseString, caseInsensitive);

                Assert.Equal(3, deserialized.Id);
            }

            using (var response = await client.GetAsync($"{baseUrl}/file-access-reject-log/0"))
            {
                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            }

            using (var response = await client.GetAsync($"{baseUrl}/file-access-reject-log/-1"))
            {
                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            }
        }

        [Fact]
        public async Task PutOne()
        {
            FileAccessRejectLog fileAccessRejectLog = null;

            using (var response = await client.GetAsync($"{baseUrl}/file-access-reject-log/1"))
            {
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                var responseString = await response.Content.ReadAsStringAsync();
                var deserialized = JsonSerializer.Deserialize<FileAccessRejectLog>(responseString, caseInsensitive);

                Assert.Equal(1, deserialized.Id);

                fileAccessRejectLog = deserialized;
            }

            using (var response = await client.PutAsync($"{baseUrl}/file-access-reject-log", JsonContent.Create(fileAccessRejectLog)))
            {
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                var responseString = await response.Content.ReadAsStringAsync();
                var deserialized = JsonSerializer.Deserialize<FileAccessRejectLog>(responseString, caseInsensitive);

                Assert.Equal(1, deserialized.Id);
            }
        }

        private readonly ITestOutputHelper output;

        private readonly TestServer server;

        private readonly HttpClient client;

        private readonly string baseUrl = "http://localhost:50598/api";

        private readonly JsonSerializerOptions caseInsensitive;
    }
}
