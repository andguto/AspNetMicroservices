using Dapper;
using Discount.Grpc.Entities;
using Discount.Grpc.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Threading.Tasks;

namespace Discount.Grpc.Repositories.Classes
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IDiscountRepository _repository;
        private readonly IConfiguration _configuration;
        private readonly string _connectionString; 

        public DiscountRepository(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<Coupon> GetDiscount(string productName)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DataBaseSettings:ConnectionString"));

            var sql = @"SELECT *
                        FROM Coupon
                        WHERE ProductName = @ProductName";

            var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>(sql, new { productName = productName });

            if (coupon == null)
                return new Coupon { ProductName = "No Discount", Amount = 0, Description = "No Discount description" };

            return coupon;
        }
        
        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DataBaseSettings:ConnectionString"));

            var sql = @"INSERT INTO Coupon (ProductName, Amount, Description) VALUES (@ProductName, @Amount, @Description)";

            var affected = await connection.ExecuteAsync(sql, new { ProductName = coupon.ProductName, Amount = coupon.Amount, Description = coupon.Description });

            if(affected == 0)
                return false;

            return true;
        }

        public async Task<bool> DeleteDiscount(string productName)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DataBaseSettings:ConnectionString"));

            var sql = @"DELETE FROM Coupon WHERE ProductName = @ProductName";

            var affected = await connection.ExecuteAsync(sql, new { ProductName = productName });

            if (affected == 0)
                return false;

            return true;

        }


        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DataBaseSettings:ConnectionString"));

            var sql = @"UPDATE Coupon SET ProductName = @ProductName, Amount = @Amount, Description = @Description WHERE Id = @Id";

            var affected = await connection.ExecuteAsync(sql, new { ProductName = coupon.ProductName, Amount = coupon.Amount, Description = coupon.Description, Id = coupon.Id });

            if (affected == 0)
                return false;

            return true;
        }
    }
}
