using English_Learning.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace English_Learning.ViewModels
{
    [QueryProperty(nameof(wordId), nameof(wordId))]
    public class ItemDetailViewModel : BaseViewModel
    {
        private string wordId;
        private string foreignWord;
        private string translation;
        private bool studyMothod;
        private DateTime dateOfInsertion;
        private int methodLevel;
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
        public bool StudyMothod
        {
            get => studyMothod;
            set => SetProperty(ref studyMothod, value);
        }
        public DateTime DateOfInsertion
        {
            get => dateOfInsertion;
            set => SetProperty(ref dateOfInsertion, value);
        }
        public int MethodLevel
        {
            get => methodLevel;
            set => SetProperty(ref methodLevel, value);
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
                LoadwordId(value);
            }
        }


        public async void LoadwordId(string wordId)
        {
            try
            {
                var item = await DataStore.GetWordAsync(wordId);
                Id = item.Id;
                ForeignWord = item.ForeignWord;
                Translation = item.Translation;
                StudyMothod = item.StudyMothod;
                DateOfInsertion = item.DateOfInsertion;
                MethodLevel = item.MethodLevel;
                LastViewed = item.LastViewed;
                NextViewing = item.NextViewing;
                IsArchived = item.IsArchived;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
