using EndlasNet.Data;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.SystemTest
{
    public class LineItemRepoTest
    {
        public class UserRepoTests
        {
            private EndlasNetDbContext _db;
            private LineItemRepo repo;
            StaticPowderInfo powderInfo;
            Vendor vendor;
            PowderOrder powderOrderGood, powderOrderBad;
            [SetUp]
            public void Setup()
            {
                _db = SingletonTestSetup.Instance().Get();
                repo = new LineItemRepo(_db);

                powderInfo = new StaticPowderInfo
                {
                    StaticPowderInfoId = Guid.NewGuid(),
                    PowderName = "TestPowder",
                    Density = 1f,
                };
                _db.StaticPowderInfos.Add(powderInfo);
                _db.SaveChanges();

                vendor = new Vendor
                {
                    VendorId = Guid.NewGuid(),
                    VendorName = "Name",
                    PointOfContact = "POC",
                    VendorAddress = "Address",
                    VendorPhone = "Phone"
                };
                _db.Vendors.Add(vendor);
                _db.SaveChanges();

                powderOrderGood = new PowderOrder
                {
                    PowderOrderId = Guid.NewGuid(),
                    PurchaseOrderDate = DateTime.Now,
                    PurchaseOrderNum = "",
                    ShippingCost = 0.0f,
                    TaxCost = 0.0f,
                    Vendor = vendor,
                    VendorId = vendor.VendorId,
                };
                _db.PowderOrders.Add(powderOrderGood);

                powderOrderBad = new PowderOrder
                {
                    PowderOrderId = Guid.NewGuid(),
                    PurchaseOrderDate = DateTime.Now,
                    PurchaseOrderNum = "",
                    ShippingCost = 0.0f,
                    TaxCost = 0.0f,
                    Vendor = vendor,
                    VendorId = vendor.VendorId,
                };
                _db.PowderOrders.Add(powderOrderBad);
                _db.SaveChanges();
            }

            [Test]
            public async Task GetLineItemsTest()
            {
                /// ARRANGE
                var lineItem1 = new LineItem
                {
                    LineItemId = Guid.NewGuid(),
                    StaticPowderInfoId = powderInfo.StaticPowderInfoId,
                    StaticPowderInfo = powderInfo,
                    PowderOrder = powderOrderGood,
                    PowderOrderId = powderOrderGood.PowderOrderId,
                    ParticleSize = 1,
                    VendorDescription = "",
                    NumBottles = 1
                };
                _db.LineItems.Add(lineItem1);
                await _db.SaveChangesAsync();
                var lineItem2 = new LineItem
                {
                    LineItemId = Guid.NewGuid(),
                    StaticPowderInfoId = powderInfo.StaticPowderInfoId,
                    StaticPowderInfo = powderInfo,
                    PowderOrder = powderOrderBad,
                    PowderOrderId = powderOrderBad.PowderOrderId,
                    ParticleSize = 2,
                    VendorDescription = "",
                    NumBottles = 2
                };
                _db.LineItems.Add(lineItem2);
                await _db.SaveChangesAsync();

                var result = await repo.GetLineItems(powderOrderGood.PowderOrderId);
              

                /// ASSERT
                Assert.IsNotNull(result);
                Assert.AreEqual(1, result.ToList().Count);
                Assert.AreEqual(lineItem1.LineItemId, result.ToList().FirstOrDefault().LineItemId);
            }

           [TearDown]
           public async Task CleanUp()
            {
                var lineItems = await _db.LineItems.ToListAsync();

                foreach (LineItem item in lineItems)
                {
                    _db.Remove(item);
                    _db.Entry(item).State = EntityState.Deleted;
                }
                _db.Remove(vendor);
                _db.Entry(vendor).State = EntityState.Deleted;
                _db.Remove(powderInfo);
                _db.Entry(powderInfo).State = EntityState.Deleted;
                // tell the db all the pilots were removed
                await _db.SaveChangesAsync();
            }
        }
    }
}
