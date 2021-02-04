﻿// <auto-generated />
using System;
using EndlasNet.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EndlasNet.Data.Migrations
{
    [DbContext(typeof(EndlasNetDbContext))]
    partial class EndlasNetDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("EndlasNet.Data.DrillBit", b =>
                {
                    b.Property<Guid>("MachiningToolId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<float>("DrillBitRadius")
                        .HasColumnType("real");

                    b.Property<DateTime>("PurchaseOrderDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PurchaseOrderNum")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<float>("PurchaseOrderPrice")
                        .HasColumnType("real");

                    b.Property<int>("ToolCount")
                        .HasColumnType("int");

                    b.Property<Guid?>("ToolToJobsToolToJobId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("VendorDescription")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<Guid>("VendorId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("MachiningToolId");

                    b.HasIndex("ToolToJobsToolToJobId");

                    b.HasIndex("UserId");

                    b.HasIndex("VendorId");

                    b.ToTable("DrillBits");
                });

            modelBuilder.Entity("EndlasNet.Data.EnvironmentalSnapshot", b =>
                {
                    b.Property<Guid>("EnvSnapshotId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateTimeCollected")
                        .HasColumnType("datetime2");

                    b.Property<float>("Humidity")
                        .HasColumnType("real");

                    b.Property<float>("Temperature")
                        .HasColumnType("real");

                    b.HasKey("EnvSnapshotId");

                    b.ToTable("EnvironmentalSnapshots");
                });

            modelBuilder.Entity("EndlasNet.Data.Insert", b =>
                {
                    b.Property<Guid>("MachiningToolId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("PurchaseOrderDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PurchaseOrderNum")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<float>("PurchaseOrderPrice")
                        .HasColumnType("real");

                    b.Property<int>("ToolCount")
                        .HasColumnType("int");

                    b.Property<float>("ToolTipRadius")
                        .HasColumnType("real");

                    b.Property<Guid?>("ToolToJobsToolToJobId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("VendorDescription")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<Guid>("VendorId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("MachiningToolId");

                    b.HasIndex("ToolToJobsToolToJobId");

                    b.HasIndex("UserId");

                    b.HasIndex("VendorId");

                    b.ToTable("Inserts");
                });

            modelBuilder.Entity("EndlasNet.Data.Job", b =>
                {
                    b.Property<Guid>("JobId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("EndlasNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("JobDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PurchaseOrderNum")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("JobId");

                    b.HasIndex("UserId");

                    b.ToTable("Jobs");
                });

            modelBuilder.Entity("EndlasNet.Data.Powder", b =>
                {
                    b.Property<Guid>("PowderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BottleNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("CostPerUnitWeight")
                        .HasColumnType("real");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<float>("InitWeight")
                        .HasColumnType("real");

                    b.Property<string>("LotNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("ParticleSize")
                        .HasColumnType("real");

                    b.Property<DateTime>("PoDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PoNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PowderName")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("VendorDescription")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<Guid>("VendorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<float>("Weight")
                        .HasColumnType("real");

                    b.HasKey("PowderId");

                    b.HasIndex("UserId");

                    b.HasIndex("VendorId");

                    b.ToTable("Powders");
                });

            modelBuilder.Entity("EndlasNet.Data.ToolToJob", b =>
                {
                    b.Property<Guid>("ToolToJobId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateUsed")
                        .HasColumnType("datetime2");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("JobId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ToolToJobId");

                    b.ToTable("ToolToJob");

                    b.HasDiscriminator<string>("Discriminator").HasValue("ToolToJob");
                });

            modelBuilder.Entity("EndlasNet.Data.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AuthString")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EndlasEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.HasIndex("EndlasEmail")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasDiscriminator<string>("Discriminator").HasValue("User");
                });

            modelBuilder.Entity("EndlasNet.Data.Vendor", b =>
                {
                    b.Property<Guid>("VendorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PointOfContact")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("VendorAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VendorName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VendorPhone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("VendorId");

                    b.HasIndex("UserId");

                    b.ToTable("Vendors");

                    b.HasData(
                        new
                        {
                            VendorId = new Guid("f2f0198c-f4c5-4cc2-86af-202222f06e46"),
                            PointOfContact = "Dummy Point of Contact",
                            VendorAddress = "Dummy Vendor Address",
                            VendorName = "Dummy Vendor Name",
                            VendorPhone = "1234567890"
                        });
                });

            modelBuilder.Entity("EndlasNet.Data.InsertToJob", b =>
                {
                    b.HasBaseType("EndlasNet.Data.ToolToJob");

                    b.HasIndex("JobId");

                    b.HasIndex("UserId");

                    b.HasDiscriminator().HasValue("InsertToJob");
                });

            modelBuilder.Entity("EndlasNet.Data.Admin", b =>
                {
                    b.HasBaseType("EndlasNet.Data.User");

                    b.HasDiscriminator().HasValue("Admin");

                    b.HasData(
                        new
                        {
                            UserId = new Guid("7850b17e-19e0-4aeb-9eaa-5a3bc70a912b"),
                            AuthString = "5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8",
                            EndlasEmail = "SA@endlas.com",
                            FirstName = "SA",
                            LastName = "SA"
                        },
                        new
                        {
                            UserId = new Guid("3dfe47ce-79bc-4723-8fcd-bfa5eda7d32d"),
                            AuthString = "10e4be5b8934f5279b7a10a0ed3988043561d2eccde97bc6ac9eb6062aa6221c",
                            EndlasEmail = "James.Tomich@endlas.com",
                            FirstName = "James",
                            LastName = "Tomich"
                        },
                        new
                        {
                            UserId = new Guid("500268c6-67a9-4499-b4ae-2194f799c84f"),
                            AuthString = "4c2a671ebe8c3cd38f3e080470701b7bf2d2a4616d986475507c5153888b63f7",
                            EndlasEmail = "Josh.Hammell@endlas.com",
                            FirstName = "Josh",
                            LastName = "Hammell"
                        },
                        new
                        {
                            UserId = new Guid("a4b925ac-44ff-4557-8810-28d24fbb40f4"),
                            AuthString = "2209cf9aaea01490c254f7a0885fa6afc2ba6807cd27dcbc28e802f613e05c82",
                            EndlasEmail = "BLT@endlas.com",
                            FirstName = "Brett",
                            LastName = "Trotter"
                        });
                });

            modelBuilder.Entity("EndlasNet.Data.Employee", b =>
                {
                    b.HasBaseType("EndlasNet.Data.User");

                    b.HasDiscriminator().HasValue("Employee");
                });

            modelBuilder.Entity("EndlasNet.Data.DrillBit", b =>
                {
                    b.HasOne("EndlasNet.Data.ToolToJob", "ToolToJobs")
                        .WithMany()
                        .HasForeignKey("ToolToJobsToolToJobId");

                    b.HasOne("EndlasNet.Data.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EndlasNet.Data.Vendor", "Vendor")
                        .WithMany()
                        .HasForeignKey("VendorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ToolToJobs");

                    b.Navigation("User");

                    b.Navigation("Vendor");
                });

            modelBuilder.Entity("EndlasNet.Data.Insert", b =>
                {
                    b.HasOne("EndlasNet.Data.ToolToJob", "ToolToJobs")
                        .WithMany()
                        .HasForeignKey("ToolToJobsToolToJobId");

                    b.HasOne("EndlasNet.Data.User", "User")
                        .WithMany("Inserts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EndlasNet.Data.Vendor", "Vendor")
                        .WithMany("Inserts")
                        .HasForeignKey("VendorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ToolToJobs");

                    b.Navigation("User");

                    b.Navigation("Vendor");
                });

            modelBuilder.Entity("EndlasNet.Data.Job", b =>
                {
                    b.HasOne("EndlasNet.Data.User", "User")
                        .WithMany("Jobs")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("EndlasNet.Data.Powder", b =>
                {
                    b.HasOne("EndlasNet.Data.User", "User")
                        .WithMany("Powders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EndlasNet.Data.Vendor", "Vendor")
                        .WithMany()
                        .HasForeignKey("VendorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("Vendor");
                });

            modelBuilder.Entity("EndlasNet.Data.Vendor", b =>
                {
                    b.HasOne("EndlasNet.Data.User", "User")
                        .WithMany("Vendors")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EndlasNet.Data.InsertToJob", b =>
                {
                    b.HasOne("EndlasNet.Data.Job", "Job")
                        .WithMany("InsertToJobs")
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.ClientNoAction)
                        .IsRequired();

                    b.HasOne("EndlasNet.Data.User", "User")
                        .WithMany("InsertToJobs")
                        .HasForeignKey("UserId");

                    b.Navigation("Job");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EndlasNet.Data.Job", b =>
                {
                    b.Navigation("InsertToJobs");
                });

            modelBuilder.Entity("EndlasNet.Data.User", b =>
                {
                    b.Navigation("Inserts");

                    b.Navigation("InsertToJobs");

                    b.Navigation("Jobs");

                    b.Navigation("Powders");

                    b.Navigation("Vendors");
                });

            modelBuilder.Entity("EndlasNet.Data.Vendor", b =>
                {
                    b.Navigation("Inserts");
                });
#pragma warning restore 612, 618
        }
    }
}
