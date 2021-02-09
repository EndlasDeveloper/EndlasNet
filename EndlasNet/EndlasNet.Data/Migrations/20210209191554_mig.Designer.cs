﻿// <auto-generated />
using System;
using EndlasNet.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EndlasNet.Data.Migrations
{
    [DbContext(typeof(EndlasNetDbContext))]
    [Migration("20210209191554_mig")]
    partial class mig
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("EndlasNet.Data.Customer", b =>
                {
                    b.Property<Guid>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CustomerAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerPhone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PointOfContact")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CustomerId");

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            CustomerId = new Guid("08a7f354-7bb9-4869-803c-0a38b5d36dda"),
                            CustomerAddress = "Dummy Customer Address",
                            CustomerName = "Dummy Customer Name",
                            CustomerPhone = "0987654321",
                            PointOfContact = "Dummy Point of Contact"
                        });
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

            modelBuilder.Entity("EndlasNet.Data.Job", b =>
                {
                    b.Property<Guid>("WorkId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("EndlasNumber")
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

                    b.Property<string>("WorkDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("WorkId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("UserId");

                    b.ToTable("Jobs");
                });

            modelBuilder.Entity("EndlasNet.Data.MachiningTool", b =>
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

                    b.Property<float>("ToolDiameter")
                        .HasColumnType("real");

                    b.Property<Guid?>("ToolToJobMachiningToolForJobId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ToolType")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("VendorDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("VendorId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("MachiningToolId");

                    b.HasIndex("ToolToJobMachiningToolForJobId");

                    b.HasIndex("UserId");

                    b.HasIndex("VendorId");

                    b.ToTable("MachiningTools");
                });

            modelBuilder.Entity("EndlasNet.Data.MachiningToolForJob", b =>
                {
                    b.Property<Guid>("MachiningToolForJobId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateUsed")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("JobId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("MachiningToolForJobId");

                    b.HasIndex("JobId");

                    b.HasIndex("UserId");

                    b.ToTable("MachiningToolForJob");
                });

            modelBuilder.Entity("EndlasNet.Data.MachiningToolForWorkOrder", b =>
                {
                    b.Property<Guid>("MachiningToolForJobId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateUsed")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("WorkOrderId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("MachiningToolForJobId");

                    b.HasIndex("UserId");

                    b.HasIndex("WorkOrderId");

                    b.ToTable("MachiningToolForWorkOrder");
                });

            modelBuilder.Entity("EndlasNet.Data.Part", b =>
                {
                    b.Property<Guid>("PartId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<float>("CladdedWeight")
                        .HasColumnType("real");

                    b.Property<string>("ConditionDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<byte[]>("DrawingImage")
                        .HasColumnType("image");

                    b.Property<string>("DrawingNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("InitWeight")
                        .HasColumnType("real");

                    b.Property<string>("ProcessingNotes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<float>("Weight")
                        .HasColumnType("real");

                    b.HasKey("PartId");

                    b.HasIndex("UserId");

                    b.ToTable("Parts");
                });

            modelBuilder.Entity("EndlasNet.Data.PartForJob", b =>
                {
                    b.Property<Guid>("PartForWorkId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("JobId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("PartId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("PartForWorkId");

                    b.HasIndex("JobId");

                    b.HasIndex("PartId");

                    b.HasIndex("UserId");

                    b.ToTable("PartsForJobs");
                });

            modelBuilder.Entity("EndlasNet.Data.PartForWorkOrder", b =>
                {
                    b.Property<Guid>("PartForWorkOrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("PartId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("WorkOrderId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("PartForWorkOrderId");

                    b.HasIndex("PartId");

                    b.HasIndex("WorkOrderId");

                    b.ToTable("PartForWorkOrder");
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
                            VendorId = new Guid("9871c51c-98b9-4f65-b857-57c2e69ce3ea"),
                            PointOfContact = "Dummy Point of Contact",
                            VendorAddress = "Dummy Vendor Address",
                            VendorName = "Dummy Vendor Name",
                            VendorPhone = "1234567890"
                        });
                });

            modelBuilder.Entity("EndlasNet.Data.WorkOrder", b =>
                {
                    b.Property<Guid>("WorkId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("EndlasNumber")
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

                    b.Property<string>("WorkDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("WorkId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("UserId");

                    b.ToTable("WorkOrders");
                });

            modelBuilder.Entity("EndlasNet.Data.Admin", b =>
                {
                    b.HasBaseType("EndlasNet.Data.User");

                    b.HasDiscriminator().HasValue("Admin");

                    b.HasData(
                        new
                        {
                            UserId = new Guid("7fd28c38-3bbf-44a7-b160-bf6889145bd1"),
                            AuthString = "5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8",
                            EndlasEmail = "SA@endlas.com",
                            FirstName = "SA",
                            LastName = "SA"
                        },
                        new
                        {
                            UserId = new Guid("d9e550b9-5045-45d1-8d20-a17c64375922"),
                            AuthString = "10e4be5b8934f5279b7a10a0ed3988043561d2eccde97bc6ac9eb6062aa6221c",
                            EndlasEmail = "james.tomich@endlas.com",
                            FirstName = "James",
                            LastName = "Tomich"
                        },
                        new
                        {
                            UserId = new Guid("e0b605fb-57cc-4de9-ad5c-b9bc2355b7af"),
                            AuthString = "4c2a671ebe8c3cd38f3e080470701b7bf2d2a4616d986475507c5153888b63f7",
                            EndlasEmail = "josh.hammell@endlas.com",
                            FirstName = "Josh",
                            LastName = "Hammell"
                        },
                        new
                        {
                            UserId = new Guid("75a5a951-d23a-4f12-8227-9ca6a04da53f"),
                            AuthString = "2209cf9aaea01490c254f7a0885fa6afc2ba6807cd27dcbc28e802f613e05c82",
                            EndlasEmail = "blt@endlas.com",
                            FirstName = "Brett",
                            LastName = "Trotter"
                        });
                });

            modelBuilder.Entity("EndlasNet.Data.Employee", b =>
                {
                    b.HasBaseType("EndlasNet.Data.User");

                    b.HasDiscriminator().HasValue("Employee");
                });

            modelBuilder.Entity("EndlasNet.Data.Job", b =>
                {
                    b.HasOne("EndlasNet.Data.Customer", "Customer")
                        .WithMany("Jobs")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EndlasNet.Data.User", "User")
                        .WithMany("Jobs")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EndlasNet.Data.MachiningTool", b =>
                {
                    b.HasOne("EndlasNet.Data.MachiningToolForJob", "ToolToJob")
                        .WithMany()
                        .HasForeignKey("ToolToJobMachiningToolForJobId");

                    b.HasOne("EndlasNet.Data.User", "User")
                        .WithMany("MachiningTools")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EndlasNet.Data.Vendor", "Vendor")
                        .WithMany("MachiningTools")
                        .HasForeignKey("VendorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ToolToJob");

                    b.Navigation("User");

                    b.Navigation("Vendor");
                });

            modelBuilder.Entity("EndlasNet.Data.MachiningToolForJob", b =>
                {
                    b.HasOne("EndlasNet.Data.Job", "Job")
                        .WithMany("ToolsForJobs")
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EndlasNet.Data.User", "User")
                        .WithMany("MachiningToolForJobs")
                        .HasForeignKey("UserId");

                    b.Navigation("Job");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EndlasNet.Data.MachiningToolForWorkOrder", b =>
                {
                    b.HasOne("EndlasNet.Data.User", "User")
                        .WithMany("MachiningToolForWorkOrders")
                        .HasForeignKey("UserId");

                    b.HasOne("EndlasNet.Data.WorkOrder", "WorkOrder")
                        .WithMany("ToolsForWorksOrders")
                        .HasForeignKey("WorkOrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("WorkOrder");
                });

            modelBuilder.Entity("EndlasNet.Data.Part", b =>
                {
                    b.HasOne("EndlasNet.Data.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("EndlasNet.Data.PartForJob", b =>
                {
                    b.HasOne("EndlasNet.Data.Job", "Job")
                        .WithMany("PartsForJobs")
                        .HasForeignKey("JobId");

                    b.HasOne("EndlasNet.Data.Part", "Part")
                        .WithMany("PartsForJobs")
                        .HasForeignKey("PartId");

                    b.HasOne("EndlasNet.Data.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("Job");

                    b.Navigation("Part");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EndlasNet.Data.PartForWorkOrder", b =>
                {
                    b.HasOne("EndlasNet.Data.Part", "Part")
                        .WithMany()
                        .HasForeignKey("PartId");

                    b.HasOne("EndlasNet.Data.WorkOrder", "Job")
                        .WithMany("PartsForWorkOrders")
                        .HasForeignKey("WorkOrderId");

                    b.Navigation("Job");

                    b.Navigation("Part");
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

            modelBuilder.Entity("EndlasNet.Data.WorkOrder", b =>
                {
                    b.HasOne("EndlasNet.Data.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId");

                    b.HasOne("EndlasNet.Data.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EndlasNet.Data.Customer", b =>
                {
                    b.Navigation("Jobs");
                });

            modelBuilder.Entity("EndlasNet.Data.Job", b =>
                {
                    b.Navigation("PartsForJobs");

                    b.Navigation("ToolsForJobs");
                });

            modelBuilder.Entity("EndlasNet.Data.Part", b =>
                {
                    b.Navigation("PartsForJobs");
                });

            modelBuilder.Entity("EndlasNet.Data.User", b =>
                {
                    b.Navigation("Jobs");

                    b.Navigation("MachiningToolForJobs");

                    b.Navigation("MachiningToolForWorkOrders");

                    b.Navigation("MachiningTools");

                    b.Navigation("Powders");

                    b.Navigation("Vendors");
                });

            modelBuilder.Entity("EndlasNet.Data.Vendor", b =>
                {
                    b.Navigation("MachiningTools");
                });

            modelBuilder.Entity("EndlasNet.Data.WorkOrder", b =>
                {
                    b.Navigation("PartsForWorkOrders");

                    b.Navigation("ToolsForWorksOrders");
                });
#pragma warning restore 612, 618
        }
    }
}