using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RestServerlessFuncApp;
using RestServerlessFuncApp.Application.Services;
using RestServerlessFuncApp.Core.Services;
using RestServerlessFuncApp.Infra;
using System;

[assembly: FunctionsStartup(typeof(Startup))]

namespace RestServerlessFuncApp
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            try
            {
                var keyVaultUrl = new Uri(Environment.GetEnvironmentVariable("KeyVaultUrl"));
                var secretClient = new SecretClient(keyVaultUrl, new DefaultAzureCredential());

                string connectionString = secretClient.GetSecret("TodosDbConnection").Value.Value;

                builder.Services.AddDbContext<AppDataContext>(options => options.UseSqlServer(connectionString));
                builder.Services.AddScoped<ITodosService, TodosService>();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
    }
}
