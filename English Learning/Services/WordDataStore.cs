using English_Learning.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace English_Learning.Services
{
    public class WordDataStore : IDataStore<Word> 
    {
        List<Word> words;
        public WordDataStore()
        {
            words = new List<Word>()
            {
                new Word {
                    Id = Guid.NewGuid().ToString(),
                    ForeignWord = "First Word",
                    Translation = "translation of first Word",
                    StudyMethodEnum = StudyMethodsEnum.Pimsler,
                    DateOfInsertion = DateTime.Now,
                    Level = 0,
                    LastViewed = DateTime.Now,
                    IsArchived = false
                },
                new Word {
                    Id = Guid.NewGuid().ToString(),
                    ForeignWord = "second Word",
                    Translation = "translation of second word",
                    StudyMethodEnum = StudyMethodsEnum.Leitner,
                    DateOfInsertion = DateTime.Now,
                    Level = 0,
                    LastViewed = DateTime.Now,
                    IsArchived = false
                }
            };
        }
        public async Task<bool> AddItemAsync(Word word)
        {
            words.Add(word);

            return await Task.FromResult(true);
        }
        public async Task<bool> UpdateItemAsync(Word word)
        {
            var oldWord = words.Where((Word arg) => arg.Id == word.Id).FirstOrDefault();
            words.Remove(oldWord);
            words.Add(word);

            return await Task.FromResult(true);
        }
        public async Task<Word> GetItemAsync(string id)
        {
            return await Task.FromResult(words.FirstOrDefault(s => s.Id == id));
        }
        public async Task<bool> DeleteItemAsync(string id)
        {
            Word findedWord = words.Where((Word arg) => arg.Id == id).FirstOrDefault();

            if (findedWord != null)
                words.Remove(findedWord);

            return await Task.FromResult(true);
        }
        public async Task<IEnumerable<Word>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(words);
        }
    }
}