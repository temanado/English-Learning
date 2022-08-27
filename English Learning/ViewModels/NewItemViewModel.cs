using English_Learning.Models;
using English_Learning.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace English_Learning.ViewModels
{
    public class NewItemViewModel : BaseViewModel
    {
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

        private readonly IDialogService _dialogService;

        public NewItemViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));

            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(Clear, IsNotClear);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
            this.PropertyChanged +=
                (_, __) => CancelCommand.ChangeCanExecute();
            AboutMethodsCommand = new Command(async () => await AboutMethodsPopUp());
        }


        public Command SaveCommand { get; }
        public Command CancelCommand { get; }
        public Command AboutMethodsCommand { get; }


        private async Task AboutMethodsPopUp()
        {
            await _dialogService.ShowAlertAsync("About methods", "The title of the alert", "Back");

        }
        

        //private async void OnCancel()
        //{
        //    // This will pop the current page off the navigation stack
        //    await Shell.Current.GoToAsync("..");
        //}

        private bool IsNotClear(object arg)
        {
            return !String.IsNullOrWhiteSpace(ForeignWord)
                || !String.IsNullOrWhiteSpace(Translation);
        }
        private void Clear(object obj)
        {
            ForeignWord = "";
            Translation = "";
        }
        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(ForeignWord)
                && !String.IsNullOrWhiteSpace(Translation);
        }
        private async void OnSave()
        {
            Word newItem = new Word()
            {
                Id = Guid.NewGuid().ToString(),
                ForeignWord = ForeignWord,
                Translation = Translation,
                StudyMothod = StudyMothod,
                DateOfInsertion = DateOfInsertion,
                MethodLevel = MethodLevel,
                LastViewed = LastViewed,
                NextViewing = NextViewing,
                IsArchived = IsArchived
            };

            await DataStore.AddWordAsync(newItem);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
