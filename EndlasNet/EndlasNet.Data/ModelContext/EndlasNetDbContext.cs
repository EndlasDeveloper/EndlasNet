using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public class EndlasNetDbContext : DbContext { 

        // TODO: this is the local host, change to new one after getting remote db setup
        private readonly string connectionString = "Server=(localdb)\\mssqllocaldb;Database=EndlasNetDb;Trusted_Connection=True;MultipleActiveResultSets=true";

        public DbSet<Customer> Customers { get; set; }
        public DbSet<RawMaterial> RawMaterials { get; set; }
        public DbSet<QuoteSession> QuoteSessions { get; set; }
        public DbSet<LaserQuoteSession> LaserQuoteSessions { get; set; }
        public DbSet<MachineQuoteSession> MachineSessions { get; set; }
        public DbSet<IntermediateParam> IntermediateParams { get; set; }
        public DbSet<Quote> Quotes { get; set; }


        public EndlasNetDbContext(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public EndlasNetDbContext(DbContextOptions<EndlasNetDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // use the connection string to find the database, then put the migration assembly folder into OpenAir.Data
                // These are from the appsettings.json
                optionsBuilder.UseSqlServer(connectionString, b => b.MigrationsAssembly("OpenAir.Data"));
            }
        }
        /*
         * This method defines the database model so the database can be created by EntityFrameworkCore.
         * Inside the map classes are details on specific entity attributes.
         * The modelBuilder.Entity<Class>().HasData calls seed the database with specific data upon the database's
         * creation.
         */
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // setup the map references so ef knows how to specifically constrain attributes
            Contract.Requires(modelBuilder != null);
            base.OnModelCreating(modelBuilder);
            _ = new CustomerMap(modelBuilder.Entity<Customer>());
            _ = new RawMaterialMap(modelBuilder.Entity<RawMaterial>());
           //_ = new QuoteSessionMap(modelBuilder.Entity<QuoteSession>());
           /* _ = new LaserQuoteSessionMap(modelBuilder.Entity<LaserQuoteSession>());
            _ = new MachineQuoteSessionMap(modelBuilder.Entity<MachineQuoteSession>());
            _ = new IntermediateParamMap(modelBuilder.Entity<IntermediateParam>);
            _ = new QuoteMap(modelBuilder.Entity<Quote>);
            _ = new MultiplicityMap(modelBuilder.Entity<Quote>);*/

            ////
            // Seed the Routes table
            ////
            // example : modelBuilder.Entity<Route>().HasData(new Route { RouteId = "VR-140" });
        }

    }
}
