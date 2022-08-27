using English_Learning.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace English_Learning.Services
{
    public interface IDataStore<T>
    {
        //Task<bool> AddItemAsync(T item);
        Task<bool> AddWordAsync(Word word);
        //Task<bool> UpdateItemAsync(T item);
        //Task<bool> DeleteItemAsync(string id);
        //Task<T> GetItemAsync(string id);
        Task<T> GetWordAsync(string id);
        Task<IEnumerable<Word>> GetWordsAsync(bool forceRefresh = false);
        //Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
        Task<IEnumerable<string>> GetLanguagesAsync(bool forceRefresh = false);
    }
}
