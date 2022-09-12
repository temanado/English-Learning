using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using English_Learning.Droid;
using English_Learning.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: Dependency(typeof(RequestIgnoreBatteryOptimizationPermission))]
namespace English_Learning.Droid
{
    public class RequestIgnoreBatteryOptimizationPermission : IRequestIgnoreBatteryOptimizationPermission
    {
        Intent intent = new Intent();
        string packageName = MainActivity.mac.PackageName;
        PowerManager pm = (PowerManager)MainActivity.mac.GetSystemService(Context.PowerService);

        Task<bool> IRequestIgnoreBatteryOptimizationPermission.CheckStatusAsync()
        {
            intent.SetAction(Android.Provider.Settings.ActionIgnoreBatteryOptimizationSettings);
            return Task.FromResult( pm.IsIgnoringBatteryOptimizations(packageName));
        }

        Task<bool> IRequestIgnoreBatteryOptimizationPermission.RequestAsync()
        {
            intent.SetAction(Android.Provider.Settings.ActionRequestIgnoreBatteryOptimizations);
            intent.SetData(Android.Net.Uri.Parse("package:" + packageName));
            return Task.FromResult(pm.IsIgnoringBatteryOptimizations(packageName));
        }
    }
}