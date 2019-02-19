using System;
using System.Data.Entity;
using System.Data.SQLite;
using System.Data.SQLite.EF6;
using AFTestApp.Data.Entities;

namespace AFTestApp.Data
{
    public sealed class AfTestAppContext : DbContext
    {
        public AfTestAppContext(SQLiteConnection sqLiteConnection)
            : base(sqLiteConnection, false)
        {
        }

        public IDbSet<Product> Products { get; set; }
        public IDbSet<Document> Documents { get; set; }
        public IDbSet<DocumentProduct> DocumentProduct { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var initializer =
                new DataBaseInitializer(modelBuilder);
            Database.SetInitializer(initializer);

            base.OnModelCreating(modelBuilder);
        }

        public static void CreateDatabaseFile(string path)
        {
            try
            {
                using (var sqLiteConnection = CreateConnection(path))
                using (var context = new AfTestAppContext(sqLiteConnection))
                {
                    var initializator =
                        new CreateDatabaseIfNotExists<AfTestAppContext>();
                    Database.SetInitializer(initializator);

                    context.SaveChanges();
                    // do not delete this log trace. we need some DB touch
                    //_log.Trace("Db Context validated. Found {0} Customer(s)", context.A1.Count());
                }
            }
            catch (Exception ex)
            {
               //_log.Error(ex);
            }
        }

        public static SQLiteConnection CreateConnection(string path)
        {
            var builder =
                (SQLiteConnectionStringBuilder) SQLiteProviderFactory.Instance.CreateConnectionStringBuilder();

            if (builder == null)
            {
                throw new Exception("Cannot create SQLiteConnectionStringBuilder");
            }

            builder.DataSource = path;
            builder.FailIfMissing = false;

            return new SQLiteConnection(builder.ToString());
        }
    }
}
