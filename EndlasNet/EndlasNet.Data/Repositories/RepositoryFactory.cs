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
        PowderForPart,
        StaticPowderInfo,
        MachiningTool
    }

    public sealed class RepositoryFactory
    {
        private readonly EndlasNetDbContext _db;
        public RepositoryFactory(EndlasNetDbContext db)
        {
            _db = db;
        }

        public IRepository GetRepository(RepositoryTypes repoType)
        {
            switch (repoType)
            {
                case RepositoryTypes.User:                  
                    return new UserRepo(_db);
                case RepositoryTypes.Vendor:
                    return new VendorRepo(_db);
                case RepositoryTypes.Customer:
                    return new CustomerRepo(_db); 
                case RepositoryTypes.PowderOrder:
                    return new PowderOrderRepo(_db); 
                case RepositoryTypes.LineItem:
                    return new LineItemRepo(_db);
                case RepositoryTypes.Powder:
                    return new PowderBottleRepo(_db); 
                case RepositoryTypes.PowderForPart:
                    return new PowderForPartRepo(_db); 
                case RepositoryTypes.StaticPowderInfo:
                    return new StaticPowderInfoRepo(_db);
                case RepositoryTypes.MachiningTool:
                    return new MachiningToolRepo(_db);
                default:
                    break;
            }
            return null;
        }
    }
}
