using English_Learning.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace English_Learning.Services
{
    public class MockDataStore : IDataStore<Word>
    {
        readonly List<Word> words;
        readonly List<string> languages;

        public MockDataStore()
        {
            languages = new List<string>()
            {
                "Русский",
                "English"
            };
            words = new List<Word>()
            {
                new Word {
                    Id = Guid.NewGuid().ToString(),
                    ForeignWord = "First Word",
                    Translation = "translation of first Word",
                    StudyMethod = StudyMethods.Pimsler,
                    DateOfInsertion = DateTime.Now,
                    Level = 0,
                    LastViewed = DateTime.Now,
                    NextViewing = Method.GetNextViewingDateTime(StudyMethods.Pimsler, DateTime.Now, DateTime.Now, 0),
                    IsArchived = false
                },
                new Word {
                    Id = Guid.NewGuid().ToString(),
                    ForeignWord = "second Word",
                    Translation = "translation of second word",
                    StudyMethod = StudyMethods.Leitner,
                    DateOfInsertion = DateTime.Now,
                    Level = 0,
                    LastViewed = DateTime.Now,
                    NextViewing = Method.GetNextViewingDateTime(StudyMethods.Leitner, DateTime.Now, DateTime.Now, 0),
                    IsArchived = false
                }
            };
        }

        //public async Task<bool> AddItemAsync(Item item)
        //{
        //    items.Add(item);

        //    return await Task.FromResult(true);
        //}
        public async Task<bool> AddWordAsync(Word word)
        {
            words.Add(word);

            return await Task.FromResult(true);
        }

        //public async Task<bool> UpdateItemAsync(Item item)
        //{
        //    var oldItem = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
        //    items.Remove(oldItem);
        //    items.Add(item);

        //    return await Task.FromResult(true);
        //}

        //public async Task<bool> DeleteItemAsync(string id)
        //{
        //    var oldItem = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
        //    items.Remove(oldItem);

        //    return await Task.FromResult(true);
        //}

        //public async Task<Item> GetItemAsync(string id)
        //{
        //    return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        //}
        public async Task<Word> GetWordAsync(string id)
        {
            return await Task.FromResult(words.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Word>> GetWordsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(words);
        }

        //public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        //{
        //    return await Task.FromResult(items);
        //}

        public async Task<IEnumerable<string>> GetLanguagesAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(languages);
        }
    }
}