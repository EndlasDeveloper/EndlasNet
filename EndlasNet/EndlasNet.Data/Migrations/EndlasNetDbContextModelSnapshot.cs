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

            modelBuilder.Entity("EndlasNet.Data.Insert", b =>
                {
                    b.Property<Guid>("InsertId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("InsertCount")
                        .HasColumnType("int");

                    b.Property<Guid?>("InsertToJobId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("PurchaseOrderDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PurchaseOrderNum")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("PurchaseOrderPrice")
                        .HasColumnType("real");

                    b.Property<float>("ToolTipRadius")
                        .HasColumnType("real");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("VendorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("VendorPartNum")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("InsertId");

                    b.HasIndex("InsertToJobId");

                    b.HasIndex("UserId");

                    b.HasIndex("VendorId");

                    b.ToTable("Inserts");
                });

            modelBuilder.Entity("EndlasNet.Data.InsertToJob", b =>
                {
                    b.Property<Guid>("InsertToJobId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateUsed")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("JobId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("InsertToJobId");

                    b.HasIndex("JobId");

                    b.HasIndex("UserId");

                    b.ToTable("InsertToJobs");
                });

            modelBuilder.Entity("EndlasNet.Data.Job", b =>
                {
                    b.Property<Guid>("JobId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("JobId");

                    b.ToTable("Jobs");
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

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

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

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

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

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PointOfContact")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

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
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("VendorId");

                    b.HasIndex("UserId");

                    b.ToTable("Vendors");
                });

            modelBuilder.Entity("EndlasNet.Data.Admin", b =>
                {
                    b.HasBaseType("EndlasNet.Data.User");

                    b.HasDiscriminator().HasValue("Admin");
                });

            modelBuilder.Entity("EndlasNet.Data.Employee", b =>
                {
                    b.HasBaseType("EndlasNet.Data.User");

                    b.HasDiscriminator().HasValue("Employee");
                });

            modelBuilder.Entity("EndlasNet.Data.Insert", b =>
                {
                    b.HasOne("EndlasNet.Data.InsertToJob", "InsertToJob")
                        .WithMany()
                        .HasForeignKey("InsertToJobId");

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

                    b.Navigation("InsertToJob");

                    b.Navigation("User");

                    b.Navigation("Vendor");
                });

            modelBuilder.Entity("EndlasNet.Data.InsertToJob", b =>
                {
                    b.HasOne("EndlasNet.Data.Job", "Job")
                        .WithMany("InsertToJobs")
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EndlasNet.Data.User", "User")
                        .WithMany("InsertToJobs")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Job");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EndlasNet.Data.Vendor", b =>
                {
                    b.HasOne("EndlasNet.Data.User", "User")
                        .WithMany("Vendors")
                        .HasForeignKey("UserId");

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
