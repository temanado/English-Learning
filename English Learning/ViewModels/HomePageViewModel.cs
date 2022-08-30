using English_Learning.Models;
using English_Learning.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace English_Learning.ViewModels
{
    public class HomePageViewModel : BaseViewModel
    {
        private Word _selectedWord;

        public ObservableCollection<Word> Words { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<Word> WordTapped { get; }

        public HomePageViewModel()
        {
            Title = "Browse";
            Words = new ObservableCollection<Word>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            WordTapped = new Command<Word>(OnItemSelected);

            AddItemCommand = new Command(OnAddItem);
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

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

        async void OnItemSelected(Word word)
        {
            if (word == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.WordId)}={word.Id}");
        }
    }
}
