using English_Learning.Models;
using English_Learning.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace English_Learning.Data
{
    public class WordDataStore : IDataStore<Word>
    {
        private static string filename = "WordsXmlResource.xml";
        private List<Word> words;
        private List<Word> Words
        {
            get
            {
                if (words == null)
                    return DeserializeWords();
                else return words;
            }
            set
            {
                words = value;
            }
        }

        public async Task<bool> AddItemAsync(Word word)
        {
            Words.Add(word);
            SerializeWrods(Words);
            return await Task.FromResult(true);
        }
        public async Task<bool> UpdateItemAsync(Word word)
        {
            var oldWord = Words.Where((arg) => arg.Id == word.Id).FirstOrDefault();
            Words.Remove(oldWord);
            Words.Add(word);
            SerializeWrods(Words);
            return await Task.FromResult(true);
        }
        public async Task<Word> GetItemAsync(string id)
        {
            return await Task.FromResult(Words.FirstOrDefault(s => s.Id == id));
        }
        public async Task<bool> DeleteItemAsync(string id)
        {
            Word findedWord = Words.Where((arg) => arg.Id == id).FirstOrDefault();

            if (findedWord != null)
                Words.Remove(findedWord);

            SerializeWrods(Words);

            return await Task.FromResult(true);
        }
        public async Task<IEnumerable<Word>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(DeserializeWords());
        }
        public List<Word> DeserializeWords()
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//" + filename;
            if (!File.Exists(path))
                SerializeWrods(new List<Word>());

            XmlSerializer reader =
                new XmlSerializer(typeof(List<Word>));

            StreamReader file = new StreamReader(path);
            Words = (List<Word>)reader.Deserialize(file);
            file.Close();

            return Words;
        }
        public void SerializeWrods(List<Word> words)
        {
            XmlSerializer writer =
                new XmlSerializer(typeof(List<Word>));

            var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//" + filename;
            FileStream file = File.Create(path);

            writer.Serialize(file, words);
            file.Close();
        }
    }
}