using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EndlasNet.Data.Migrations
{
    public partial class mig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PointOfContact = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerPhone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "EnvironmentalSnapshots",
                columns: table => new
                {
                    EnvSnapshotId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateTimeCollected = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Temperature = table.Column<float>(type: "real", nullable: false),
                    Humidity = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnvironmentalSnapshots", x => x.EnvSnapshotId);
                });

            migrationBuilder.CreateTable(
                name: "Quotes",
                columns: table => new
                {
                    QuoteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EndlasNumber = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quotes", x => x.QuoteId);
                });

            migrationBuilder.CreateTable(
                name: "StaticPowderInfo",
                columns: table => new
                {
                    StaticPowderInfoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PowderName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstCostPerLb = table.Column<float>(type: "real", nullable: false),
                    Composition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Density = table.Column<float>(type: "real", nullable: false),
                    FlowRateSlope = table.Column<float>(type: "real", nullable: true),
                    FlowRateYIntercept = table.Column<float>(type: "real", nullable: true),
                    CompositionFilePdfBytes = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaticPowderInfo", x => x.StaticPowderInfoId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EndlasEmail = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AuthString = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "StaticPartInfo",
                columns: table => new
                {
                    StaticPartInfoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DrawingNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApproxWeight = table.Column<float>(type: "real", nullable: false),
                    PartDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DrawingImageBytes = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    BlankDrawingPdfBytes = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    FinishDrawingPdfBytes = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaticPartInfo", x => x.StaticPartInfoId);
                    table.ForeignKey(
                        name: "FK_StaticPartInfo_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StaticPartInfo_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vendors",
                columns: table => new
                {
                    VendorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VendorName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PointOfContact = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VendorAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VendorPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendors", x => x.VendorId);
                    table.ForeignKey(
                        name: "FK_Vendors_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Work",
                columns: table => new
                {
                    WorkId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EndlasNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    QuoteId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    WorkDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    PurchaseOrderNum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProcessSheetNotesPdfBytes = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Work", x => x.WorkId);
                    table.ForeignKey(
                        name: "FK_Work_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Work_Quotes_QuoteId",
                        column: x => x.QuoteId,
                        principalTable: "Quotes",
                        principalColumn: "QuoteId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Work_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MachiningTools",
                columns: table => new
                {
                    MachiningToolId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ToolType = table.Column<int>(type: "int", nullable: false),
                    ToolDiameter = table.Column<float>(type: "real", nullable: false),
                    RadialMetric = table.Column<int>(type: "int", nullable: false),
                    Units = table.Column<int>(type: "int", nullable: false),
                    ToolDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VendorDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InitToolCount = table.Column<int>(type: "int", nullable: false),
                    ToolCount = table.Column<int>(type: "int", nullable: false),
                    PurchaseOrderNum = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    PurchaseOrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PurchaseOrderCost = table.Column<float>(type: "real", nullable: false),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    VendorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachiningTools", x => x.MachiningToolId);
                    table.ForeignKey(
                        name: "FK_MachiningTools_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MachiningTools_Vendors_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendors",
                        principalColumn: "VendorId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PowderOrders",
                columns: table => new
                {
                    PowderOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PurchaseOrderNum = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PurchaseOrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShippingCost = table.Column<float>(type: "real", nullable: true),
                    TaxCost = table.Column<float>(type: "real", nullable: true),
                    VendorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    NumberOfLineItems = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PowderOrders", x => x.PowderOrderId);
                    table.ForeignKey(
                        name: "FK_PowderOrders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PowderOrders_Vendors_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendors",
                        principalColumn: "VendorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PartsForWork",
                columns: table => new
                {
                    PartForWorkId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StaticPartInfoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Suffix = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumParts = table.Column<int>(type: "int", nullable: false),
                    ConditionDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InitWeight = table.Column<float>(type: "real", nullable: true),
                    CladdedWeight = table.Column<float>(type: "real", nullable: true),
                    FinishedWeight = table.Column<float>(type: "real", nullable: true),
                    ProcessingNotes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartsForWork", x => x.PartForWorkId);
                    table.ForeignKey(
                        name: "FK_PartsForWork_StaticPartInfo_StaticPartInfoId",
                        column: x => x.StaticPartInfoId,
                        principalTable: "StaticPartInfo",
                        principalColumn: "StaticPartInfoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PartsForWork_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PartsForWork_Work_WorkId",
                        column: x => x.WorkId,
                        principalTable: "Work",
                        principalColumn: "WorkId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MachiningToolsForWork",
                columns: table => new
                {
                    MachiningToolForWorkId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateUsed = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WorkId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MachiningToolId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MachiningType = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachiningToolsForWork", x => x.MachiningToolForWorkId);
                    table.ForeignKey(
                        name: "FK_MachiningToolsForWork_MachiningTools_MachiningToolId",
                        column: x => x.MachiningToolId,
                        principalTable: "MachiningTools",
                        principalColumn: "MachiningToolId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MachiningToolsForWork_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MachiningToolsForWork_Work_WorkId",
                        column: x => x.WorkId,
                        principalTable: "Work",
                        principalColumn: "WorkId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LineItems",
                columns: table => new
                {
                    LineItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StaticPowderInfoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    VendorDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Weight = table.Column<float>(type: "real", nullable: false),
                    LineItemCost = table.Column<float>(type: "real", nullable: false),
                    ParticleSizeMin = table.Column<float>(type: "real", nullable: false),
                    ParticleSizeMax = table.Column<float>(type: "real", nullable: false),
                    PowderOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NumBottles = table.Column<int>(type: "int", nullable: false),
                    IsInitialized = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineItems", x => x.LineItemId);
                    table.ForeignKey(
                        name: "FK_LineItems_PowderOrders_PowderOrderId",
                        column: x => x.PowderOrderId,
                        principalTable: "PowderOrders",
                        principalColumn: "PowderOrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LineItems_StaticPowderInfo_StaticPowderInfoId",
                        column: x => x.StaticPowderInfoId,
                        principalTable: "StaticPowderInfo",
                        principalColumn: "StaticPowderInfoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PowderBottles",
                columns: table => new
                {
                    PowderBottleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BottleNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InitWeight = table.Column<float>(type: "real", nullable: false),
                    Weight = table.Column<float>(type: "real", nullable: false),
                    LotNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LineItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StaticPowderInfoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PowderBottles", x => x.PowderBottleId);
                    table.ForeignKey(
                        name: "FK_PowderBottles_LineItems_LineItemId",
                        column: x => x.LineItemId,
                        principalTable: "LineItems",
                        principalColumn: "LineItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PowderBottles_StaticPowderInfo_StaticPowderInfoId",
                        column: x => x.StaticPowderInfoId,
                        principalTable: "StaticPowderInfo",
                        principalColumn: "StaticPowderInfoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PowderBottles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PowderForParts",
                columns: table => new
                {
                    PowderForPartId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateUsed = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PowderBottleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PartForWorkId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PowderWeightUsed = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PowderForParts", x => x.PowderForPartId);
                    table.ForeignKey(
                        name: "FK_PowderForParts_PartsForWork_PartForWorkId",
                        column: x => x.PartForWorkId,
                        principalTable: "PartsForWork",
                        principalColumn: "PartForWorkId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PowderForParts_PowderBottles_PowderBottleId",
                        column: x => x.PowderBottleId,
                        principalTable: "PowderBottles",
                        principalColumn: "PowderBottleId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "CustomerAddress", "CustomerName", "CustomerPhone", "PointOfContact" },
                values: new object[] { new Guid("6a7594f9-ce85-4eb1-95b1-2b36d8f65f8c"), "Dummy Customer Address", "Dummy Customer Name", "0987654321", "Dummy Point of Contact" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "AuthString", "Discriminator", "EndlasEmail", "FirstName", "LastName" },
                values: new object[,]
                {
                    { new Guid("e28aa626-7235-4ac6-b479-1cb8af0d7f40"), "5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8", "Admin", "sa@endlas.com", "SA", "SA" },
                    { new Guid("c3c0c3b5-5999-4c4e-9963-2a5738ac2b9c"), "10e4be5b8934f5279b7a10a0ed3988043561d2eccde97bc6ac9eb6062aa6221c", "Admin", "james.tomich@endlas.com", "Jimmy", "Tomich" },
                    { new Guid("43b49a4c-7949-47a8-9b52-89dc73682cbd"), "4c2a671ebe8c3cd38f3e080470701b7bf2d2a4616d986475507c5153888b63f7", "Admin", "josh.hammell@endlas.com", "Josh", "Hammell" },
                    { new Guid("2d6542ad-4a32-4380-a3af-dc0ed049667f"), "2209cf9aaea01490c254f7a0885fa6afc2ba6807cd27dcbc28e802f613e05c82", "Admin", "blt@endlas.com", "Brett", "Trotter" }
                });

            migrationBuilder.InsertData(
                table: "Vendors",
                columns: new[] { "VendorId", "PointOfContact", "UserId", "VendorAddress", "VendorName", "VendorPhone" },
                values: new object[] { new Guid("ed3c5f4b-60d9-46ef-aef5-f1b011d90f52"), "Dummy Point of Contact", null, "Dummy Vendor Address", "Dummy Vendor Name", "1234567890" });

            migrationBuilder.CreateIndex(
                name: "IX_LineItems_PowderOrderId",
                table: "LineItems",
                column: "PowderOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_LineItems_StaticPowderInfoId",
                table: "LineItems",
                column: "StaticPowderInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_MachiningTools_UserId",
                table: "MachiningTools",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MachiningTools_VendorId",
                table: "MachiningTools",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_MachiningToolsForWork_MachiningToolId",
                table: "MachiningToolsForWork",
                column: "MachiningToolId");

            migrationBuilder.CreateIndex(
                name: "IX_MachiningToolsForWork_UserId",
                table: "MachiningToolsForWork",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MachiningToolsForWork_WorkId",
                table: "MachiningToolsForWork",
                column: "WorkId");

            migrationBuilder.CreateIndex(
                name: "IX_PartsForWork_StaticPartInfoId",
                table: "PartsForWork",
                column: "StaticPartInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_PartsForWork_UserId",
                table: "PartsForWork",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PartsForWork_WorkId",
                table: "PartsForWork",
                column: "WorkId");

            migrationBuilder.CreateIndex(
                name: "IX_PowderBottles_LineItemId",
                table: "PowderBottles",
                column: "LineItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PowderBottles_StaticPowderInfoId",
                table: "PowderBottles",
                column: "StaticPowderInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_PowderBottles_UserId",
                table: "PowderBottles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PowderForParts_PartForWorkId",
                table: "PowderForParts",
                column: "PartForWorkId");

            migrationBuilder.CreateIndex(
                name: "IX_PowderForParts_PowderBottleId",
                table: "PowderForParts",
                column: "PowderBottleId");

            migrationBuilder.CreateIndex(
                name: "IX_PowderOrders_UserId",
                table: "PowderOrders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PowderOrders_VendorId",
                table: "PowderOrders",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_Quotes_EndlasNumber",
                table: "Quotes",
                column: "EndlasNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StaticPartInfo_CustomerId",
                table: "StaticPartInfo",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_StaticPartInfo_UserId",
                table: "StaticPartInfo",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_EndlasEmail",
                table: "Users",
                column: "EndlasEmail",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vendors_UserId",
                table: "Vendors",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Work_CustomerId",
                table: "Work",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Work_EndlasNumber",
                table: "Work",
                column: "EndlasNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Work_QuoteId",
                table: "Work",
                column: "QuoteId");

            migrationBuilder.CreateIndex(
                name: "IX_Work_UserId",
                table: "Work",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnvironmentalSnapshots");

            migrationBuilder.DropTable(
                name: "MachiningToolsForWork");

            migrationBuilder.DropTable(
                name: "PowderForParts");

            migrationBuilder.DropTable(
                name: "MachiningTools");

            migrationBuilder.DropTable(
                name: "PartsForWork");

            migrationBuilder.DropTable(
                name: "PowderBottles");

            migrationBuilder.DropTable(
                name: "StaticPartInfo");

            migrationBuilder.DropTable(
                name: "Work");

            migrationBuilder.DropTable(
                name: "LineItems");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Quotes");

            migrationBuilder.DropTable(
                name: "PowderOrders");

            migrationBuilder.DropTable(
                name: "StaticPowderInfo");

            migrationBuilder.DropTable(
                name: "Vendors");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
