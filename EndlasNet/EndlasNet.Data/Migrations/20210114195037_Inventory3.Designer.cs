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
    [Migration("20210114195037_Inventory3")]
    partial class Inventory3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("EndlasNet.Data.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("POC")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CustomerId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("EndlasNet.Data.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.HasKey("EmployeeId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("EndlasNet.Data.Insert", b =>
                {
                    b.Property<int>("InsertId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("InsertToJobId")
                        .HasColumnType("int");

                    b.Property<string>("PurchaseDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PurchaseOrderDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PurchaseOrderNum")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("PurchaseOrderPrice")
                        .HasColumnType("real");

                    b.Property<float>("ToolTipRadius")
                        .HasColumnType("real");

                    b.Property<int>("VendorId")
                        .HasColumnType("int");

                    b.Property<string>("VendorPartNum")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("InsertId");

                    b.HasIndex("InsertToJobId");

                    b.HasIndex("VendorId");

                    b.ToTable("Inserts");
                });

            modelBuilder.Entity("EndlasNet.Data.InsertToJob", b =>
                {
                    b.Property<int>("InsertToJobId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("DateUsed")
                        .HasColumnType("datetime2");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<int>("JobId")
                        .HasColumnType("int");

                    b.HasKey("InsertToJobId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("JobId");

                    b.ToTable("InsertToJobs");
                });

            modelBuilder.Entity("EndlasNet.Data.IntermediateParam", b =>
                {
                    b.Property<int>("IntermediateParamId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<double>("ApproxVolPerLayerCubicCm")
                        .HasColumnType("float");

                    b.Property<double>("AssumedAvgPassLenIn")
                        .HasColumnType("float");

                    b.Property<double>("CladAddRateSqInPerMin")
                        .HasColumnType("float");

                    b.Property<int>("LaserQuoteSessionId")
                        .HasColumnType("int");

                    b.Property<int>("PseudoNumPasses")
                        .HasColumnType("int");

                    b.Property<double>("PseudoWidthIn")
                        .HasColumnType("float");

                    b.Property<double>("StepIn")
                        .HasColumnType("float");

                    b.Property<double>("StepMm")
                        .HasColumnType("float");

                    b.Property<double>("SurfaceVelocity")
                        .HasColumnType("float");

                    b.Property<double>("TimeBetweenBeadsMin")
                        .HasColumnType("float");

                    b.Property<double>("TimePerBeadSec")
                        .HasColumnType("float");

                    b.Property<double>("TimePerLayerMin")
                        .HasColumnType("float");

                    b.HasKey("IntermediateParamId");

                    b.HasIndex("LaserQuoteSessionId");

                    b.ToTable("IntermediateParams");
                });

            modelBuilder.Entity("EndlasNet.Data.Job", b =>
                {
                    b.Property<int>("JobId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.HasKey("JobId");

                    b.ToTable("Jobs");
                });

            modelBuilder.Entity("EndlasNet.Data.LaserQuoteSession", b =>
                {
                    b.Property<int>("LaserQuoteSessionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<double>("ArgonCost")
                        .HasColumnType("float");

                    b.Property<double>("EstPowerCost")
                        .HasColumnType("float");

                    b.Property<double>("FinishedPartWeight")
                        .HasColumnType("float");

                    b.Property<double>("FringeRate")
                        .HasColumnType("float");

                    b.Property<double>("HourlyLaborRate")
                        .HasColumnType("float");

                    b.Property<double>("HourlyUseRate")
                        .HasColumnType("float");

                    b.Property<bool>("IsFlowRateAnalytical")
                        .HasColumnType("bit");

                    b.Property<int>("NumLayers")
                        .HasColumnType("int");

                    b.Property<int>("NumParts")
                        .HasColumnType("int");

                    b.Property<int?>("OptionalLaserServicesId")
                        .HasColumnType("int");

                    b.Property<double>("OverheadRate")
                        .HasColumnType("float");

                    b.Property<double>("PartChangeoverTimeHr")
                        .HasColumnType("float");

                    b.Property<double>("PartSurfaceAreaSqIn")
                        .HasColumnType("float");

                    b.Property<double>("ProfitRate")
                        .HasColumnType("float");

                    b.Property<int>("QuoteSessionId")
                        .HasColumnType("int");

                    b.Property<double>("SetupTimeMin")
                        .HasColumnType("float");

                    b.Property<double>("ShippingWeightFactor")
                        .HasColumnType("float");

                    b.HasKey("LaserQuoteSessionId");

                    b.HasIndex("OptionalLaserServicesId");

                    b.HasIndex("QuoteSessionId");

                    b.ToTable("LaserQuoteSessions");
                });

            modelBuilder.Entity("EndlasNet.Data.MachineQuoteSession", b =>
                {
                    b.Property<int>("MachineQuoteSessionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("QuoteSessionId")
                        .HasColumnType("int");

                    b.HasKey("MachineQuoteSessionId");

                    b.HasIndex("QuoteSessionId");

                    b.ToTable("MachineSessions");
                });

            modelBuilder.Entity("EndlasNet.Data.OptionalLaserService", b =>
                {
                    b.Property<int>("OptionalLaserServicesId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<double>("HeatTreatedBlankWt")
                        .HasColumnType("float");

                    b.Property<double>("HeatTreatedPricePerLb")
                        .HasColumnType("float");

                    b.Property<double>("MinHeatTreatmentPrice")
                        .HasColumnType("float");

                    b.HasKey("OptionalLaserServicesId");

                    b.ToTable("OptionalLaserServices");
                });

            modelBuilder.Entity("EndlasNet.Data.Quote", b =>
                {
                    b.Property<int>("QuoteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<double>("CogsTotal")
                        .HasColumnType("float");

                    b.Property<double>("EnergyTotal")
                        .HasColumnType("float");

                    b.Property<double>("FringeTotal")
                        .HasColumnType("float");

                    b.Property<double>("GasTotal")
                        .HasColumnType("float");

                    b.Property<double>("LaborDirectTotal")
                        .HasColumnType("float");

                    b.Property<double>("OverheadTotal")
                        .HasColumnType("float");

                    b.Property<double>("PowderDirectTotal")
                        .HasColumnType("float");

                    b.Property<double>("ProfitTotal")
                        .HasColumnType("float");

                    b.Property<int>("QuoteSessionId")
                        .HasColumnType("int");

                    b.Property<double>("ShippingTotal")
                        .HasColumnType("float");

                    b.HasKey("QuoteId");

                    b.HasIndex("QuoteSessionId");

                    b.ToTable("Quotes");
                });

            modelBuilder.Entity("EndlasNet.Data.QuoteSession", b =>
                {
                    b.Property<int>("QuoteSessionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("QuoteSessionDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("QuoteSessionName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("QuoteSessionId");

                    b.HasIndex("CustomerId");

                    b.ToTable("QuoteSessions");
                });

            modelBuilder.Entity("EndlasNet.Data.RawMaterial", b =>
                {
                    b.Property<int>("RawMaterialId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<double>("CladLayerDensity")
                        .HasColumnType("float");

                    b.Property<string>("OrderNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PowderFeeder")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("PowderLayerPrice")
                        .HasColumnType("float");

                    b.Property<string>("PowderType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RawMaterialEmpiricalId")
                        .HasColumnType("int");

                    b.Property<string>("Vendor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RawMaterialId");

                    b.HasIndex("RawMaterialEmpiricalId");

                    b.ToTable("RawMaterials");
                });

            modelBuilder.Entity("EndlasNet.Data.RawMaterialEmpirical", b =>
                {
                    b.Property<int>("RawMaterialEmpiricalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<double>("FlowRateSlope")
                        .HasColumnType("float");

                    b.Property<double>("FlowRateYIntercept")
                        .HasColumnType("float");

                    b.HasKey("RawMaterialEmpiricalId");

                    b.ToTable("RawMaterialEmpiricals");
                });

            modelBuilder.Entity("EndlasNet.Data.RawMaterial_LaserQuoteSession", b =>
                {
                    b.Property<int>("LaserQuoteSessionId")
                        .HasColumnType("int");

                    b.Property<int>("RawMaterialId")
                        .HasColumnType("int");

                    b.Property<double>("AvgThicknessIn")
                        .HasColumnType("float");

                    b.Property<double>("EstCaptureEffeciency")
                        .HasColumnType("float");

                    b.Property<double>("LayerSurfaceAreaSqIn")
                        .HasColumnType("float");

                    b.Property<double>("PercentBeadOverlap")
                        .HasColumnType("float");

                    b.Property<double>("PowderRpm")
                        .HasColumnType("float");

                    b.Property<double>("PowerInWatts")
                        .HasColumnType("float");

                    b.Property<double>("ProcessingFlowRateLiPerMin")
                        .HasColumnType("float");

                    b.Property<double>("SpotSizeMm")
                        .HasColumnType("float");

                    b.Property<double>("SurfaceVelocityMmPerSec")
                        .HasColumnType("float");

                    b.HasKey("LaserQuoteSessionId", "RawMaterialId");

                    b.HasIndex("RawMaterialId");

                    b.ToTable("RawMaterial_LaserQuoteSessions");
                });

            modelBuilder.Entity("EndlasNet.Data.Vendor", b =>
                {
                    b.Property<int>("VendorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("POC")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("VendorId");

                    b.ToTable("Vendors");
                });

            modelBuilder.Entity("EndlasNet.Data.Insert", b =>
                {
                    b.HasOne("EndlasNet.Data.InsertToJob", "InsertToJob")
                        .WithMany()
                        .HasForeignKey("InsertToJobId");

                    b.HasOne("EndlasNet.Data.Vendor", "Vendor")
                        .WithMany("Inserts")
                        .HasForeignKey("VendorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("InsertToJob");

                    b.Navigation("Vendor");
                });

            modelBuilder.Entity("EndlasNet.Data.InsertToJob", b =>
                {
                    b.HasOne("EndlasNet.Data.Employee", "Employee")
                        .WithMany("InsertToJobs")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EndlasNet.Data.Job", "Job")
                        .WithMany("InsertToJobs")
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("Job");
                });

            modelBuilder.Entity("EndlasNet.Data.IntermediateParam", b =>
                {
                    b.HasOne("EndlasNet.Data.LaserQuoteSession", "LaserQuoteSession")
                        .WithMany()
                        .HasForeignKey("LaserQuoteSessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LaserQuoteSession");
                });

            modelBuilder.Entity("EndlasNet.Data.LaserQuoteSession", b =>
                {
                    b.HasOne("EndlasNet.Data.OptionalLaserService", "OptionalLaserServices")
                        .WithMany()
                        .HasForeignKey("OptionalLaserServicesId");

                    b.HasOne("EndlasNet.Data.QuoteSession", "QuoteSession")
                        .WithMany()
                        .HasForeignKey("QuoteSessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OptionalLaserServices");

                    b.Navigation("QuoteSession");
                });

            modelBuilder.Entity("EndlasNet.Data.MachineQuoteSession", b =>
                {
                    b.HasOne("EndlasNet.Data.QuoteSession", "QuoteSession")
                        .WithMany()
                        .HasForeignKey("QuoteSessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("QuoteSession");
                });

            modelBuilder.Entity("EndlasNet.Data.Quote", b =>
                {
                    b.HasOne("EndlasNet.Data.QuoteSession", "QuoteSession")
                        .WithMany()
                        .HasForeignKey("QuoteSessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("QuoteSession");
                });

            modelBuilder.Entity("EndlasNet.Data.QuoteSession", b =>
                {
                    b.HasOne("EndlasNet.Data.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("EndlasNet.Data.RawMaterial", b =>
                {
                    b.HasOne("EndlasNet.Data.RawMaterialEmpirical", "RawMaterialEmpirical")
                        .WithMany()
                        .HasForeignKey("RawMaterialEmpiricalId");

                    b.Navigation("RawMaterialEmpirical");
                });

            modelBuilder.Entity("EndlasNet.Data.RawMaterial_LaserQuoteSession", b =>
                {
                    b.HasOne("EndlasNet.Data.LaserQuoteSession", "LaserQuoteSession")
                        .WithMany("RawMat_LasQuoteSes")
                        .HasForeignKey("LaserQuoteSessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EndlasNet.Data.RawMaterial", "RawMaterial")
                        .WithMany("RawMat_LasQuoteSes")
                        .HasForeignKey("RawMaterialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LaserQuoteSession");

                    b.Navigation("RawMaterial");
                });

            modelBuilder.Entity("EndlasNet.Data.Employee", b =>
                {
                    b.Navigation("InsertToJobs");
                });

            modelBuilder.Entity("EndlasNet.Data.Job", b =>
                {
                    b.Navigation("InsertToJobs");
                });

            modelBuilder.Entity("EndlasNet.Data.LaserQuoteSession", b =>
                {
                    b.Navigation("RawMat_LasQuoteSes");
                });

            modelBuilder.Entity("EndlasNet.Data.RawMaterial", b =>
                {
                    b.Navigation("RawMat_LasQuoteSes");
                });

            modelBuilder.Entity("EndlasNet.Data.Vendor", b =>
                {
                    b.Navigation("Inserts");
                });
#pragma warning restore 612, 618
        }
    }
}
