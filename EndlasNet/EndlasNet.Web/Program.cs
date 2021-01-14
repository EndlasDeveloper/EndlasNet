using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndlasNet.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    var sshServer = "192.168.1.103";
                    var sshUserName = "endlas_developer";
                    var sshPassword = "endlas_dev1qazxsw2!QAZXSW@";

                    var dbServer = "127.0.0.1";
                    var dbUserName = "dba";
                    var dbPwd = "1qazxsw2!QAZXSW@";

                    var (sshClient, localPort) = ConnectSshClass.ConnectSsh(sshServer, sshUserName, sshPassword, databaseServer: dbServer);
                    MySqlConnectionStringBuilder csb = new MySqlConnectionStringBuilder
                    {
                        Server = "127.0.0.1",
                        Port = localPort,
                        UserID = dbUserName,
                        Password = dbPwd,
                    };
                    using (sshClient)
                    {
                        webBuilder.UseStartup<Startup>();
                    }
                });
    }
}
