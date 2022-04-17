using Contact.Services.Dtos;
using Contact.Services.Models;
using Contact.Services.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;

namespace Gateways
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var contactService = serviceProvider.GetRequiredService<IContactService>();
                if (!contactService.GetAllAsync().Result.Data.Any())
                {
                    contactService.CreateAsync(new PersonCreateDto
                    {
                        CompanyName = "test",
                        Name = "batuhan",
                        Surname = "baki",
                        personContactInfo=new PersonContactInfo()
                        {
                            PhoneNumber="12312312",
                            Location="istanbul",
                            Email="bbng3773@gmail.com"
                        }
                    }).Wait();

                    contactService.CreateAsync(new PersonCreateDto
                    {
                        CompanyName = "test2",
                        Name = "anýl",
                        Surname = "arslan",
                        personContactInfo = new PersonContactInfo()
                        {
                            PhoneNumber = "12312312",
                            Location = "kayseri",
                            Email = "anil.arslan@gmail.com"
                        }
                    }).Wait();
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args).ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.AddJsonFile($"configuration.{hostingContext.HostingEnvironment.EnvironmentName.ToLower()}.json").AddEnvironmentVariables();

            })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
