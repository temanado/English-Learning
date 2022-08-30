using English_Learning.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace English_Learning.Services
{
    public interface IDataStore<T>
        where T : class
    {
        Task<bool> AddItemAsync(T item);
        Task<bool> UpdateItemAsync(T word);
        Task<bool> DeleteItemAsync(string id);
        Task<T> GetItemAsync(string id);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);


        //Task<T> GetConfig();
        //Task<bool> SaveConfig();
        //Task<bool> AddWordAsync(T word);
        //Task<bool> UpdateWordAsync(T word);
        //Task<bool> DeleteWordAsync(string id);
        //Task<T> GetWordAsync(string id);
        //Task<IEnumerable<T>> GetWordsAsync(bool forceRefresh = false);
        //Task<IEnumerable<string>> GetLanguagesAsync(bool forceRefresh = false);
    }
}
