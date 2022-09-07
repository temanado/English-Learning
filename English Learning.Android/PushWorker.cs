using Android.Content;
using Android.Util;
using AndroidX.Work;
using English_Learning.Models;
using English_Learning.Services;
using System.Threading;
using Xamarin.Forms;

namespace English_Learning.Droid
{
    public class PushWorker : Worker
    {
        INotificationManager notificationManager;

        public const string TAG = "PushWorker";
        public PushWorker(Context context, WorkerParameters workerParams) :
            base(context, workerParams)
        {

            notificationManager = DependencyService.Get<INotificationManager>();
            notificationManager.NotificationReceived += (sender, eventArgs) =>
            {
                var evtData = (NotificationEventArgs)eventArgs;
                ShowNotification(evtData.Title, evtData.Message);
            };
        }
        void ShowNotification(string title, string message)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                Log.Debug(TAG, $"Notification Received:\nTitle: {title}\nMessage: {message}");
            });
        }
        public override Result DoWork()
        {
            Log.Debug(TAG, "Started.");

            //Perform a process here, simulated by sleeping for 5 seconds.


            //logic

            //push
            //title 
            //notificationManager.SendNotification(title, message);
            notificationManager.SendNotification("notification", "my first notification");

            Log.Debug(TAG, "Completed.");

            return Result.InvokeSuccess();
        }

    }
}