using English_Learning.Models;
using English_Learning.RESX;
using English_Learning.Services;
using English_Learning.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace English_Learning.ViewModels
{
    public class HomePageViewModel : BaseViewModel
    {
        private readonly IDialogService _dialogService;

        private Word _selectedWord;
        private bool isFirstUse;

        public ObservableCollection<Word> Words { get; }
        public Command LoadItemsCommand { get; }
        public Command PreferencesCommand { get; set; }
        public Command AddItemCommand { get; }
        public Command<Word> WordTapped { get; }

        public HomePageViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));

            Title = "Browse";
            Words = new ObservableCollection<Word>();

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            PreferencesCommand = new Command(OnPreferences);
            AddItemCommand = new Command(OnAddItem);
            WordTapped = new Command<Word>(OnItemSelected);

            isFirstUse = Preferences.Get(nameof(isFirstUse), true);

        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;
            await Task.FromResult(CheckAndRequestStorageWritePermission());
            //Запрос разрешения на запуск служб после перезагрузки устройства
            //await Task.FromResult(CheckAndRequestReceiveBootCompletedPermission());

            try
            {
                Words.Clear();
                var words = await WordDataStore.GetItemsAsync(true);
                foreach (var word in words)
                {
                    Words.Add(word);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private object CheckAndRequestReceiveBootCompletedPermission()
        {
            throw new NotImplementedException();
        }

        public async Task<PermissionStatus> CheckAndRequestStorageWritePermission()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();

            if (status == PermissionStatus.Granted)
                return status;

            if (status == PermissionStatus.Denied && DeviceInfo.Platform == DevicePlatform.iOS)
            {
                // Prompt the user to turn on in settings
                // On iOS once a permission has been denied it may not be requested again from the application
                return status;
            }

            if (Permissions.ShouldShowRationale<Permissions.StorageWrite>())
            {
                await _dialogService.ShowAlertAsync(AppResources.StorageAccessText, AppResources.StorageAccessTitle, AppResources.OkButton);
            }

            status = await Permissions.RequestAsync<Permissions.StorageWrite>();

            return status;
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedWord = null;
        }

        public Word SelectedWord
        {
            get => _selectedWord;
            set
            {
                SetProperty(ref _selectedWord, value);
                OnItemSelected(value);
            }
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewItemPage));
        }

        private async void OnPreferences(object obj)
        {
            await Shell.Current.GoToAsync(nameof(PreferencesPage));
        }

        async void OnItemSelected(Word word)
        {
            if (word == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.WordId)}={word.Id}");
        }
    }
}
