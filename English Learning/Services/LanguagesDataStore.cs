using English_Learning.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace English_Learning.Services
{
    public class LanguagesDataStore : IDataStore<Language>
    {
        List<Language> languages;
        public LanguagesDataStore()
        {
            languages = new List<Language>()
            {
                new Language { Id = "0", Name = "English", CultureInfo = CultureInfo.GetCultureInfo("en-US") },
                new Language { Id = "1", Name = "Russian", CultureInfo = CultureInfo.GetCultureInfo("ru-RU") }
            };
        }
        public async Task<bool> AddItemAsync(Language language)
        {
            languages.Add(language);

            return await Task.FromResult(true);
        }
        public async Task<bool> UpdateItemAsync(Language language)
        {
            var oldLanguage = languages.Where((Language arg) => arg.Id == language.Id).FirstOrDefault();
            languages.Remove(oldLanguage);
            languages.Add(language);

            return await Task.FromResult(true);
        }
        public async Task<Language> GetItemAsync(string id)
        {
            return await Task.FromResult(languages.FirstOrDefault(s => s.Id == id));
        }
        public async Task<bool> DeleteItemAsync(string id)
        {
            Language findedLanguage = languages.Where((Language arg) => arg.Id == id).FirstOrDefault();

            if (findedLanguage != null)
                languages.Remove(findedLanguage);

            return await Task.FromResult(true);
        }
        public async Task<IEnumerable<Language>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(languages);
        }
    }
}