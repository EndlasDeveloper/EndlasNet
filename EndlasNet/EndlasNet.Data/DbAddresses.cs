namespace EndlasNet.Data
{
    /*
    * Class: DbAddresses
    * Description: Static class to hold different db paths that may be needed
    */
    public static class DbAddresses
    {
        // directly connect to sql server from LAN device 
        public const string endlas_svr = "Server=192.168.1.103,1433\\SQLEXPRESS;Database=endlas_db;user id=SA; password=sa1qazxsw2!QAZXSW@;Trusted_Connection=True;Integrated security=False;";
        // connect to sql server from local device
        public const string endlas_web = "localhost,1433\\SQLEXPRESS;Database=endlas_db;user id=SA; password=sa1qazxsw2!QAZXSW@;Trusted_Connection=True;Integrated security=False;";


        public const string mssql_ex = "Server=localhost\\SQLEXPRESS;Database=master;Trusted_Connection=True;";
        public const string localDb = "server=127.0.0.1;user=root;password=1qazxsw2!QAZXSW@;database=local_db";
        public const string endlasTestDb = "server=127.0.0.1;user=dba;password=1qazxsw2!QAZXSW@;database=endlas_test_db";
        // local db addreses (paths)
        public const string endlasNetLocalDBPath = "Server=(localdb)\\mssqllocaldb;Database=EndlasNetDb;Trusted_Connection=True;MultipleActiveResultSets=true";
        public const string endlasNetLocalTestDbPath = "Server=(localdb)\\mssqllocaldb;Database=EndlasNetTestDb;Trusted_Connection=True;MultipleActiveResultSets=true";
        // example azure address
        public const string endlasNetAzurePath = "Server=endlasnetsvr.database.windows.net;Database=EndlasNetDb;user id = dev_svr; password=1qazxsw2!QAZXSW@;Encrypt=True;Trusted_Connection=False;MultipleActiveResultSets=true;App=EntityFramework";
    }
}
