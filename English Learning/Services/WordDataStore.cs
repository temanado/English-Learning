using English_Learning.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Xamarin.Essentials;

namespace English_Learning.Services
{
    public class WordDataStore : IDataStore<Word>
    {
        private static string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        private static string foldername = "EnglishLearning";
        private static string filename = "WordsXmlResource.xml";

        List<Word> mockWrds = new List<Word>()
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
            var oldWord = Words.Where((Word arg) => arg.Id == word.Id).FirstOrDefault();
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
            Word findedWord = Words.Where((Word arg) => arg.Id == id).FirstOrDefault();

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
            FileStream file = System.IO.File.Create(path);

            writer.Serialize(file, words);
            file.Close();
        }

    }
}




//public WordDataStore()
//{
//    words = new List<Word>()
//    {
//        new Word {
//            Id = Guid.NewGuid().ToString(),
//            ForeignWord = "First Word",
//            Translation = "translation of first Word",
//            StudyMethodEnum = StudyMethodsEnum.Pimsler,
//            DateOfInsertion = DateTime.Now,
//            Level = 0,
//            LastViewed = DateTime.Now,
//            IsArchived = false
//        },
//        new Word {
//            Id = Guid.NewGuid().ToString(),
//            ForeignWord = "second Word",
//            Translation = "translation of second word",
//            StudyMethodEnum = StudyMethodsEnum.Leitner,
//            DateOfInsertion = DateTime.Now,
//            Level = 0,
//            LastViewed = DateTime.Now,
//            IsArchived = false
//        }
//    };
//}