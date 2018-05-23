using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using WebApplication1;
using Xunit;

namespace XUnitTestProject1
{
    public class UnitTest1
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public UnitTest1()
        {
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        [Fact]
        public async Task ReturnsValue()
        {
            var response = await _client.GetAsync("/api/values/1");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Equal("value", responseString);
        }

        [Fact]
        public async Task ReturnsValues()
        {
            var response = await _client.GetAsync("/api/values");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var responseArray = JsonConvert.DeserializeObject<string[]>(responseString);

            Assert.Equal(2, responseArray.Length);
        }
    }
}
