using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace MyFirstEF
{
    class MyProductService : IMyProductService
    {
        private readonly ProductsContext _context;
        public MyProductService(ProductsContext context)
        {
            _context = context;
        }

        public void AddLogging()
        {
            IServiceProvider provider = _context.GetInfrastructure<IServiceProvider>();

            ILoggerFactory logger = provider.GetService<ILoggerFactory>();
            //logger.AddConsole(LogLevel.Trace);
            logger.AddDebug();
        }

        public void ShowAllEntries()
        {
            var query = _context.Products
                .Include(s => s.Supplier).ThenInclude(sa => sa.Address).ThenInclude(sc => sc.Country)
                .Include(p => p.Producer).ToList();
            foreach (var product in query)
            {
                Console.WriteLine($"{product.productId}, Name {product.Name}, Supplier: {product.Supplier.SupplierName}");
                Debug.WriteLine("Hello World");
            }
                        
        }

        public void FilterEntries()
        {
            var query = from p in _context.Products
                        where p.productId == 1
                        select p;

            foreach (var product in query)
            {
                Console.WriteLine($"{product.productId}, Name {product.Name}, Description {product.Description}");
            }
        }

        

        public void addData ()
        {
            var mySupplier = new Supplier
            {
                SupplierName = "DAN",
                Active = true
            };

            var myProducer = new Producer
            {
                SupplierName = "Tischler1",
                Active = true,
                Notice = "Test",
                Address = new Address { City = "St. Pölten", Street = "Porschestraße 6", zip = "3100", Country= new Country { ISOCode="AT", Name="Österreich"} }
            };

            var myWood = new Material { Title = "Holz" };
            var myGlas = new Material { Title = "Glas" };
            var myPVC = new Material { Title = "PVC" };

            _context.Add(mySupplier);

            _context.Add(new Product {
                //productId = 1,
                Name = "2Sitzer",
                Description = "Schöne Couch für 2 Personen",
                Supplier = mySupplier,
                Producer = myProducer,
                WG = new Productgroup { Title = "Sitzen", Aktive = true },
                Materials = new List<Material> { myGlas, myPVC }
            });
            _context.Add(new Product
            {
                //productId = 2,
                Name = "4Sitzer",
                Description = "Schöne Sitzbankgruppe in L-Form",
                Supplier =mySupplier,
                Producer = myProducer,
                WG = new Productgroup { Title = "Wohnen", Aktive = true },
                Materials = new List<Material> { myWood, myPVC }
            });
            _context.Add(new Product
            {
                //productId = 3,
                Name = "8Sitzer",
                Description = "Große Sitzbankgruppe in U-Form",
                Producer = myProducer,
                Supplier = mySupplier,
                WG = new Productgroup { Title = "Schlafen", Aktive = true },
                Materials = new List<Material> { myGlas }
            });

            

            int iChangesSaved = _context.SaveChanges();
            Console.WriteLine($"Inserted {iChangesSaved} datasets");
        }

        public void UpdateData(string param)
        {
            var toUpdate = _context.Products.Where(p => p.productId == 1).First();
            toUpdate.Description = param;
            _context.SaveChanges();
        }

        public void createDatabase()
        {
            bool created = _context.Database.EnsureCreated();
            Console.WriteLine($"database created? {created}");
        }

        public void dropDatabase()
        {
            bool dropped = _context.Database.EnsureDeleted();
            Console.WriteLine($"database deleted? {dropped}");

        }
    }
}
