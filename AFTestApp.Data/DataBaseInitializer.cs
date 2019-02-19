using System.Data.Entity;
using AFTestApp.Data.Entities;
using SQLite.CodeFirst;

namespace AFTestApp.Data
{
    public class DataBaseInitializer: SqliteCreateDatabaseIfNotExists<AfTestAppContext>
    {
        protected override void Seed(AfTestAppContext context)
        {
            var products = new[] {"Мороженное детское", "Пельмени", "Печенье"};

            var i = 1;
            foreach (var product in products)
            {
                var productEntity = new Product
                {
                    Name = product,
                    Code = i.ToString()
                };
                context.Products.Add(productEntity);
                i++;
            }

            context.SaveChanges();
        }

        public DataBaseInitializer(DbModelBuilder modelBuilder) : base(modelBuilder)
        {
        }

        public DataBaseInitializer(DbModelBuilder modelBuilder, bool nullByteFileMeansNotExisting) : base(modelBuilder, nullByteFileMeansNotExisting)
        {
        }
    }
}
