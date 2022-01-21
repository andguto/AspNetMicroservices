using MongoDB.Driver;

namespace Catalog.API.Data.ContextSeed.Interfaces
{
    public interface ICatalogContextSeed<T> where T : class
    {
        void SeedData(IMongoCollection<T> collection);
    }
}
