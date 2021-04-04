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
        MachiningTool
    }

    public sealed class RepositoryFactory
    {
        private readonly EndlasNetDbContext _db;
        private readonly UserRepo _userRepo;
        private readonly VendorRepo _vendorRepo;
        private readonly CustomerRepo _customerRepo;
        private readonly PowderBottleRepo _powderRepo;
        private readonly PowderOrderRepo _powderOrderRepo;
        private readonly LineItemRepo _lineItemRepo;
        private readonly StaticPowderInfoRepo _staticPowderInfoRepo;
        private readonly MachiningToolRepo _machiningToolRepo;
        static RepositoryFactory() { }
        public RepositoryFactory(EndlasNetDbContext db)
        {
            _db = db;
            _userRepo = new UserRepo(_db);
            _vendorRepo = new VendorRepo(_db);
            _customerRepo = new CustomerRepo(_db);
            _powderOrderRepo = new PowderOrderRepo(_db);
            _lineItemRepo = new LineItemRepo(_db);
            _powderRepo = new PowderBottleRepo(_db);
            _staticPowderInfoRepo = new StaticPowderInfoRepo(_db);
            _machiningToolRepo = new MachiningToolRepo(_db);
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
                case RepositoryTypes.MachiningTool:
                    return _machiningToolRepo;
                default:
                    break;
            }
            return null;
        }
    }
}
