using System;
using AFTestApp.Configs;
using AFTestApp.Data;
using AFTestApp.Data.Entities;

namespace AFTestApp.DataBasePopulator
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var configProvider = new ConfigurationProvider();
            var factory = new AfTestAppContextFactory(configProvider);
            using (var context = factory.CreateContext())
            {
                context.Products.Add(new Product()
                {
                    Name = "Test"
                });
                context.SaveChanges();
            }
        }
    }
}
