using English_Learning.Models;
using English_Learning.RESX;
using English_Learning.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
        private StudyMethodsEnum studyMethod;
        private DateTime dateOfInsertion;
        private int level;
        private DateTime lastViewed;
        private DateTime nextViewing;
        private bool isArchived;
        private string selectedMethod;
        private bool translationIsVisible;

        private Word OldWord { get; set; }

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
        public StudyMethodsEnum StudyMethod
        {
            get => studyMethod;
            set => SetProperty(ref studyMethod, value);
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

        public bool TranslationIsVisible
        {
            get { return translationIsVisible; }
            set { SetProperty(ref translationIsVisible, value); }
        }

        private readonly IDialogService _dialogService;

        public Command DeleteCommand { get; }
        public Command UpdateCommand { get; }
        public Command AboutMethodsCommand { get; }
        public Command GetTranslationCommand { get; }

        public ItemDetailViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));

            DeleteCommand = new Command(Delete, IsNotClear);
            UpdateCommand = new Command(Update, ValidateSave);
            GetTranslationCommand = new Command( () =>  GetTranslation());
            AboutMethodsCommand = new Command(async () => await AboutMethodsPopUp());
            this.PropertyChanged +=
                (_, __) => DeleteCommand.ChangeCanExecute();
            this.PropertyChanged +=
                (_, __) => UpdateCommand.ChangeCanExecute();
        }

        private void GetTranslation()
        {
            TranslationIsVisible = !TranslationIsVisible;
        }

        private async Task AboutMethodsPopUp()
        {
            await _dialogService.ShowAlertAsync(AppResources.DetailAboutMethodDescription, AppResources.DetailAboutMethodsTitle, AppResources.DetailAboutMethodsBackButton);
        }

        public async void LoadWordId(string wordId)
        {
            try
            {
                var word = await WordDataStore.GetItemAsync(wordId);
                OldWord = word;

                Id = word.Id;
                ForeignWord = word.ForeignWord;
                Translation = word.Translation;
                StudyMethod = word.StudyMethodEnum;
                SelectedMethod = StudyMethods.GetMethodNameFormEnum(word.StudyMethodEnum);
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
        public List<string> MethodNames
        {
            get
            {
                return StudyMethods.GetMethodNames();
            }
        }
        public string SelectedMethod
        {
            get
            {
                return selectedMethod;
            }
            set
            {
                SetProperty(ref selectedMethod, value);

                StudyMethod = StudyMethods.GetMethodEnumFromString(value);

            }
        }
        private bool IsNotClear()
        {
            return !String.IsNullOrWhiteSpace(Id);
        }
        private async void Delete()
        {
            await WordDataStore.DeleteItemAsync(Id);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
        private bool ValidateSave()
        {
            //условия: не одинаковые значения и заполненность
            return !String.IsNullOrWhiteSpace(ForeignWord)
                && !String.IsNullOrWhiteSpace(Translation)
                &&
                    (
                       !ForeignWord.Equals(OldWord.ForeignWord)
                    || !Translation.Equals(OldWord.Translation)
                    || !StudyMethod.Equals(OldWord.StudyMethodEnum)
                    );
        }
        private async void Update()
        {
            Word newWord = new Word()
            {
                Id = WordId,
                ForeignWord = ForeignWord,
                Translation = Translation,
                StudyMethodEnum = StudyMethod,
                DateOfInsertion = DateOfInsertion,
                Level = Level,
                LastViewed = LastViewed,
                IsArchived = IsArchived
            };

            await WordDataStore.UpdateItemAsync(newWord);


            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}



