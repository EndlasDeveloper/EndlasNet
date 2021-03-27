using System;
using System.Collections.Generic;
using System.Text;

namespace EndlasNet.Data
{
    public enum RepositoryTypes
    {
        User,
        Vendor,
        Customer,
        PowderOrder,
        LineItem,
        Powder,
        StaticPowderInfo,
    }

    public sealed class RepositoryFactory
    {
        private static RepositoryFactory instance = null;
        private readonly EndlasNetDbContext _db;
        private UserRepo _userRepo;
        private VendorRepo _vendorRepo;
        private CustomerRepo _customerRepo;
        private PowderRepo _powderRepo;
        private PowderOrderRepo _powderOrderRepo;
        private LineItemRepo _lineItemRepo;
        private StaticPowderInfoRepo _staticPowderInfoRepo;

        static RepositoryFactory() { }
        private RepositoryFactory(EndlasNetDbContext db)
        {
            _db = db;
            _userRepo = new UserRepo(_db);
            _vendorRepo = new VendorRepo(_db);
            _customerRepo = new CustomerRepo(_db);
            _powderOrderRepo = new PowderOrderRepo(_db);
            _lineItemRepo = new LineItemRepo(_db);
            _powderRepo = new PowderRepo(_db);
            _staticPowderInfoRepo = new StaticPowderInfoRepo(_db);
        }

        public static RepositoryFactory Instance(EndlasNetDbContext db) 
        {
            if (instance == null)
            {
                instance = new RepositoryFactory(db);
            }
            return instance;
        }

        public IRepository GetRepository(RepositoryTypes repoType)
        {
            switch (repoType)
            {
                case RepositoryTypes.User:                  
                    return _userRepo;   
                case RepositoryTypes.Vendor:
                    return _vendorRepo;
                case RepositoryTypes.Customer:
                    return _customerRepo;
                case RepositoryTypes.PowderOrder:
                    return _powderOrderRepo;
                case RepositoryTypes.LineItem:
                    return _lineItemRepo;
                case RepositoryTypes.Powder:
                    return _powderRepo;
                case RepositoryTypes.StaticPowderInfo:
                    return _staticPowderInfoRepo;                 
                default:
                    break;
            }
            return null;
        }
    }
}
