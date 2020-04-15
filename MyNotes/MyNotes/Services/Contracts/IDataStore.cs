using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyNotes.Services.Contracts
{
    public interface IDataStore<T>
    {
        Task<string> AddItemAsync(T item);
        Task<bool> UpdateItemAsync(T item);
        Task<bool> DeleteItemAsync(string id);
        Task<T> GetItemAsync(string id);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
    }
}
