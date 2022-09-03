using English_Learning.Models;
using English_Learning.RESX;
using English_Learning.Services;
using English_Learning.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace English_Learning.ViewModels
{
    public class PreferencesVievModel : BaseViewModel
    {
        private readonly IDialogService _dialogService;
        private const bool isFirstUseDefault = true;
        private readonly string methodDefault = Preferences.Get("default_method", StudyMethods.Methods[0].Title);
        private const bool notificationIsOnlyPushDefault = false;
        private readonly DateTime startTimeAppNotificationsDefault = DateTime.MinValue;
        private readonly DateTime endTimeAppNotificationsDefault = new DateTime(1,1,1,23,59,0);

        bool isFirstUse;
        string method;
        bool notificationIsOnlyPush;
        DateTime startTime;
        DateTime endTime;

        public bool IsFirstUse
        {
            get => isFirstUse;
            set => SetProperty(ref isFirstUse, value);
        }
        public string Method
        {
            get => method;
            set => SetProperty(ref method, value);
        }
        public bool NotificationIsOnlyPush
        {
            get => notificationIsOnlyPush;
            set => SetProperty(ref notificationIsOnlyPush, value);
        }
        public DateTime StartTimeAppNotification
        {
            get => startTime;
            set => SetProperty(ref startTime, value);
        }
        public DateTime EndTimeAppNotification
        {
            get => endTime;
            set => SetProperty(ref endTime, value);
        }
        public bool IsNotFirstUse
        {
            get => !isFirstUse;
        }

        public List<string> MethodNames
        {
            get
            {
                return StudyMethods.GetMethodNames();
            }
        }

        public Command AboutMethodsCommand { get; }
        public Command CancelCommand { get; }
        public Command SaveCommand { get; }

        public PreferencesVievModel(IDialogService dialogService)
        {
            _dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));

            CancelCommand = new Command(CancelAsync, (object obj) => IsNotFirstUse);
            SaveCommand = new Command(Save);
            AboutMethodsCommand = new Command(async () => await AboutMethodsPopUp());
            this.PropertyChanged +=
                (_, __) => CancelCommand.ChangeCanExecute();
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();



            if (Preferences.Get(nameof(IsFirstUse), true) == true)
                GetDefaultPreferences();
            else
            {
                IsFirstUse = Preferences.Get(nameof(IsFirstUse), isFirstUseDefault);
                Method = Preferences.Get("default_method", methodDefault);//если настроек нет, то Пимслер
                NotificationIsOnlyPush = Preferences.Get(nameof(NotificationIsOnlyPush), notificationIsOnlyPushDefault);
                StartTimeAppNotification = Preferences.Get(nameof(StartTimeAppNotification), startTimeAppNotificationsDefault);
                EndTimeAppNotification = Preferences.Get(nameof(EndTimeAppNotification), endTimeAppNotificationsDefault);
            }
        }

        private void GetDefaultPreferences()
        {
            _dialogService.ShowAlertAsync(AppResources.WelcomeText, "", AppResources.WelcomeButton);

            IsFirstUse = Preferences.Get(nameof(IsFirstUse), isFirstUseDefault);
            Method = Preferences.Get("default_method", methodDefault);
            NotificationIsOnlyPush = Preferences.Get(nameof(NotificationIsOnlyPush), notificationIsOnlyPushDefault);
            StartTimeAppNotification = Preferences.Get(nameof(StartTimeAppNotification), startTimeAppNotificationsDefault);
            EndTimeAppNotification = Preferences.Get(nameof(EndTimeAppNotification), endTimeAppNotificationsDefault);
        }

        private void Save()
        {
            Preferences.Set(nameof(IsFirstUse), false);
            Preferences.Set("default_method", Method);
            Preferences.Set(nameof(NotificationIsOnlyPush), NotificationIsOnlyPush);
            Preferences.Set(nameof(StartTimeAppNotification), StartTimeAppNotification);
            Preferences.Set(nameof(EndTimeAppNotification), EndTimeAppNotification);

            Shell.Current.Navigation.PopToRootAsync();
            Shell.Current.GoToAsync($"{nameof(HomePage)}");

        }
        private async void CancelAsync(object obj)
        {
            await Shell.Current.GoToAsync("..");
        }
        private async Task AboutMethodsPopUp()
        {
            await _dialogService.ShowAlertAsync(AppResources.DetailAboutMethodDescription, AppResources.DetailAboutMethodsTitle, AppResources.DetailAboutMethodsBackButton);

        }
    }
}
