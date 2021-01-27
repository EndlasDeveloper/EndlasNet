using System;
using System.Collections.Generic;
using System.Text;
using EndlasNet.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EndlasNet.SystemTest
{

    public sealed class SingletonTestSetup
    {
        private static SingletonTestSetup instance = null;
        private readonly string connectionString = ConnectionStrings.endlas_test;
        private EndlasNetDbContext _context;

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static SingletonTestSetup() { }

        /*
            * Private constructor. Only Instance function can call it.
            */
        private SingletonTestSetup()
        {
            // setup the local db for the system testing
            var serviceProvider = new ServiceCollection()
                                .AddEntityFrameworkSqlServer()
                                .BuildServiceProvider();

            // setup builder and point to the local db
            var builder = new DbContextOptionsBuilder<EndlasNetDbContext>();
            builder.UseSqlServer(connectionString, b => b.MigrationsAssembly("EndlasNet.Data"));
            _context = new EndlasNetDbContext(builder.Options);
            _context.Database.Migrate();
        }

        /*
            * Getter of the context instance. 
            */
        public static SingletonTestSetup Instance()
        {
            if (instance == null)
            {
                instance = new SingletonTestSetup();
            }
            return instance;
        }

        /*
            * Getter for the instance of OpenAirDbContext
            */
        public EndlasNetDbContext Get()
        {
            return _context;
        }
    }
}

