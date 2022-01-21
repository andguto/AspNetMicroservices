using Catalog.API.Data.Context.Interfaces;
using Catalog.API.Entities.Repositories.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.API.Entities.Repositories.Classes
{
    public class CRUDRepository<T> : ICRUDRepository<T> where T : class
    {
        private readonly ICatalogContext _context;
        public CRUDRepository(ICatalogContext catalogContext)
        {
            _context = catalogContext ?? throw new ArgumentNullException(nameof(catalogContext));
        }
        public async Task<bool> Delete(T objeto)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> Delete(IEnumerable<T> lista)
        {
            throw new System.NotImplementedException();
        }

        public async Task<T> Get(string id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            //return await _context.Products.Find(x => true).ToListAsync();
            // precisava acessar um objeto generico
            throw new System.NotImplementedException();
        }

        public async Task<string> Insert(T objeto)
        {
            throw new System.NotImplementedException();
        }

        public async Task<string> InsertRange(IEnumerable<T> lista)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> Update(T objeto)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> UpdateRange(IEnumerable<T> lista)
        {
            throw new System.NotImplementedException();
        }
    }
}
