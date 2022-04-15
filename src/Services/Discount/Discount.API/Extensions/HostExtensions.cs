﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace Discount.API.Extensions
{
    public static class HostExtensions
    {
        public static IHost MigrateDatabase<TContext>(this IHost host, int? retry = 0)
        {

            int retryForAvailability = retry.Value;

            using (var scope= host.Services.CreateScope())
            {

                var services = scope.ServiceProvider;
                var configuration = services.GetRequiredService<IConfiguration>();
                var logger = services.GetRequiredService<ILogger<TContext>>();

                try
                {
                    logger.LogInformation("Migrating PostegreSQL DataBase");

                    using var connection = new NpgsqlConnection(configuration.GetValue<string>("DataBaseSettings:ConnectionString"));
                    connection.Open();

                    using var command = new NpgsqlCommand
                    {
                        Connection = connection,
                    };

                    command.CommandText = "DROP TABLE IF EXISTS Coupon";
                    command.ExecuteNonQuery();

                    command.CommandText = @"CREATE TABLE Coupon(    Id          SERIAL PRIMARY KEY,
                                                                    ProductName VARCHAR(24),
                                                                    Description TEXT,
                                                                    Amount      INT)";
                    command.ExecuteNonQuery();

                    command.CommandText = @"INSERT INTO Coupon(ProductName, Description, Amount) VALUES('IPhone X', 'IPhone Discount', 150)";
                    command.ExecuteNonQuery();

                    command.CommandText = @"INSERT INTO Coupon(ProductName, Description, Amount) VALUES('Samsung Galaxy S10', 'Samsung Discount', 100)";
                    command.ExecuteNonQuery();

                    logger.LogInformation("Migrated PostegreSQL DataBase");
                }
                catch (NpgsqlException postgreSqlEx)
                {
                    logger.LogError(postgreSqlEx, "An error ocurrred while migrating the postgresql database.");
                    
                    if(retryForAvailability < 50)
                    {
                        retryForAvailability++;
                        System.Threading.Thread.Sleep(2000);
                        MigrateDatabase<TContext>(host, retryForAvailability);
                    }
                }

            }
            return host;
        }
    }
}