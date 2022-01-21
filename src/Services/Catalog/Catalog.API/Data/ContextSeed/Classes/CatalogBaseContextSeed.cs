using Catalog.API.Data.ContextSeed.Interfaces;
using Catalog.API.Entities;
using MongoDB.Driver;
using System.Collections.Generic;

namespace Catalog.API.Data.ContextSeed.Classes
{
    public class CatalogBaseContextSeed<T> : ICatalogContextSeed<T> where T : class
    {
        public void SeedData(IMongoCollection<T> collection)
        {
            //bool existProduct = collection.Find(p => true).Any();
            //if (!existProduct)
            //    collection.InsertMany(GetPreconfigured());
        }

        public IEnumerable<T> GetPreconfigured<T>()
        {
            return new List<T>();
        }

    }
}
