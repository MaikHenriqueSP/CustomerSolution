using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace CustomerCardService.IntegrationTests
{
    public class BaseIntegrationTest
    {
        protected readonly HttpClient TestClient;

        protected BaseIntegrationTest()
        {
            var appFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        services.RemoveAll(typeof(DbContext));
                        services.AddDbContext<DbContext>(options =>
                        {
                            options.UseInMemoryDatabase("TestDB");
                        });
                    });

                });
            TestClient = appFactory.CreateClient();
        }

    }
}
