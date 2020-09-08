using System;
using System.Threading.Tasks;
using TestWebApi.Data.Entities;

namespace TestWebApi.Data
{
    public interface IUnitOfWork
    {
         IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;         
    }
}