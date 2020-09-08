using System.Collections.Generic;
using System.Threading.Tasks;
using TestWebApi.Data.Entities;

namespace TestWebApi.Services
{
    public interface IPatientsService<T> where T : BaseEntity
    {
         Task<T> Create(T entity);
         Task<T> Update(T entity);
         Task<bool> Delete(T entity);
         Task<T> GetById(int id);
         Task<IList<T>> ListAll();
    }
}