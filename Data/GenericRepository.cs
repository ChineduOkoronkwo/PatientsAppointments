using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using TestWebApi.Data.Entities;

namespace TestWebApi.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly DataContext _dataContext;
        public GenericRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        
        public Task<T> Create(T entity)
        {
            var id = DataContext.GenerateId;
            entity.Id = id;

            var type = typeof(T).Name;
            var data = JsonSerializer.Serialize(entity);

            if (_dataContext.Create(GetId(id, type), data)) 
            {
                return Task.FromResult(entity);
            }

            return null;
        }

        public Task<T> Update(T entity)
        {
            var type = typeof(T).Name;
            var data = JsonSerializer.Serialize(entity);

            if (_dataContext.Update(GetId(entity.Id, type), data)) 
            {
                return Task.FromResult(entity);
            }

            return null;
        }

        public Task<bool> Delete(T entity)
        {
            var id = entity.Id;
            var type = typeof(T).Name;
            var result = _dataContext.Delete(GetId(id, type));
            return Task.FromResult(result);
        }

        public Task<T> GetById(int id)
        {
            var type = typeof(T).Name;
            var data = _dataContext.GetById(GetId(id, type));

            T dataObject = null;
            if (data != null)
            {
                dataObject = JsonSerializer.Deserialize<T>(data);
            }

            return Task.FromResult(dataObject);
        }

        public Task<IList<T>> ListAll()
        {
            var type = typeof(T).Name + "_";
            IList<T> result = new List<T>();

            var database = _dataContext.Database;
            foreach(var kvp in database) {
                if (kvp.Key.StartsWith(type)) 
                {
                    var dataObject = JsonSerializer.Deserialize<T>(kvp.Value);
                    result.Add(dataObject);
                }
            }

            return Task.FromResult(result);
        }        

        private string GetId(int id, string type) 
        {
            return $"{type}_{id}";
        }
    }
}