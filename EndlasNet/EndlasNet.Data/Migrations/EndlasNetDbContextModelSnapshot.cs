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
                .HasAnnotation("ProductVersion", "5.0.6")
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
                            CustomerId = new Guid("ff2aee2d-01a4-426a-93ea-7570bb466292"),
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

                    b.Property<byte[]>("CertPdfBytes")
                        .HasColumnType("varbinary(max)");

                    b.Property<bool>("IsInitialized")
                        .HasColumnType("bit");

                    b.Property<float>("LineItemCost")
                        .HasColumnType("real");

                    b.Property<int>("NumBottles")
                        .HasColumnType("int");

                    b.Property<float>("ParticleSizeMax")
                        .HasColumnType("real");

                    b.Property<float>("ParticleSizeMin")
                        .HasColumnType("real");

                    b.Property<Guid>("PowderOrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("StaticPowderInfoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("VendorDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Weight")
                        .HasColumnType("real");

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

                    b.Property<int>("InitToolCount")
                        .HasColumnType("int");

                    b.Property<string>("InvoiceNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("PurchaseOrderCost")
                        .HasColumnType("real");

                    b.Property<DateTime>("PurchaseOrderDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PurchaseOrderNum")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<int>("RadialMetric")
                        .HasColumnType("int");

                    b.Property<int>("ToolCount")
                        .HasColumnType("int");

                    b.Property<string>("ToolDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("ToolDiameter")
                        .HasColumnType("real");

                    b.Property<int>("ToolType")
                        .HasColumnType("int");

                    b.Property<int>("Units")
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

                    b.HasIndex("UserId");

                    b.HasIndex("VendorId");

                    b.ToTable("MachiningTools");
                });

            modelBuilder.Entity("EndlasNet.Data.MachiningToolForWork", b =>
                {
                    b.Property<Guid>("MachiningToolForWorkId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateUsed")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("MachiningToolId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("MachiningType")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("WorkId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("WorkItemId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("MachiningToolForWorkId");

                    b.HasIndex("MachiningToolId");

                    b.HasIndex("UserId");

                    b.HasIndex("WorkId");

                    b.HasIndex("WorkItemId");

                    b.ToTable("MachiningToolsForWork");
                });

            modelBuilder.Entity("EndlasNet.Data.PartForWork", b =>
                {
                    b.Property<Guid>("PartForWorkId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<float?>("CladdedWeight")
                        .HasColumnType("real");

                    b.Property<byte[]>("CladdingImageBytes")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("ConditionDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("FinishedImageBytes")
                        .HasColumnType("varbinary(max)");

                    b.Property<float?>("FinishedWeight")
                        .HasColumnType("real");

                    b.Property<float?>("InitWeight")
                        .HasColumnType("real");

                    b.Property<byte[]>("MachiningImageBytes")
                        .HasColumnType("varbinary(max)");

                    b.Property<Guid?>("PartForWorkImgId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ProcessingNotes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Suffix")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<byte[]>("UsedImageBytes")
                        .HasColumnType("varbinary(max)");

                    b.Property<Guid?>("WorkItemId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("PartForWorkId");

                    b.HasIndex("PartForWorkImgId");

                    b.HasIndex("WorkItemId");

                    b.ToTable("PartsForWork");

                    b.HasDiscriminator<string>("Discriminator").HasValue("PartForWork");
                });

            modelBuilder.Entity("EndlasNet.Data.PartForWorkImg", b =>
                {
                    b.Property<Guid>("PartForWorkImgId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("ImageBytes")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("ImageName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PartForWorkImgId");

                    b.ToTable("PartForWorkImages");
                });

            modelBuilder.Entity("EndlasNet.Data.PowderBottle", b =>
                {
                    b.Property<Guid>("PowderBottleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

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

                    b.Property<Guid?>("StaticPowderInfoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<float>("Weight")
                        .HasColumnType("real");

                    b.HasKey("PowderBottleId");

                    b.HasIndex("LineItemId");

                    b.HasIndex("StaticPowderInfoId");

                    b.HasIndex("UserId");

                    b.ToTable("PowderBottles");
                });

            modelBuilder.Entity("EndlasNet.Data.PowderForPart", b =>
                {
                    b.Property<Guid>("PowderForPartId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateUsed")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("PartForWorkId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("PowderBottleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<float>("PowderWeightUsed")
                        .HasColumnType("real");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("PowderForPartId");

                    b.HasIndex("PartForWorkId");

                    b.HasIndex("PowderBottleId");

                    b.HasIndex("UserId");

                    b.ToTable("PowderForParts");
                });

            modelBuilder.Entity("EndlasNet.Data.PowderOrder", b =>
                {
                    b.Property<Guid>("PowderOrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("NumberOfLineItems")
                        .HasColumnType("int");

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

            modelBuilder.Entity("EndlasNet.Data.Quote", b =>
                {
                    b.Property<Guid>("QuoteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("EndlasNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ShortDescription")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("QuoteId");

                    b.HasIndex("EndlasNumber")
                        .IsUnique();

                    b.ToTable("Quotes");
                });

            modelBuilder.Entity("EndlasNet.Data.StaticPartInfo", b =>
                {
                    b.Property<Guid>("StaticPartInfoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<float>("ApproxWeight")
                        .HasColumnType("real");

                    b.Property<byte[]>("BlankDrawingPdfBytes")
                        .HasColumnType("varbinary(max)");

                    b.Property<Guid?>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("DrawingImageBytes")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("DrawingNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("FinishDrawingPdfBytes")
                        .HasColumnType("varbinary(max)");

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

                    b.Property<string>("Composition")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Density")
                        .HasColumnType("real");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("EstCostPerLb")
                        .HasColumnType("real");

                    b.Property<float?>("FlowRateSlope")
                        .HasColumnType("real");

                    b.Property<float?>("FlowRateYIntercept")
                        .HasColumnType("real");

                    b.Property<byte[]>("InformationFilePdfBytes")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("PowderName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StaticPowderInfoId");

                    b.ToTable("StaticPowderInfo");
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
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

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

                    b.ToTable("Vendors");

                    b.HasData(
                        new
                        {
                            VendorId = new Guid("6a549bc5-071c-4c1b-b9d9-4ad9220e9d25"),
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
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("PoDate")
                        .HasColumnType("datetime2");

                    b.Property<byte[]>("ProcessSheetNotesPdfBytes")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("PurchaseOrderNum")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("QuoteId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("WorkDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("WorkId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("EndlasNumber")
                        .IsUnique();

                    b.HasIndex("QuoteId");

                    b.ToTable("Work");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Work");
                });

            modelBuilder.Entity("EndlasNet.Data.WorkItem", b =>
                {
                    b.Property<Guid>("WorkItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CompleteDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsInitialized")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("StaticPartInfoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<Guid?>("WorkId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("WorkItemImageBytes")
                        .HasColumnType("varbinary(max)");

                    b.HasKey("WorkItemId");

                    b.HasIndex("StaticPartInfoId");

                    b.HasIndex("WorkId");

                    b.ToTable("WorkItems");
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
                            UserId = new Guid("c544ed62-402a-4b0f-9a1d-2e81a48420aa"),
                            AuthString = "5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8",
                            EndlasEmail = "sa@endlas.com",
                            FirstName = "SA",
                            LastName = "SA"
                        },
                        new
                        {
                            UserId = new Guid("12cc046a-1c28-4ef1-9f00-dbbe4b3a003b"),
                            AuthString = "10e4be5b8934f5279b7a10a0ed3988043561d2eccde97bc6ac9eb6062aa6221c",
                            EndlasEmail = "james.tomich@endlas.com",
                            FirstName = "Jimmy",
                            LastName = "Tomich"
                        },
                        new
                        {
                            UserId = new Guid("d042b8a1-0193-484a-a866-741d2292f547"),
                            AuthString = "4c2a671ebe8c3cd38f3e080470701b7bf2d2a4616d986475507c5153888b63f7",
                            EndlasEmail = "josh.hammell@endlas.com",
                            FirstName = "Josh",
                            LastName = "Hammell"
                        },
                        new
                        {
                            UserId = new Guid("014585da-8e26-4aec-80d8-e5e8833f455d"),
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
                        .HasForeignKey("StaticPowderInfoId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("PowderOrder");

                    b.Navigation("StaticPowderInfo");
                });

            modelBuilder.Entity("EndlasNet.Data.MachiningTool", b =>
                {
                    b.HasOne("EndlasNet.Data.User", null)
                        .WithMany("MachiningTools")
                        .HasForeignKey("UserId");

                    b.HasOne("EndlasNet.Data.Vendor", "Vendor")
                        .WithMany("MachiningTools")
                        .HasForeignKey("VendorId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Vendor");
                });

            modelBuilder.Entity("EndlasNet.Data.MachiningToolForWork", b =>
                {
                    b.HasOne("EndlasNet.Data.MachiningTool", "MachiningTool")
                        .WithMany()
                        .HasForeignKey("MachiningToolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EndlasNet.Data.User", "User")
                        .WithMany("MachiningToolForWork")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("EndlasNet.Data.Work", "Work")
                        .WithMany("MachiningToolsForWork")
                        .HasForeignKey("WorkId");

                    b.HasOne("EndlasNet.Data.WorkItem", "WorkItem")
                        .WithMany("MachiningToolsForWork")
                        .HasForeignKey("WorkItemId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("MachiningTool");

                    b.Navigation("User");

                    b.Navigation("Work");

                    b.Navigation("WorkItem");
                });

            modelBuilder.Entity("EndlasNet.Data.PartForWork", b =>
                {
                    b.HasOne("EndlasNet.Data.PartForWorkImg", null)
                        .WithMany("PartsForWork")
                        .HasForeignKey("PartForWorkImgId");

                    b.HasOne("EndlasNet.Data.WorkItem", "WorkItem")
                        .WithMany("PartsForWork")
                        .HasForeignKey("WorkItemId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("WorkItem");
                });

            modelBuilder.Entity("EndlasNet.Data.PowderBottle", b =>
                {
                    b.HasOne("EndlasNet.Data.LineItem", "LineItem")
                        .WithMany("PowderBottles")
                        .HasForeignKey("LineItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EndlasNet.Data.StaticPowderInfo", "StaticPowderInfo")
                        .WithMany("Powders")
                        .HasForeignKey("StaticPowderInfoId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("EndlasNet.Data.User", null)
                        .WithMany("PowderBottles")
                        .HasForeignKey("UserId");

                    b.Navigation("LineItem");

                    b.Navigation("StaticPowderInfo");
                });

            modelBuilder.Entity("EndlasNet.Data.PowderForPart", b =>
                {
                    b.HasOne("EndlasNet.Data.PartForWork", "PartForWork")
                        .WithMany("PowdersUsed")
                        .HasForeignKey("PartForWorkId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EndlasNet.Data.PowderBottle", "PowderBottle")
                        .WithMany("PowderForParts")
                        .HasForeignKey("PowderBottleId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("EndlasNet.Data.User", "User")
                        .WithMany("PowderForParts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("PartForWork");

                    b.Navigation("PowderBottle");

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
                    b.HasOne("EndlasNet.Data.Customer", null)
                        .WithMany("StaticPartInfos")
                        .HasForeignKey("CustomerId");

                    b.HasOne("EndlasNet.Data.User", null)
                        .WithMany("StaticPartInfo")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("EndlasNet.Data.Work", b =>
                {
                    b.HasOne("EndlasNet.Data.Customer", "Customer")
                        .WithMany("Work")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("EndlasNet.Data.Quote", "Quote")
                        .WithMany()
                        .HasForeignKey("QuoteId");

                    b.Navigation("Customer");

                    b.Navigation("Quote");
                });

            modelBuilder.Entity("EndlasNet.Data.WorkItem", b =>
                {
                    b.HasOne("EndlasNet.Data.StaticPartInfo", "StaticPartInfo")
                        .WithMany("WorkItems")
                        .HasForeignKey("StaticPartInfoId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("EndlasNet.Data.Work", "Work")
                        .WithMany("WorkItems")
                        .HasForeignKey("WorkId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("StaticPartInfo");

                    b.Navigation("Work");
                });

            modelBuilder.Entity("EndlasNet.Data.Customer", b =>
                {
                    b.Navigation("StaticPartInfos");

                    b.Navigation("Work");
                });

            modelBuilder.Entity("EndlasNet.Data.LineItem", b =>
                {
                    b.Navigation("PowderBottles");
                });

            modelBuilder.Entity("EndlasNet.Data.PartForWork", b =>
                {
                    b.Navigation("PowdersUsed");
                });

            modelBuilder.Entity("EndlasNet.Data.PartForWorkImg", b =>
                {
                    b.Navigation("PartsForWork");
                });

            modelBuilder.Entity("EndlasNet.Data.PowderBottle", b =>
                {
                    b.Navigation("PowderForParts");
                });

            modelBuilder.Entity("EndlasNet.Data.PowderOrder", b =>
                {
                    b.Navigation("LineItems");
                });

            modelBuilder.Entity("EndlasNet.Data.StaticPartInfo", b =>
                {
                    b.Navigation("WorkItems");
                });

            modelBuilder.Entity("EndlasNet.Data.StaticPowderInfo", b =>
                {
                    b.Navigation("LineItems");

                    b.Navigation("Powders");
                });

            modelBuilder.Entity("EndlasNet.Data.User", b =>
                {
                    b.Navigation("MachiningToolForWork");

                    b.Navigation("MachiningTools");

                    b.Navigation("PowderBottles");

                    b.Navigation("PowderForParts");

                    b.Navigation("StaticPartInfo");
                });

            modelBuilder.Entity("EndlasNet.Data.Vendor", b =>
                {
                    b.Navigation("MachiningTools");
                });

            modelBuilder.Entity("EndlasNet.Data.Work", b =>
                {
                    b.Navigation("MachiningToolsForWork");

                    b.Navigation("WorkItems");
                });

            modelBuilder.Entity("EndlasNet.Data.WorkItem", b =>
                {
                    b.Navigation("MachiningToolsForWork");

                    b.Navigation("PartsForWork");
                });
#pragma warning restore 612, 618
        }
    }
}
