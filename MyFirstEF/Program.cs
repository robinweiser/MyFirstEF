using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace MyFirstEF
{
    class Program
    {
        private const string ConnectionString = @"server=(localdb)\mssqllocaldb;database=ProductDB3;trusted_connection=true";

        static void Main(string[] args)
        {
            ConfigureServices();
            using (var scope = ApplicationServices.CreateScope())
            {
                string strEnter = "";

                var service = scope.ServiceProvider.GetService<IMyProductService>();
                service.AddLogging();
                service.createDatabase();
                //service.addData();
                //service.ShowAllEntries();
                
                //service.UpdateData();
                Console.ReadLine();
                Console.WriteLine("Old DS:");
                service.FilterEntries();

                while (strEnter.ToLower() != "x")
                {
                    service.UpdateData(strEnter);
                    Console.WriteLine("Updated DS:");
                    service.FilterEntries();
                    strEnter = Console.ReadLine();
                }
                //service.dropDatabase();
            }

        }

        private static void ConfigureServices()
        {
            var services = new ServiceCollection();
            services.AddDbContext<ProductsContext>(options =>
            {
                options.UseSqlServer(ConnectionString);
            });
            services.AddScoped<IMyProductService,MyProductService>();
            ApplicationServices = services.BuildServiceProvider();


        }

        public static IServiceProvider ApplicationServices { get; private set; }

    }
}
