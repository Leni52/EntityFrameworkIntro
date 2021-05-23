using Dapper;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityFramework.Intro.Database
{
    //productRepository in class
    public interface IDatabaseProduct
    {
        void Setup();
    }
    public class DatabaseProduct:IDatabaseProduct
    {
        
       private readonly DatabaseConfig databaseConfig;
        public DatabaseProduct(DatabaseConfig databaseConfig)
        {
            this.databaseConfig = databaseConfig;
        }
        //the constructor will have dependency on the
        //databaseConfig class for accessing the database
        //connection string from the configuration
        public void Setup()
        {
            using var connection = new SqliteConnection($"Data Source={databaseConfig.Name}");
            var table = connection.Query<string>("SELECT name FROM sqlite_master WHERE type='table' AND name = 'Product';");
            var tableName = table.FirstOrDefault();
            if (!string.IsNullOrEmpty(tableName) && tableName == "Product")
                return;

            connection.Execute("Create Table Product (" +
                "Name VARCHAR(100) NOT NULL," +
                "Description VARCHAR(1000) NULL);");

        }



        }
    }

