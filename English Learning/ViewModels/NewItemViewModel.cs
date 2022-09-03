using English_Learning.Models;
using English_Learning.RESX;
using English_Learning.Services;
using English_Learning.Views;
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
        private readonly IDialogService _dialogService;

        private string foreignWord;
        private string translation;
        private StudyMethodsEnum studyMethod;
        private DateTime dateOfInsertion;
        private int level;
        private DateTime lastViewed;
        private DateTime nextViewing;
        private bool isArchived;
        private string selectedMethod;

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

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }
        public Command AboutMethodsCommand { get; }

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


        private async Task AboutMethodsPopUp()
        {
            await _dialogService.ShowAlertAsync(AppResources.DetailAboutMethodDescription, AppResources.DetailAboutMethodsTitle, AppResources.DetailAboutMethodsBackButton);

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
                StudyMethodEnum = StudyMethod,
                DateOfInsertion = DateOfInsertion,
                Level = Level,
                LastViewed = LastViewed,
                IsArchived = IsArchived
            };

            await WordDataStore.AddItemAsync(newItem);
            Clear(newItem);
            await Shell.Current.Navigation.PopToRootAsync();


            //null ref
            await Shell.Current.GoToAsync($"{nameof(HomePage)}");
            await Shell.Current.Navigation.PopToRootAsync();
        }
    }
}
