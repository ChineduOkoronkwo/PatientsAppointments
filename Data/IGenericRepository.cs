using System.Collections.Generic;
using System.Threading.Tasks;
using TestWebApi.Data.Entities;

namespace TestWebApi.Data
{
    public interface IGenericRepository <T> where T : BaseEntity
    {
         Task<T> GetById(int Id);
         Task<IList<T>> ListAll();         
         Task<T> Create(T entity);
         Task<T> Update(T entity);
         Task<bool> Delete(T entity);
    }
}