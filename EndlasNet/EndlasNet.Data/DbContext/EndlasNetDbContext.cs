using Microsoft.EntityFrameworkCore;
using System;

namespace EndlasNet.Data
{
    /*
    * Class: EndlasNetDbContext
    * Description: DbContext object for EndlasNet. Describes the db and gives an API to access the db.
    */
    public class EndlasNetDbContext : DbContext
    {
        // connection string for context to db
        private readonly string connectionString = ConnectionStrings.endlas_local;
        // USER
        public DbSet<User> Users { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Admin> Admins { get; set; }
        // INVENTORY
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<MachiningTool> MachiningTools { get; set; }
        public DbSet<Powder> Powders { get; set; }
        // WORK
        public DbSet<Part> Parts { get; set; }
        public DbSet<PartForJob> PartsForJobs { get; set; }
        public DbSet<PartForWorkOrder> PartsForWorkOrders { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<WorkOrder> WorkOrders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        // ENVIRONMENT
        public DbSet<EnvironmentalSnapshot> EnvironmentalSnapshots { get; set; }

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
            // USER
            _ = new UserMap(modelBuilder.Entity<User>());
            // INVENTORY
            _ = new VendorMap(modelBuilder.Entity<Vendor>());
            _ = new PowderMap(modelBuilder.Entity<Powder>());
            _ = new MachiningToolMap(modelBuilder.Entity<MachiningTool>());
            // WORK
            _ = new CustomerMap(modelBuilder.Entity<Customer>());
            _ = new WorkOrderMap(modelBuilder.Entity<WorkOrder>());
            _ = new JobMap(modelBuilder.Entity<Job>());
            _ = new PartMap(modelBuilder.Entity<Part>());
            _ = new PartForJobMap(modelBuilder.Entity<PartForJob>());
            _ = new MachiningToolForJobMap(modelBuilder.Entity<MachiningToolForJob>());
            _ = new MachiningToolForWorkOrderMap(modelBuilder.Entity<MachiningToolForWorkOrder>());
            _ = new PartForJobMap(modelBuilder.Entity<PartForJob>());
            _ = new PartForWorkOrderMap(modelBuilder.Entity<PartForWorkOrder>());
            // ENVIRONMENT
            _ = new EnvironmentalSnapshotMap(modelBuilder.Entity<EnvironmentalSnapshot>());
            // MULTIPLICITY
            _ = new MultiplicityMap(modelBuilder);

            var SA = new Admin
            {
                UserId = Guid.NewGuid(),
                FirstName = "SA",
                LastName = "SA",
                AuthString = "5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8",
                EndlasEmail = "SA@endlas.com"
            };
            var JamesAdmin = new Admin
            {
                UserId = Guid.NewGuid(),
                FirstName = "James",
                LastName = "Tomich",
                AuthString = "10e4be5b8934f5279b7a10a0ed3988043561d2eccde97bc6ac9eb6062aa6221c",
                EndlasEmail = "james.tomich@endlas.com"
            };
            var JoshAdmin = new Admin
            {
                UserId = Guid.NewGuid(),
                FirstName = "Josh",
                LastName = "Hammell",
                AuthString = "4c2a671ebe8c3cd38f3e080470701b7bf2d2a4616d986475507c5153888b63f7",
                EndlasEmail = "josh.hammell@endlas.com"
            };
            var BrettAdmin = new Admin
            {
                UserId = Guid.NewGuid(),
                FirstName = "Brett",
                LastName = "Trotter",
                AuthString = "2209cf9aaea01490c254f7a0885fa6afc2ba6807cd27dcbc28e802f613e05c82",
                EndlasEmail = "blt@endlas.com"
            };

            // PUT ADMINS TO USER TABLE
            // auth str: password
            modelBuilder.Entity<Admin>().HasData(SA);
            // auth str: JimmyLasers4Life
            modelBuilder.Entity<Admin>().HasData(JamesAdmin);
            // auth str: JoshLasers4Life
            modelBuilder.Entity<Admin>().HasData(JoshAdmin);
            // auth str: BrettLasers4Life
            modelBuilder.Entity<Admin>().HasData(BrettAdmin);

            // PUT VENDOR DUMMY TO VENDOR TABLE
            modelBuilder.Entity<Vendor>().HasData(new Vendor
            {
                VendorId = Guid.NewGuid(),
                VendorName = "Dummy Vendor Name",
                PointOfContact = "Dummy Point of Contact",
                VendorAddress = "Dummy Vendor Address",
                VendorPhone = "1234567890"
            });

            // PUT CUSTOMER DUMMY TO CUSTOMER TABLE
            modelBuilder.Entity<Customer>().HasData(new Customer
            {
                CustomerId = Guid.NewGuid(),
                CustomerName = "Dummy Customer Name",
                PointOfContact = "Dummy Point of Contact",
                CustomerAddress = "Dummy Customer Address",
                CustomerPhone = "0987654321"
            });

            // PUT PART DUMMY TO PART TABLE

            // PUT JOB DUMMY TO JOB TABLE
        }

    }
}