using Catalog.API.Data.Context.Interfaces;
using Catalog.API.Data.ContextSeed.Classes;
using Catalog.API.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;

namespace Catalog.API.Data.Context.Classes
{
    public class CatalogContext<T> : ICatalogContext where T : class
    {
        public CatalogContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DataBaseSettings:ConnectionString"));
            var dataBase = client.GetDatabase(configuration.GetValue<string>("DataBaseSettings:DataBaseName"));
            var collectionName = configuration.GetValue<string>("DataBaseSettings:CollectionName");

            Products = dataBase.GetCollection<Product>(collectionName);
            CatalogContextSeed.SeedData(Products);
        }

        public IMongoCollection<Product> Products { get; }

    }
}
