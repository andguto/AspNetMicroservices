using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.API.Entities.Repositories.Interfaces
{
    public interface ICRUDRepository<T> where T : class
    {
        Task<T> Get(string id);
        Task<IEnumerable<T>> GetAll();
        Task<string> Insert(T objeto);
        Task<string> InsertRange(IEnumerable<T> lista);
        Task<bool> Update(T objeto);
        Task<bool> UpdateRange(IEnumerable<T> lista);
        Task<bool> Delete(T objeto);
        Task<bool> Delete(IEnumerable<T> lista);
    }
}
