﻿// Copyright (c) 2017 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT licence. See License.txt in the project root for license information.

using System.Data.SqlClient;
using System.Linq;
using DataLayer.EfClasses;
using DataLayer.EfCode;
using Microsoft.EntityFrameworkCore;
using Test.Helpers;
using TestSupport.EfHelpers;
using Xunit;
using Xunit.Extensions.AssertExtensions;

namespace Test.UnitTests.DataLayer
{
    public class TestCreateInstance 
    {

        [Fact]
        public void TestExampleSetupViaConstructorOk()
        {
            //SETUP
            const string connectionString //#A
                = "Server=(localdb)\\mssqllocaldb;Database=EfCore.TestSupport-Test;Trusted_Connection=True";
            var builder = new                             //#B
                DbContextOptionsBuilder<EfCoreContext>(); //#B
            builder.UseSqlServer(connectionString);       //#C
            var options = builder.Options;                //#D
            using (var context = new EfCoreContext(options)) //#E
            {
                //VERIFY
                context.Database.GetDbConnection().ConnectionString.ShouldEqual(connectionString);
            }
            /********************************************************************
            #A This holds the connection string for the SQL Server database
            #B We need to create DbContextOptionsBuilder<T> class to build the options
            #C Here I define that I want to use the SQL Server database provider
            #D I then build the final DbContextOptions<EfCoreContext> options that the application's DbContext needs
            #E This then allows me to create an instance for my unit tests
             * ******************************************************************/
        }

        [Fact]
        public void TestExampleOnConfiguringNormalOk()
        {
            //SETUP
            const string expectedConnectionString //#A
                = "Server=(localdb)\\mssqllocaldb;Database=EfCore.TestSupport-Test-OnConfiguring;Trusted_Connection=True";
            //ATTEMPT
            using (var context = new DbContextOnConfiguring()) //#B
            {
                //VERIFY
                context.Database.GetDbConnection().ConnectionString.ShouldEqual(expectedConnectionString);
            }
        }

        [Fact]
        public void TestExampleSetupViaOnConfiguringOk()
        {
            //SETUP
            const string connectionString //#A
                = "Server=(localdb)\\mssqllocaldb;Database=EfCore.TestSupport-Test-AlternateName;Trusted_Connection=True";
            //ATTEMPT
            using (var context = new DbContextOnConfiguring //#B
                (connectionString))                         //#B
            {
                //VERIFY
                context.Database.GetDbConnection().ConnectionString.ShouldEqual(connectionString);     
            }
            /********************************************************************
            #A This holds the connection string for the database to be used for the unit test
            #B I then use the application's DbContext constructor that takes a connection string as a parameter
             * ******************************************************************/
        }

    }
}