using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using English_Learning.Models;
using English_Learning.Views;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace English_Learning.ViewModels
{
    public class LanguageViewModel : BaseViewModel
    {
        public ObservableCollection<string> Languages { get; }

        public Command LoadLanguagesCommand { get; }
        public Command SetLanguageCommand { get; }

        string selectedLanguage;
        public string SelectedLanguage
        {
            get { return selectedLanguage; }
            set 
            { 
                SetProperty(ref selectedLanguage, value);
            }
        }

        public LanguageViewModel()
        {
            LoadLanguagesCommand = new Command(async () => await ExecuteLoadLanguagesCommand());
            SetLanguageCommand = new Command(async () => await LanguageIsSelected(SelectedLanguage));
            Languages = new ObservableCollection<string>();
            //ExecuteLoadLanguagesCommand();
            Languages.Add("English");
            //Languages.Add("Русский");
        }

        async Task ExecuteLoadLanguagesCommand()
        {

            IsBusy = true;

            try
            {
                Languages.Clear();
                var langs = await DataStore.GetLanguagesAsync(true);
                foreach (var lang in langs)
                {
                    Languages.Add(lang);
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
            SelectedLanguage = null;
        }

        private async Task LanguageIsSelected(string value)
        {
            //Логика локализации
            //Переход на домашнюю страницу
            Shell.Current.GoToAsync("HomePage");
        }

    }
}
