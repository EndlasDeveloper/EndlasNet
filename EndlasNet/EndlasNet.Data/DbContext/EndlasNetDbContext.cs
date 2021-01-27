﻿using Microsoft.EntityFrameworkCore;
using System;

namespace EndlasNet.Data
{
    /*
    * Class: EndlasNetDbContext
    * Description: DbContext object for EndlasNet. Describes the db and gives an API to access the db.
    */
    public class EndlasNetDbContext : DbContext
    {
        private readonly string connectionString = ConnectionStrings.endlas_local;

        // define what tables exist in the DbContext
/*        public DbSet<Customer> Customers { get; set; }
        public DbSet<RawMaterial> RawMaterials { get; set; }
        public DbSet<QuoteSession> QuoteSessions { get; set; }
        public DbSet<LaserQuoteSession> LaserQuoteSessions { get; set; }
        public DbSet<MachineQuoteSession> MachineSessions { get; set; }
        public DbSet<IntermediateParam> IntermediateParams { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<RawMaterialEmpirical> RawMaterialEmpiricals { get; set; }
        public DbSet<RawMaterial_LaserQuoteSession> RawMaterial_LaserQuoteSessions { get; set; }
        public DbSet<OptionalLaserService> OptionalLaserServices { get; set; }*/

        public DbSet<User> Users { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Insert> Inserts { get; set; }
        public DbSet<DrillBit> DrillBits { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<InsertToJob> InsertToJobs { get; set; }

        // setup connection string
        public EndlasNetDbContext(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public DbContextOptions<EndlasNetDbContext> options;
        public EndlasNetDbContext(DbContextOptions<EndlasNetDbContext> options) : base(options) {
            this.options = options;
        }

        // configure ef to use .Data project as target project
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connectionString, b => b.MigrationsAssembly("EndlasNet.Data"));
            }
        }
        // setup column and multiplicity constraints
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // setup the map references so ef knows how to specifically constrain attributes
            base.OnModelCreating(modelBuilder);
            _ = new UserMap(modelBuilder.Entity<User>());
            _ = new VendorMap(modelBuilder.Entity<Vendor>());
            _ = new InsertMap(modelBuilder.Entity<Insert>());
            _ = new DrillBitMap(modelBuilder.Entity<DrillBit>());
            _ = new ToolToJobMap(modelBuilder.Entity<ToolToJob>());
            _ = new JobMap(modelBuilder.Entity<Job>());
            _ = new InventoryMultiplicityMap(modelBuilder);

            /*            _ = new CustomerMap(modelBuilder.Entity<Customer>());
                        _ = new RawMaterialMap(modelBuilder.Entity<RawMaterial>());
                        _ = new QuoteSessionMap(modelBuilder.Entity<QuoteSession>());
                        _ = new LaserQuoteSessionMap(modelBuilder.Entity<LaserQuoteSession>());
                        _ = new OptionalLaserServicesMap(modelBuilder.Entity<OptionalLaserService>());
                        _ = new MachineQuoteSessionMap(modelBuilder.Entity<MachineQuoteSession>());
                        _ = new RawMaterial_LaserQuoteSessionMap(modelBuilder.Entity<RawMaterial_LaserQuoteSession>());
                        _ = new IntermediateParamMap(modelBuilder.Entity<IntermediateParam>());
                        _ = new QuoteMap(modelBuilder.Entity<Quote>());
                        _ = new RawMaterialEmpiricalMap(modelBuilder.Entity<RawMaterialEmpirical>());
                        _ = new QuoteInfoMultiplicityMap(modelBuilder);*/


            // auth str: JamesLasers4Life

            var JamesAdmin = new Admin
            {
                UserId = Guid.NewGuid(),
                FirstName = "James",
                LastName = "Tomich",
                AuthString = "10e4be5b8934f5279b7a10a0ed3988043561d2eccde97bc6ac9eb6062aa6221c",
                EndlasEmail = "James.Tomich@endlas.com"
            };
            var JoshAdmin = new Admin
            {
                UserId = Guid.NewGuid(),
                FirstName = "Josh",
                LastName = "Hammell",
                AuthString = "4c2a671ebe8c3cd38f3e080470701b7bf2d2a4616d986475507c5153888b63f7",
                EndlasEmail = "Josh.Hammell@endlas.com"
            };
            var BrettAdmin = new Admin
            {
                UserId = Guid.NewGuid(),
                FirstName = "Brett",
                LastName = "Trotter",
                AuthString = "2209cf9aaea01490c254f7a0885fa6afc2ba6807cd27dcbc28e802f613e05c82",
                EndlasEmail = "Brett.Trotter@endlas.com"
            };
            modelBuilder.Entity<Admin>().HasData(JamesAdmin);
            // auth str: JoshLasers4Life
            modelBuilder.Entity<Admin>().HasData(JoshAdmin);
            // auth str: BrettLasers4Life
            modelBuilder.Entity<Admin>().HasData(BrettAdmin);

        }

    }
}