using Manero_BanckEnd.Contexts;
using Manero_BanckEnd.Repositories;
using Manero_BanckEnd.Services;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Exchange.WebServices.Data;

namespace Manero_BanckEnd.Helpers;

public class WebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.AddDbContext<DataContext>(x => x.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()));
            services.AddScoped<UserRepo>();
            services.AddScoped<UserService>();
        });


    }
}
