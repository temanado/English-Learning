using English_Learning.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace English_Learning.ViewModels
{
    [QueryProperty(nameof(WordId), nameof(WordId))]
    public class ItemDetailViewModel : BaseViewModel
    {
        private string wordId;
        private string foreignWord;
        private string translation;
        private StudyMethods studyMothod;
        private DateTime dateOfInsertion;
        private int level;
        private DateTime lastViewed;
        private DateTime nextViewing;
        private bool isArchived;
        public string Id { get; set; }
        public string ForeignWord
        {
            get => foreignWord;
            set => SetProperty(ref foreignWord, value);
        }
        public string Translation
        {
            get => translation;
            set => SetProperty(ref translation, value);
        }
        public StudyMethods StudyMothod
        {
            get => studyMothod;
            set => SetProperty(ref studyMothod, value);
        }
        public DateTime DateOfInsertion
        {
            get => dateOfInsertion;
            set => SetProperty(ref dateOfInsertion, value);
        }
        public int Level
        {
            get => level;
            set => SetProperty(ref level, value);
        }
        public DateTime LastViewed
        {
            get => lastViewed;
            set => SetProperty(ref lastViewed, value);
        }
        public DateTime NextViewing
        {
            get => nextViewing;
            set => SetProperty(ref nextViewing, value);
        }
        public bool IsArchived
        {
            get => isArchived;
            set => SetProperty(ref isArchived, value);
        }
        public string WordId
        {
            get
            {
                return wordId;
            }
            set
            {
                wordId = value;
                LoadWordId(value);
            }
        }


        public async void LoadWordId(string wordId)
        {
            try
            {
                var word = await DataStore.GetWordAsync(wordId);
                Id = word.Id;
                ForeignWord = word.ForeignWord;
                Translation = word.Translation;
                StudyMothod = word.StudyMethod;
                DateOfInsertion = word.DateOfInsertion;
                Level = word.Level;
                LastViewed = word.LastViewed;
                NextViewing = word.NextViewing;
                IsArchived = word.IsArchived;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
