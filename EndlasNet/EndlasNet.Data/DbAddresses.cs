namespace EndlasNet.Data
{
    /*
    * Class: DbAddresses
    * Description: Static class to hold different db paths that may be needed
    */
    public static class DbAddresses
    {
        // local db addreses (paths)
        public const string endlasNetLocalDBPath = "Server=(localdb)\\mssqllocaldb;Database=EndlasNetDb;Trusted_Connection=True;MultipleActiveResultSets=true";
        public const string endlasNetLocalTestDbPath = "Server=(localdb)\\mssqllocaldb;Database=EndlasNetTestDb;Trusted_Connection=True;MultipleActiveResultSets=true";
        // example azure address
        public const string endlasNetAzurePath = "Server=endlasnetsvr.database.windows.net;Database=EndlasNetDb;user id = dev_svr; password=1qazxsw2!QAZXSW@;Encrypt=True;Trusted_Connection=False;MultipleActiveResultSets=true;App=EntityFramework";
    }
}
