using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Data.Context.Interfaces
{
    public interface ICatalogContext
    {
        IMongoCollection<Product> Products { get; }
    }
}