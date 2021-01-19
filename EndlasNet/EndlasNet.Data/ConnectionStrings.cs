namespace EndlasNet.Data
{
    /*
    * Class: ConnectionStrings
    * Description: Static class to hold different db paths that may be needed
    */
    public static class ConnectionStrings
    {
        // directly connect to sql server from LAN device 
        public const string endlas_svr = "Server=192.168.1.103,1433\\SQLEXPRESS;Database=endlas_db;user id=SA; password=sa1qazxsw2!QAZXSW@;Trusted_Connection=True;Integrated security=False;";
        // connect to sql server from local device
        public const string endlas_web = "localhost,1433\\SQLEXPRESS;Database=endlas_db;user id=SA; password=sa1qazxsw2!QAZXSW@;Trusted_Connection=True;Integrated security=False;";
        // local db addreses (paths)
        public const string endlasNetLocalDBPath = "Server=(localdb)\\mssqllocaldb;Database=EndlasNetDb;Trusted_Connection=True;MultipleActiveResultSets=true";
    }
}
