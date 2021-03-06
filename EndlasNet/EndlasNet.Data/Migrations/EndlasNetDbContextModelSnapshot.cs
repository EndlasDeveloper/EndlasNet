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
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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
                            CustomerId = new Guid("50060fe9-476a-4951-9fb1-5c7b79022bb1"),
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

            modelBuilder.Entity("EndlasNet.Data.LineItem", b =>
                {
                    b.Property<Guid>("LineItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("NumBottles")
                        .HasColumnType("int");

                    b.Property<float>("ParticleSize")
                        .HasColumnType("real");

                    b.Property<Guid>("PowderOrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("StaticPowderInfoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("VendorDescription")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LineItemId");

                    b.HasIndex("PowderOrderId");

                    b.HasIndex("StaticPowderInfoId");

                    b.ToTable("LineItems");
                });

            modelBuilder.Entity("EndlasNet.Data.MachiningTool", b =>
                {
                    b.Property<Guid>("MachiningToolId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("InvoiceNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

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

                    b.Property<Guid?>("ToolToWorkMachiningToolForWorkId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ToolType")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("VendorDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("VendorId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("MachiningToolId");

                    b.HasIndex("ToolToWorkMachiningToolForWorkId");

                    b.HasIndex("UserId");

                    b.HasIndex("VendorId");

                    b.ToTable("MachiningTools");
                });

            modelBuilder.Entity("EndlasNet.Data.MachiningToolForWork", b =>
                {
                    b.Property<Guid>("MachiningToolForWorkId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateUsed")
                        .HasColumnType("datetime2");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("WorkId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("MachiningToolForWorkId");

                    b.HasIndex("WorkId");

                    b.ToTable("MachiningToolForWork");

                    b.HasDiscriminator<string>("Discriminator").HasValue("MachiningToolForWork");
                });

            modelBuilder.Entity("EndlasNet.Data.PartForWork", b =>
                {
                    b.Property<Guid>("PartForWorkId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<float?>("CladdedWeight")
                        .HasColumnType("real");

                    b.Property<string>("ConditionDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float?>("FinishedWeight")
                        .HasColumnType("real");

                    b.Property<float?>("InitWeight")
                        .HasColumnType("real");

                    b.Property<int>("NumParts")
                        .HasColumnType("int");

                    b.Property<string>("ProcessingNotes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("StaticPartInfoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Suffix")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("WorkId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("PartForWorkId");

                    b.HasIndex("StaticPartInfoId");

                    b.HasIndex("UserId");

                    b.HasIndex("WorkId");

                    b.ToTable("PartsForWork");

                    b.HasDiscriminator<string>("Discriminator").HasValue("PartForWork");
                });

            modelBuilder.Entity("EndlasNet.Data.Powder", b =>
                {
                    b.Property<Guid>("PowderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<float>("BottleCost")
                        .HasColumnType("real");

                    b.Property<string>("BottleNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<float>("InitWeight")
                        .HasColumnType("real");

                    b.Property<Guid>("LineItemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LotNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("StaticPowderInfoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<float>("Weight")
                        .HasColumnType("real");

                    b.HasKey("PowderId");

                    b.HasIndex("LineItemId");

                    b.HasIndex("StaticPowderInfoId");

                    b.HasIndex("UserId");

                    b.ToTable("Powders");
                });

            modelBuilder.Entity("EndlasNet.Data.PowderOrder", b =>
                {
                    b.Property<Guid>("PowderOrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("PurchaseOrderDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PurchaseOrderNum")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float?>("ShippingCost")
                        .HasColumnType("real");

                    b.Property<float?>("TaxCost")
                        .HasColumnType("real");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("VendorId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("PowderOrderId");

                    b.HasIndex("UserId");

                    b.HasIndex("VendorId");

                    b.ToTable("PowderOrders");
                });

            modelBuilder.Entity("EndlasNet.Data.StaticPartInfo", b =>
                {
                    b.Property<Guid>("StaticPartInfoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<float>("ApproxWeight")
                        .HasColumnType("real");

                    b.Property<Guid?>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("DrawingImage")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("DrawingNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PartDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("StaticPartInfoId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("UserId");

                    b.ToTable("StaticPartInfo");
                });

            modelBuilder.Entity("EndlasNet.Data.StaticPowderInfo", b =>
                {
                    b.Property<Guid>("StaticPowderInfoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<float>("Density")
                        .HasColumnType("real");

                    b.Property<float?>("FlowRateSlope")
                        .HasColumnType("real");

                    b.Property<float?>("FlowRateYIntercept")
                        .HasColumnType("real");

                    b.Property<string>("PowderName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StaticPowderInfoId");

                    b.ToTable("StaticPowderInfos");
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
                            VendorId = new Guid("615bd2fa-98e5-45e7-9829-4df367edcd7d"),
                            PointOfContact = "Dummy Point of Contact",
                            VendorAddress = "Dummy Vendor Address",
                            VendorName = "Dummy Vendor Name",
                            VendorPhone = "1234567890"
                        });
                });

            modelBuilder.Entity("EndlasNet.Data.Work", b =>
                {
                    b.Property<Guid>("WorkId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

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

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("WorkDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("WorkId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("UserId");

                    b.ToTable("Work");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Work");
                });

            modelBuilder.Entity("EndlasNet.Data.MachiningToolForJob", b =>
                {
                    b.HasBaseType("EndlasNet.Data.MachiningToolForWork");

                    b.HasIndex("UserId");

                    b.HasDiscriminator().HasValue("MachiningToolForJob");
                });

            modelBuilder.Entity("EndlasNet.Data.MachiningToolForWorkOrder", b =>
                {
                    b.HasBaseType("EndlasNet.Data.MachiningToolForWork");

                    b.HasIndex("UserId");

                    b.HasDiscriminator().HasValue("MachiningToolForWorkOrder");
                });

            modelBuilder.Entity("EndlasNet.Data.PartForJob", b =>
                {
                    b.HasBaseType("EndlasNet.Data.PartForWork");

                    b.HasDiscriminator().HasValue("PartForJob");
                });

            modelBuilder.Entity("EndlasNet.Data.PartForWorkOrder", b =>
                {
                    b.HasBaseType("EndlasNet.Data.PartForWork");

                    b.HasDiscriminator().HasValue("PartForWorkOrder");
                });

            modelBuilder.Entity("EndlasNet.Data.Admin", b =>
                {
                    b.HasBaseType("EndlasNet.Data.User");

                    b.HasDiscriminator().HasValue("Admin");

                    b.HasData(
                        new
                        {
                            UserId = new Guid("3bea719f-c496-49d1-b700-0c680950b7b0"),
                            AuthString = "5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8",
                            EndlasEmail = "SA@endlas.com",
                            FirstName = "SA",
                            LastName = "SA"
                        },
                        new
                        {
                            UserId = new Guid("3aaf111c-9e5b-48d9-baf9-79136573c963"),
                            AuthString = "10e4be5b8934f5279b7a10a0ed3988043561d2eccde97bc6ac9eb6062aa6221c",
                            EndlasEmail = "james.tomich@endlas.com",
                            FirstName = "James",
                            LastName = "Tomich"
                        },
                        new
                        {
                            UserId = new Guid("5a6530c4-1288-4205-b76d-882bb80cb280"),
                            AuthString = "4c2a671ebe8c3cd38f3e080470701b7bf2d2a4616d986475507c5153888b63f7",
                            EndlasEmail = "josh.hammell@endlas.com",
                            FirstName = "Josh",
                            LastName = "Hammell"
                        },
                        new
                        {
                            UserId = new Guid("5b23bac7-fcdb-4103-b0bc-34f76d570d86"),
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
                    b.HasBaseType("EndlasNet.Data.Work");

                    b.HasDiscriminator().HasValue("Job");
                });

            modelBuilder.Entity("EndlasNet.Data.WorkOrder", b =>
                {
                    b.HasBaseType("EndlasNet.Data.Work");

                    b.HasDiscriminator().HasValue("WorkOrder");
                });

            modelBuilder.Entity("EndlasNet.Data.LineItem", b =>
                {
                    b.HasOne("EndlasNet.Data.PowderOrder", "PowderOrder")
                        .WithMany("LineItems")
                        .HasForeignKey("PowderOrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EndlasNet.Data.StaticPowderInfo", "StaticPowderInfo")
                        .WithMany("LineItems")
                        .HasForeignKey("StaticPowderInfoId");

                    b.Navigation("PowderOrder");

                    b.Navigation("StaticPowderInfo");
                });

            modelBuilder.Entity("EndlasNet.Data.MachiningTool", b =>
                {
                    b.HasOne("EndlasNet.Data.MachiningToolForWork", "ToolToWork")
                        .WithMany()
                        .HasForeignKey("ToolToWorkMachiningToolForWorkId");

                    b.HasOne("EndlasNet.Data.User", "User")
                        .WithMany("MachiningTools")
                        .HasForeignKey("UserId");

                    b.HasOne("EndlasNet.Data.Vendor", "Vendor")
                        .WithMany("MachiningTools")
                        .HasForeignKey("VendorId");

                    b.Navigation("ToolToWork");

                    b.Navigation("User");

                    b.Navigation("Vendor");
                });

            modelBuilder.Entity("EndlasNet.Data.MachiningToolForWork", b =>
                {
                    b.HasOne("EndlasNet.Data.Work", "Work")
                        .WithMany("ToolsForWork")
                        .HasForeignKey("WorkId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Work");
                });

            modelBuilder.Entity("EndlasNet.Data.PartForWork", b =>
                {
                    b.HasOne("EndlasNet.Data.StaticPartInfo", "PartInfo")
                        .WithMany("Parts")
                        .HasForeignKey("StaticPartInfoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EndlasNet.Data.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.HasOne("EndlasNet.Data.Work", "Work")
                        .WithMany("PartsForWork")
                        .HasForeignKey("WorkId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PartInfo");

                    b.Navigation("User");

                    b.Navigation("Work");
                });

            modelBuilder.Entity("EndlasNet.Data.Powder", b =>
                {
                    b.HasOne("EndlasNet.Data.LineItem", "LineItem")
                        .WithMany("Powders")
                        .HasForeignKey("LineItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EndlasNet.Data.StaticPowderInfo", "StaticPowderInfo")
                        .WithMany("Powders")
                        .HasForeignKey("StaticPowderInfoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EndlasNet.Data.User", "User")
                        .WithMany("Powders")
                        .HasForeignKey("UserId");

                    b.Navigation("LineItem");

                    b.Navigation("StaticPowderInfo");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EndlasNet.Data.PowderOrder", b =>
                {
                    b.HasOne("EndlasNet.Data.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.HasOne("EndlasNet.Data.Vendor", "Vendor")
                        .WithMany()
                        .HasForeignKey("VendorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("Vendor");
                });

            modelBuilder.Entity("EndlasNet.Data.StaticPartInfo", b =>
                {
                    b.HasOne("EndlasNet.Data.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId");

                    b.HasOne("EndlasNet.Data.User", null)
                        .WithMany("StaticPartInfo")
                        .HasForeignKey("UserId");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("EndlasNet.Data.Vendor", b =>
                {
                    b.HasOne("EndlasNet.Data.User", "User")
                        .WithMany("Vendors")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EndlasNet.Data.Work", b =>
                {
                    b.HasOne("EndlasNet.Data.Customer", "Customer")
                        .WithMany("Work")
                        .HasForeignKey("CustomerId");

                    b.HasOne("EndlasNet.Data.User", "User")
                        .WithMany("Work")
                        .HasForeignKey("UserId");

                    b.Navigation("Customer");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EndlasNet.Data.MachiningToolForJob", b =>
                {
                    b.HasOne("EndlasNet.Data.User", "User")
                        .WithMany("MachiningToolForJobs")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EndlasNet.Data.MachiningToolForWorkOrder", b =>
                {
                    b.HasOne("EndlasNet.Data.User", "User")
                        .WithMany("MachiningToolForWorkOrders")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EndlasNet.Data.Customer", b =>
                {
                    b.Navigation("Work");
                });

            modelBuilder.Entity("EndlasNet.Data.LineItem", b =>
                {
                    b.Navigation("Powders");
                });

            modelBuilder.Entity("EndlasNet.Data.PowderOrder", b =>
                {
                    b.Navigation("LineItems");
                });

            modelBuilder.Entity("EndlasNet.Data.StaticPartInfo", b =>
                {
                    b.Navigation("Parts");
                });

            modelBuilder.Entity("EndlasNet.Data.StaticPowderInfo", b =>
                {
                    b.Navigation("LineItems");

                    b.Navigation("Powders");
                });

            modelBuilder.Entity("EndlasNet.Data.User", b =>
                {
                    b.Navigation("MachiningToolForJobs");

                    b.Navigation("MachiningToolForWorkOrders");

                    b.Navigation("MachiningTools");

                    b.Navigation("Powders");

                    b.Navigation("StaticPartInfo");

                    b.Navigation("Vendors");

                    b.Navigation("Work");
                });

            modelBuilder.Entity("EndlasNet.Data.Vendor", b =>
                {
                    b.Navigation("MachiningTools");
                });

            modelBuilder.Entity("EndlasNet.Data.Work", b =>
                {
                    b.Navigation("PartsForWork");

                    b.Navigation("ToolsForWork");
                });
#pragma warning restore 612, 618
        }
    }
}
