using Microsoft.Extensions.Logging;
using Ordering.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Persistence
{
    public class OrderContextSeed
    {

        public static async Task SeedAsync(OrderContext orderContext, ILogger<OrderContextSeed> logger)
        {
            if (!orderContext.Orders.Any())
            {
                orderContext.Orders.AddRange(GetPreconfigureOrders());
                await orderContext.SaveChangesAsync();
                logger.LogInformation("Seed database associated with context {DbContextName}", typeof(OrderContextSeed).Name);
            }
        }

        private static IEnumerable<Order> GetPreconfigureOrders()
        {
            return new List<Order>
            {
                new Order() {UserName = "AndersonMasiero", FirstName ="Anderson", LastName = "Masiero", EmailAddress = "anderson.masiero@gmail.com", Country = "Brasil", TotalPrice = 350}
            };
        }
    }
}
