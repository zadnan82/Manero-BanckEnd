using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Manero_BanckEnd.Controllers;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

namespace Manero.Test.ExternalServicesTests
{
    public class ExternalLoginControllerTests : IClassFixture<TestServerFixture>
    {
            private readonly HttpClient _client;

            public ExternalLoginControllerTests(TestServerFixture fixture)
            {
                _client = fixture.Client;
            }

            [Fact] //FredrikSpanien Test
            public async Task ExternalLogin_Returns_Status_With_Facebook_Auth()
            {
                var client = _client;

                
                var response = await client.GetAsync("/api/ExternalLogin?provider=Facebook");
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

            }
         
            [Fact] //FredrikSpanien Test
            public async Task ExternalLoginCallback_Returns_Status_With_Facebook_Auth()
            {
                var client = _client;

                
                var response = await client.GetAsync("/api/ExternalLoginCallback");
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

            }
    }

        public class TestStartup
        {
            public void ConfigureServices(IServiceCollection services)
            {
                services.AddDbContext<DbContext>(options => { options.UseInMemoryDatabase("TestDatabase"); });

                
            }

            public void Configure(IApplicationBuilder app)
            {
                
            }
        }

        public class TestServerFixture : IDisposable
        {
            public TestServer Server { get; }
            public HttpClient Client { get; }

            public TestServerFixture()
            {
                var builder = new WebHostBuilder().UseStartup<TestStartup>();

                Server = new TestServer(builder);
                Client = Server.CreateClient();
            }

            public void Dispose()
            {
                Client.Dispose();
                Server.Dispose();
            }
        }
}






