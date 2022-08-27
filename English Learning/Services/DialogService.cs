using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace English_Learning.Services
{
    public class DialogService : IDialogService
    {
        public async Task ShowAlertAsync(string message, string title, string buttonLabel)
        {
            await Application.Current.MainPage.DisplayAlert(title, message, buttonLabel);


            //недоступная версия


            //if (App.IsActive)
            //    await UserDialogs.Instance.AlertAsync(message, title, buttonLabel);
            //else
            //{
            //    MessagingCenter.Instance.Subscribe<object>(this, MessageKeys.AppIsActive, async (obj) =>
            //    {
            //        await UserDialogs.Instance.AlertAsync(message, title, buttonLabel);
            //        MessagingCenter.Instance.Unsubscribe<object>(this, MessageKeys.AppIsActive);
            //    });
            //}
        }

    }
}
