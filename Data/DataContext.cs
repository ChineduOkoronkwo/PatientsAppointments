using System;
using System.Collections.Generic;

namespace TestWebApi.Data
{
    public class DataContext
    {
        private static int id = 0;
        private Dictionary<string, string> _dataStore; 
        
        public DataContext()
        {
            _dataStore = new Dictionary<string, string>();
        }

        public bool Create(string id, string data) {
            if (_dataStore.ContainsKey(id)) 
            {
                return false;
            } 
               
            _dataStore.Add(id, data);
            return true;    
        }

        internal void Dispose()
        {
            throw new NotImplementedException();
        }

        public bool Update(string id, string data) {
            if (_dataStore.ContainsKey(id)) 
            {
               _dataStore[id] = data;
               return true;
            }    
            return false; 
        }

        public bool Delete(string id) 
        {
            if (_dataStore.ContainsKey(id)) 
            {
               _dataStore.Remove(id);
               return true;
            }    
            return false; 
        }

        public string GetById(string id) {
            if (_dataStore.ContainsKey(id)) 
            {
              return _dataStore[id];
            }    
            return null; 
        }

        public Dictionary<string, string> Database 
        {
            get 
            {
                return _dataStore;
            }
        }

        public static int GenerateId
        {
            get 
            {
                return ++id;
            }
        }
    }
}