using System;
using Xunit;
using System.Net.Http;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using ATAPWebSitesATAPConsulting;
using System.Threading.Tasks;

namespace ATAPConsulting.IntegrationTests
{
    public class IntegrationTests
  {
    private readonly TestServer _server;
    private readonly HttpClient _client;

    public IntegrationTests()
    {
      _server = new TestServer(WebHost.CreateDefaultBuilder()
          .UseStartup<Startup>()
          .UseEnvironment("Development"));
      _client = _server.CreateClient();
    }

    public void Dispose()
    {
      _client.Dispose();
      _server.Dispose();
    }

    [Fact]
    public async Task Index_Get_ReturnsIndexHtmlPage()
    {
      // Act
      var response = await _client.GetAsync("/");

      // Assert
      response.EnsureSuccessStatusCode();
      var responseString = await response.Content.ReadAsStringAsync();
      Assert.Contains("<title>Home Page - ATAP Technology, Inc.</title>", responseString);
    }
  }
}
